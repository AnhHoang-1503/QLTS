<template>
    <div class="container unselectable" @keydown="keyDownEvent($event)" v-autofocus tabindex="0">
        <div class="container__header">
            <div class="header__title">
                {{ $_MISAResources.assetsManagementIncrease.title }}
            </div>
            <div class="header__button">
                <MISAToolTip text="Ctrl + D">
                    <template #content>
                        <div class="header__button--add">
                            <MISAButton
                                :text="$_MISAResources.assetsManagementIncrease.add"
                                @click-btn="
                                    $store.dispatch(
                                        'voucherForm/showForm',
                                        $_MISAEnums.formMode.add
                                    )
                                "
                            ></MISAButton>
                        </div>
                    </template>
                </MISAToolTip>
                <div class="header__button--layout">
                    <LayoutOptions v-model:currentOption="layoutOption"></LayoutOptions>
                </div>
            </div>
        </div>
        <div class="container__main unselectable" ref="containerMain">
            <div
                class="main__table table__vouchers"
                :style="{ height: `calc(100% - ${secondTableHeight}px)` }"
            >
                <div class="vouchers__header">
                    <div class="header__toolbar__left">
                        <div class="vouchers__header__input">
                            <MISASearch
                                :placeholder="
                                    $_MISAResources.assetsManagementIncrease.searchPlaceholder
                                "
                                :width="'300px'"
                                v-model="searchValue"
                            >
                            </MISASearch>
                        </div>
                        <div class="toolbar__total-selected-records" v-if="countSelectedVouchers">
                            <div
                                class="total-selected-records__number"
                                v-html="
                                    $_MISAResources.assetsManagement.toolbar.totalSelectedRecords(
                                        countSelectedVouchers
                                    )
                                "
                            ></div>
                            <div class="total-selected-records__clear" @click="clearSelectedItems">
                                {{ $_MISAResources.assetsManagement.toolbar.clearSelected }}
                            </div>
                        </div>
                    </div>
                    <div class="vouchers__header__tool">
                        <MISAToolTip :text="$_MISAResources.assetsManagementIncrease.delete">
                            <template #content>
                                <button
                                    class="vouchers__btn vouchers__btn--delete"
                                    v-if="
                                        Object.values(
                                            $store.state.increase.selectedVouchers
                                        ).filter((e) => e).length
                                    "
                                    @click="showDeleteManyConfirmDialog"
                                >
                                    <MISAIcon icon="delete"></MISAIcon>
                                </button>
                            </template>
                        </MISAToolTip>
                        <MISAToolTip :text="$_MISAResources.assetsManagementIncrease.refresh">
                            <template #content>
                                <button
                                    class="vouchers__btn vouchers__btn--refresh"
                                    @click="refreshData()"
                                >
                                    <MISAIcon icon="refresh"></MISAIcon>
                                </button>
                            </template>
                        </MISAToolTip>
                        <MISAToolTip :text="$_MISAResources.assetsManagementIncrease.print">
                            <template #content>
                                <button class="vouchers__btn vouchers__btn--print">
                                    <MISAIcon icon="print"></MISAIcon>
                                </button>
                            </template>
                        </MISAToolTip>
                        <MISAToolTip :text="$_MISAResources.assetsManagementIncrease.options">
                            <template #content>
                                <button class="vouchers__btn vouchers__btn--more">
                                    <MISAIcon icon="more-options"></MISAIcon>
                                </button>
                            </template>
                        </MISAToolTip>
                    </div>
                </div>
                <div class="vouchers__table">
                    <Table
                        :checkBoxKey="'ref_id'"
                        ref="firstTable"
                        :toolColumn="{ edit: true, delete: true }"
                        :columns="$store.state.increase.firstTableColumns"
                        :tableData="vouchersData"
                        @click-row="showDetails"
                        :selectedRow="selectedRow"
                        :footer="{ total_cost: $store.state.increase.totalCost }"
                        :isLoading="$store.state.increase.firstTableLoading"
                        @edit-row="showEditForm"
                        @double-click="showEditForm"
                        :offset="pageSize * (pageIndex - 1)"
                        @delete-row="showDeleteConfirmDialog"
                        :selectedList="$store.state.increase.selectedVouchers"
                        @update-selected-list="updateSelectedVouchers"
                        @update-columns="
                            (columnsList) =>
                                $store.dispatch('increase/updateFirstTableColumns', columnsList)
                        "
                        :item="'Vouchers'"
                    ></Table>
                </div>
                <div class="vouchers__paging table__paging">
                    <p
                        class="paging__totalrecords"
                        v-html="
                            $_MISAResources.assetsManagement.table.totalRecords(
                                $store.state.increase.totalVouchers
                            )
                        "
                    ></p>
                    <MISAPaging
                        :totalRecords="$store.state.increase.totalVouchers"
                        v-model:recordPerPage="pageSize"
                        v-model:currentPage="pageIndex"
                    />
                </div>
            </div>
            <div
                class="main__line"
                @mousedown="mouseDownInLine($event)"
                v-if="layoutOption != $_MISAEnums.layoutOptions.oneTable"
            ></div>
            <div class="main_table table__detail" :style="{ height: secondTableHeight + 'px' }">
                <div class="detail__header">
                    {{ $_MISAResources.assetsManagementIncrease.detailTable.title }}
                </div>
                <Table
                    :tableData="$store.state.increase.details"
                    ref="secondTable"
                    :columns="$store.state.increase.secondTableColumns"
                    :isLoading="$store.state.increase.secondTableLoading"
                    :showEmpty="false"
                    @update-columns="
                        (columnsList) =>
                            $store.dispatch('increase/updateSecondTableColumns', columnsList)
                    "
                ></Table>
            </div>
        </div>
        <Teleport to="#app">
            <VourcherForm
                v-if="$store.state.voucherForm.formMode != $_MISAEnums.formMode.hide"
                @close-form="$store.commit('voucherForm/hideForm')"
                @update-table="getVouchers"
            />
            <MISADialog
                :message="dialogMessage"
                v-if="showDeleteConfirm"
                @cancel="showDeleteConfirm = false"
            >
                <template #middle-btn>
                    <MISAButton
                        type="sub"
                        :text="$_MISAResources.dialog.delete.btnCancel"
                        @click-btn="showDeleteConfirm = false"
                        :style="{ 'border-color': '#afafaf', 'box-shadow': 'none' }"
                        :tabindex="1"
                    ></MISAButton>
                </template>
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.delete.btnConfirm"
                        @click-btn="
                            () => {
                                deleteVoucher();
                                showDeleteConfirm = false;
                            }
                        "
                        :tabindex="2"
                    ></MISAButton>
                </template>
            </MISADialog>
            <MISADialog
                :message="dialogMessage"
                v-if="showDeleteManyConfirm"
                @cancel="showDeleteManyConfirm = false"
            >
                <template #middle-btn>
                    <MISAButton
                        type="sub"
                        :text="$_MISAResources.dialog.delete.btnCancel"
                        @click-btn="showDeleteManyConfirm = false"
                        :style="{ 'border-color': '#afafaf', 'box-shadow': 'none' }"
                        :tabindex="1"
                    ></MISAButton>
                </template>
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.delete.btnConfirm"
                        @click-btn="
                            () => {
                                deleteManyVoucher();
                                showDeleteManyConfirm = false;
                            }
                        "
                        :tabindex="2"
                    ></MISAButton>
                </template>
            </MISADialog>
        </Teleport>
        <Teleport to="#app">
            <Transition name="toast-message">
                <MISAToastMessage
                    :type="'success'"
                    :message="$store.state.increase.toastContent"
                    :icon="'success'"
                    v-if="$store.state.increase.showSuccessToast"
                ></MISAToastMessage>
            </Transition>
            <MISALoadingOverlay :show="loadingOverlay" />
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./AssetsManagementIncrease.css);
</style>

<script>
import LayoutOptions from "./components/LayoutOptions/LayoutOptions.vue";
import Table from "./components/Table/Table.vue";
import MISATextfield from "../../components/base/MISATextfield/MISATextfield.vue";
import MISASearch from "../../components/base/MISASearch/MISASearch.vue";
import MISAPaging from "@/components/base/MISAPaging/MISAPaging.vue";
import VourcherForm from "./components/VourcherForm/VourcherForm.vue";
import MISADialog from "@/components/base/MISADialog/MISADialog.vue";
import MISAToastMessage from "@/components/base/MISAToastMessage/MISAToastMessage.vue";
import MISALoadingOverlay from "@/components/base/MISALoadingOverlay/MISALoadingOverlay.vue";

export default {
    name: "AssetsManagementIncrease",
    components: {
        LayoutOptions,
        Table,
        MISATextfield,
        MISASearch,
        MISAPaging,
        VourcherForm,
        MISAToastMessage,
        MISADialog,
        MISALoadingOverlay,
    },
    data() {
        return {
            // Kiểu layout hiện tại
            layoutOption: this.$_MISAEnums.layoutOptions.stacked,
            // mouse hold trên line thay đổi
            mouseHold: false,
            // vị trí trước đó của con trỏ chuột
            previousMouseY: 0,
            // Chiều cao bảng 2
            secondTableHeight: 300,
            // Hiển thị form
            showForm: false,
            // Giá trị tìm kiếm
            searchValue: "",
            // Số bản ghi 1 trang
            pageSize: 20,
            // Trang hiện tại
            pageIndex: 1,
            // thông báo dialog
            dialogMessage: "",
            // Chứng từ xoá
            deleteTarget: null,
            // hiển thị dialog xoá
            showDeleteConfirm: false,
            // hiển thị dialog xoá nhiều
            showDeleteManyConfirm: false,
            // loading overlay
            loadingOverlay: false,
        };
    },
    methods: {
        /**
         * Sự kiện nhấn chuột xuống trên line thay đổi kích thước bảng
         * @param {*} event
         * Author: vtahoang (29/08/2023)
         */
        mouseDownInLine(event) {
            this.mouseHold = true;
            this.previousMouseY = event.clientY;
            // Thêm sự kiện mousemove và mouseup cho window
            window.addEventListener("mousemove", this.changeSizeTable);
        },
        /**
         * Kéo thả thay đổi kích thước bảng
         * @param {*} event
         * Author: vtahoang (29/08/2023)
         */
        changeSizeTable(event) {
            if (this.mouseHold) {
                let currentMouseY = event.clientY;
                // Tính sự thay đổi chiều cao
                let changeHeight = this.previousMouseY - currentMouseY;
                this.previousMouseY = currentMouseY;

                let newSecondTableHeight = this.secondTableHeight + changeHeight;
                let newFirstTableHeight =
                    this.$refs.containerMain.offsetHeight -
                    50 -
                    newSecondTableHeight -
                    changeHeight;
                if (
                    (newSecondTableHeight >= 42 || changeHeight > 0) &&
                    (newFirstTableHeight >= 186 || changeHeight < 0)
                ) {
                    this.secondTableHeight = newSecondTableHeight;
                }
            }
        },
        /**
         * Hiển thị chi tiết
         * @param {*} index index target
         * Author: vtahoang (24/08/2023)
         */
        async showDetails(index) {
            let id = this.vouchersData[index].ref_id;
            if (this.$store.state.increase.currentVoucherId == id) return;
            await this.$store.dispatch("increase/getDetails", { id });
        },
        /**
         * Hiển thị form sửa
         * @param {*} index index tài sản
         * Author: vtahoang (25/08/2023)
         */
        async showEditForm(index) {
            let voucher = this.vouchersData[index];
            if (this.$store.state.increase.currentVoucherId != voucher.ref_id) {
                await this.showDetails(index);
            }

            this.$store.dispatch("voucherForm/openEditForm", voucher);
        },
        /**
         * Xoá chứng từ
         * @param {*} index index chứng từ
         * Author: vtahoang (27/08/2023)
         */
        async deleteVoucher() {
            this.loadingOverlay = true;
            let voucher = this.deleteTarget;
            try {
                await this.$_MISAApi.increaseVoucherApi.deleteVoucher(voucher.ref_id);
                // Hiện toast message
                this.$store.dispatch("increase/showSuccessToast", "delete");

                // Xoá danh sách đã chọn
                this.clearSelectedItems();
                // Lấy lại danh sách chứng từ
                await this.getVouchers();
            } catch (error) {
                console.log("deleteVoucher ~ error:", error);
            } finally {
                this.loadingOverlay = false;
            }
        },
        /**
         * Cập nhật danh sách chứng từ đã chọn
         * @param {*} selectedList Danh sách chọn
         * Author: vtahoang (29/08/2023)
         */
        updateSelectedVouchers(selectedList) {
            this.$store.commit("increase/updateSelectedVouchers", selectedList);
        },
        /**
         * Lấy danh sách chứng từ
         * Author: vtahoang (27/08/2023)
         */
        async getVouchers() {
            await this.$store.dispatch("increase/getVouchers", {
                search: this.searchValue,
                pageSize: this.pageSize,
                pageIndex: this.pageIndex,
            });
        },
        /**
         * Xoá danh sách đã chọn
         * Author: vtahoang (27/08/2023)
         */
        clearSelectedItems() {
            this.$store.commit("increase/setSelectedVouchers", {});
            this.$refs.firstTable.clearSelectedItems();
        },
        /**
         * Xoá nhiều chứng từ
         * Author: vtahoang (27/08/2023)
         */
        async deleteManyVoucher() {
            this.loadingOverlay = true;
            let seledtedIds = Object.keys(this.$store.state.increase.selectedVouchers);

            seledtedIds = seledtedIds.filter(
                (id) => this.$store.state.increase.selectedVouchers[id]
            );

            try {
                await this.$_MISAApi.increaseVoucherApi.deleteManyVoucher(seledtedIds);
                this.$store.dispatch("increase/showSuccessToast", "delete");

                this.clearSelectedItems();

                // Lấy lại danh sách chứng từ
                this.getVouchers();
            } catch (error) {
                console.log("deleteManyVoucher ~ error:", error);
            } finally {
                this.loadingOverlay = false;
            }
        },
        /**
         * Hiển thị dialog xác nhận xoá
         * @param {*} index index chứng từ
         * Author: vtahoang (27/08/2023)
         */
        showDeleteConfirmDialog(index) {
            let voucher = this.vouchersData[index];

            this.dialogMessage = this.$_MISAResources.dialog.delete.deleteVoucherMessage(
                voucher.ref_no
            );
            this.showDeleteConfirm = true;

            this.deleteTarget = voucher;
        },
        /**
         * Hiển thị dialog xác nhận xoá nhiều chứng từ
         * Author: vtahoang (27/08/2023)
         */
        showDeleteManyConfirmDialog() {
            let count = this.countSelectedVouchers;

            count = count.toString();
            if (count.length == 1) count = "0" + count;

            this.dialogMessage =
                this.$_MISAResources.dialog.delete.deleteManyVouchersMessage(count);
            this.showDeleteManyConfirm = true;
        },
        /**
         * Sự kiện key down
         * @param {*} event
         * Author: vtahoang (05/09/2023)
         */
        keyDownEvent(event) {
            // ctrl d để thêm mới chứng từ
            if (event.ctrlKey && event.key == "d") {
                event.preventDefault();
                this.$store.dispatch("voucherForm/showForm", this.$_MISAEnums.formMode.add);
            }
        },
        async refreshData() {
            await this.getVouchers();

            this.clearSelectedItems();
        },
    },
    updated() {
        this.$refs.firstTable.updateBodyMaxheight();
        this.$refs.secondTable.updateBodyMaxheight();
    },
    watch: {
        /**
         * chọn kiểu layout
         * @param {*} value
         * Author: vtahoang (29/08/2023)
         */
        layoutOption(value) {
            // thay đổi chiều cao bảng 2
            if (value == this.$_MISAEnums.layoutOptions.oneTable) {
                this.secondTableHeight = 0;
            } else {
                this.secondTableHeight = 300;
            }
        },
        /**
         * Cập nhật bảng theo giá trị tìm kiếm
         * Author: vtahoang (29/08/2023)
         */
        async searchValue() {
            this.pageIndex = 1;
            await this.getVouchers();
        },
        async pageIndex() {
            await this.getVouchers();
        },
        async pageSize() {
            this.pageIndex = 1;
            await this.getVouchers();
        },
    },
    async mounted() {
        // Lấy danh sách voucher
        await this.getVouchers();

        // Lấy danh sách nguồn hình thành
        await this.$store.dispatch("increase/getBudgetCategory", {});

        window.addEventListener("mouseup", () => {
            window.removeEventListener("mousemove", this.changeSizeTable);
        });
    },
    computed: {
        vouchersData() {
            return this.$store.state.increase.vouchers;
        },
        /**
         * Hàng hiển thị chi tiết
         * Author: vtahoang (29/08/2023)
         */
        selectedRow() {
            let currentId = this.$store.state.increase.currentVoucherId;
            let index = this.vouchersData.findIndex((item) => item.ref_id == currentId);

            return index;
        },
        /**
         * Số chứng từ đã chọn
         * Author: vtahoang (27/08/2023)
         */
        countSelectedVouchers() {
            return Object.values(this.$store.state.increase.selectedVouchers).filter((e) => e)
                .length;
        },
    },
};
</script>
