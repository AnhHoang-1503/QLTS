<template>
    <div
        class="container"
        v-focusloop="0"
        @keydown.stop="keyDownEvent($event)"
        v-autofocus
        tabindex="-1"
    >
        <div class="container__form">
            <div class="form__header">
                <div class="header__title">
                    {{ $store.state.voucherForm.formTitle }}
                </div>
                <MISAToolTip text="Esc">
                    <template #content>
                        <div class="header__closebtn" @click="closeForm">
                            <MISAIcon icon="close"></MISAIcon>
                        </div>
                    </template>
                </MISAToolTip>
            </div>
            <div class="form__body">
                <div class="body__infor">
                    <div class="body__title">
                        {{ $_MISAResources.assetsManagementIncrease.form.inforTitle }}
                    </div>
                    <div class="body__row">
                        <div class="row__item">
                            <MISATextfield
                                :label="$_MISAResources.assetsManagementIncrease.form.voucherCode"
                                :required="true"
                                :placeholder="
                                    $_MISAResources.assetsManagementIncrease.form
                                        .voucherCodePlaceholer
                                "
                                v-model="formData.ref_no"
                                ref="ref_no"
                                :tabindex="0"
                                :maxLength="50"
                                v-model:modelErrormsg="errorMessageCode"
                            />
                        </div>
                        <div class="row__item">
                            <MISADatePicker
                                :label="
                                    $_MISAResources.assetsManagementIncrease.form.startUsingDate
                                "
                                icon="calendar"
                                :required="true"
                                v-model="formData.ref_date"
                                ref="ref_date"
                                :tabindex="1"
                            ></MISADatePicker>
                        </div>
                        <div class="row__item">
                            <MISADatePicker
                                :label="$_MISAResources.assetsManagementIncrease.form.increaseDate"
                                icon="calendar"
                                :required="true"
                                v-model="formData.increase_date"
                                ref="increase_date"
                                :tabindex="2"
                                v-model:modelErrormsg="increaseDateError"
                            ></MISADatePicker>
                        </div>
                    </div>
                    <div class="body__row">
                        <div class="row__item">
                            <MISATextfield
                                :label="$_MISAResources.assetsManagementIncrease.form.note"
                                v-model="formData.note"
                                :tabindex="3"
                            />
                        </div>
                    </div>
                </div>
                <div class="body__detail">
                    <div class="body__title">
                        {{ $_MISAResources.assetsManagementIncrease.form.detailTitle }}
                    </div>
                    <div class="detail__toolbar">
                        <div class="detail__search">
                            <MISASearch
                                :placeholder="
                                    $_MISAResources.assetsManagementIncrease.form.searchPlaceholder
                                "
                                :tabindex="4"
                                v-model="searchValue"
                            />
                        </div>
                        <div class="detail__selectbtn">
                            <MISAButton
                                type="outline"
                                :text="$_MISAResources.assetsManagementIncrease.form.selectAssets"
                                :style="{
                                    'border-color': '#000',
                                }"
                                @click-btn="
                                    $store.commit('voucherForm/setShowSelectAssetsForm', true)
                                "
                                :tabindex="5"
                            />
                        </div>
                    </div>
                    <div class="detail__table">
                        <Table
                            :showFooter="false"
                            @double-click="editAsset"
                            @edit-row="editAsset"
                            @delete-row="deleteAsset"
                            :toolColumn="{
                                edit: true,
                                delete: true,
                            }"
                            :columns="$store.state.voucherForm.tableColumns"
                            :tableData="tableData"
                            :isLoading="tableLoading"
                            ref="table"
                            :showEmpty="false"
                            :offset="pageSize * (pageIndex - 1)"
                            @update-columns="
                                (columns) =>
                                    $store.dispatch('voucherForm/updateTableColumns', columns)
                            "
                        ></Table>
                    </div>
                    <div class="detail__paging">
                        <p
                            class="paging__totalrecords"
                            v-html="
                                $_MISAResources.assetsManagement.table.totalRecords(totalRecords)
                            "
                        ></p>
                        <MISAPaging
                            :totalRecords="totalRecords"
                            v-model:recordPerPage="pageSize"
                            v-model:currentPage="pageIndex"
                        />
                    </div>
                </div>
            </div>
            <div class="form__footer">
                <MISAToolTip text="Esc">
                    <template #content>
                        <div class="footer__btn footer__btn--close">
                            <MISAButton
                                type="sub"
                                :text="$_MISAResources.assetsManagementIncrease.form.cancelBtn"
                                @click-btn="closeForm"
                                :tabindex="7"
                            ></MISAButton>
                        </div>
                    </template>
                </MISAToolTip>
                <MISAToolTip text="Ctrl + S">
                    <template #content>
                        <div class="footer__btn footer__btn--save">
                            <MISAButton
                                :text="$_MISAResources.assetsManagementIncrease.form.saveBtn"
                                @click-btn="saveForm"
                                :tabindex="6"
                            ></MISAButton>
                        </div>
                    </template>
                </MISAToolTip>
            </div>
        </div>
        <SelectAssetsFrom
            v-if="$store.state.voucherForm.showSelectAssetsForm"
            @close-form="$store.commit('voucherForm/setShowSelectAssetsForm', false)"
        />
        <EditAssetForm
            v-if="$store.state.voucherForm.showEditAssetForm"
            @close-form="$store.commit('voucherForm/setShowEditAssetForm', false)"
        />
        <Teleport to="#app">
            <MISADialog
                :message="errorDialogMessage"
                v-if="showDialog"
                @cancel="showDialog = false"
            >
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.error.close"
                        @click-btn="showDialog = false"
                        :tabindex="1"
                    ></MISAButton>
                </template>
            </MISADialog>
            <MISADialog
                :message="closeAddDialogMessage"
                v-if="closeDialog && $store.state.voucherForm.formMode == $_MISAEnums.formMode.add"
                @cancel="closeDialog = false"
            >
                <template #middle-btn>
                    <MISAButton
                        type="outline"
                        :text="$_MISAResources.dialog.add.btnCancel"
                        @click-btn="closeDialog = false"
                        :style="{ 'border-color': '#afafaf' }"
                        :tabindex="1"
                    ></MISAButton>
                </template>
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.add.btnConfirm"
                        @click-btn="$emit('close-form')"
                        :tabindex="2"
                    ></MISAButton>
                </template>
            </MISADialog>
            <MISADialog
                :message="closeEditDialogMessage"
                v-if="closeDialog && $store.state.voucherForm.formMode == $_MISAEnums.formMode.edit"
                @cancel="closeDialog = false"
            >
                <template #left-btn>
                    <MISAButton
                        type="outline"
                        :text="$_MISAResources.dialog.edit.btnCancel"
                        @click-btn="closeDialog = false"
                        :style="{ 'border-color': '#afafaf' }"
                        :tabindex="1"
                    ></MISAButton>
                </template>
                <template #middle-btn>
                    <MISAButton
                        type="outline"
                        :text="$_MISAResources.dialog.edit.btnUnsave"
                        @click-btn="$emit('close-form')"
                        :style="{
                            'border-color': '#0582a2',
                            color: '#0582a2',
                        }"
                        :tabindex="2"
                    ></MISAButton>
                </template>
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.edit.btnSave"
                        @click-btn="saveForm"
                        :tabindex="3"
                    ></MISAButton>
                </template>
            </MISADialog>
            <MISALoadingOverlay :show="loadingOverlay" />
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./VourcherForm.css);
</style>

<script>
import MISATextfield from "@/components/base/MISATextfield/MISATextfield.vue";
import MISADatePicker from "@/components/base/MISADatePicker/MISADatePicker.vue";
import MISASearch from "@/components/base/MISASearch/MISASearch.vue";
import Table from "../Table/Table.vue";
import MISAPaging from "@/components/base/MISAPaging/MISAPaging.vue";
import SelectAssetsFrom from "../SelectAssetsForm/SelectAssetsFrom.vue";
import EditAssetForm from "../EditAssetForm/EditAssetForm.vue";
import MISADialog from "@/components/base/MISADialog/MISADialog.vue";
import MISALoadingOverlay from "@/components/base/MISALoadingOverlay/MISALoadingOverlay.vue";

export default {
    name: "VourcherForm",
    components: {
        MISATextfield,
        MISADatePicker,
        MISASearch,
        Table,
        MISAPaging,
        SelectAssetsFrom,
        EditAssetForm,
        MISADialog,
        MISALoadingOverlay,
    },
    methods: {
        /**
         * Đóng form
         * Author: vtahoang (17/08/2023)
         */
        closeForm() {
            if (this.formDataChange) {
                this.closeDialog = true;
                return;
            }
            this.$emit("close-form");
        },
        /**
         * Sửa tài sản
         * @param {*} target index tài sản sửa
         * Author: vtahoang (22/08/2023)
         */
        async editAsset(target) {
            this.$store.commit("voucherForm/setShowEditAssetForm", true);
            let asset = this.tableData[target];
            if (asset.budget === undefined) {
                let data = await this.$_MISAApi.fixedAssetApi.getListAssetByIds([
                    asset.fixed_asset_id,
                ]);
                asset = data[0];
            }
            this.$store.dispatch("editAssetForm/setFixedAsset", asset);
        },
        /**
         * Xoá tài sản khỏi bảng
         * @param {*} target index tài sản xoá
         * Author: vtahoang (22/08/2023)
         */
        deleteAsset(target) {
            let id = this.tableData[target].fixed_asset_id;
            this.$store.dispatch("voucherForm/removeFromSelectedList", [id]);
        },
        /** Lưu form
         * Author: vtahoang (06/09/2023)
         */
        async saveForm() {
            // Lấy danh sách input
            let inputList = [];
            let focus = false;
            inputList.push(this.$refs.ref_no);
            inputList.push(this.$refs.ref_date);
            inputList.push(this.$refs.increase_date);

            // Kiểm tra form hợp lệ
            let status = inputList.every((input) => {
                let isValid = input.isValid();
                if (!isValid && !focus) {
                    input.focus();
                    focus = true;
                }
                return isValid;
            });
            if (!status) return;

            // Ngày ghi tăng lớn hơn ngày chứng từ
            let increaseDate = new Date(this.formData.increase_date).setHours(0, 0, 0, 0);
            let refDate = new Date(this.formData.ref_date).setHours(0, 0, 0, 0);
            if (increaseDate < refDate) {
                this.errorDialogMessage =
                    this.$_MISAResources.assetsManagementIncrease.dialog.increaseDateError;

                this.$nextTick(() => {
                    this.increaseDateError =
                        this.$_MISAResources.assetsManagementIncrease.dialog.increaseDateError;
                });
                this.showDialog = true;
                return;
            }

            // Chọn ít nhất 1 tài sản
            if (this.$store.state.voucherForm.assetsList.length <= 0) {
                this.errorDialogMessage =
                    this.$_MISAResources.assetsManagementIncrease.dialog.noAsset;
                this.showDialog = true;
                return;
            }

            // bật loading overlay
            this.loadingOverlay = true;
            try {
                this.$store.commit("voucherForm/updateFormData", this.formData);

                await this.$store.dispatch("voucherForm/submitForm");

                this.$emit("update-table");

                this.$store.commit("voucherForm/hideForm");
            } catch (error) {
                // Lỗi trùng mã chứng từ
                if (error.response.data.ErrorCode == 409) {
                    this.errorMessageCode = error.response.data.UserMessage;
                    console.log(error.response.data.UserMessage);
                }
                if (error.response.data.ErrorCode == 400) {
                    this.errorDialogMessage = error.response.data.UserMessage;
                    this.showDialog = true;
                }
            } finally {
                this.loadingOverlay = false;
            }
        },
        /**
         * Sự kiện keydown
         * @param {*} event
         * Author: vtahoang (05/09/2023)
         */
        keyDownEvent(event) {
            if (event.key == "Escape") {
                event.preventDefault();
                this.closeForm();
            }
            if (event.ctrlKey && event.key == "s") {
                event.preventDefault();
                if (
                    this.$store.state.voucherForm.showSelectAssetsForm ||
                    this.$store.state.voucherForm.showEditAssetForm
                )
                    return;
                this.saveForm();
            }
        },
    },
    data() {
        return {
            // Loading bảng
            tableLoading: false,
            // Tổng số bản ghi
            totalRecords: 0,
            // Số bản ghi 1 trang
            pageSize: 20,
            // Trang hiện tại
            pageIndex: 1,
            // Giá trị tìm kiếm
            searchValue: "",
            // Hiển thị dialog
            showDialog: false,
            // Nội dung dialog
            errorDialogMessage: "",
            // Lỗi trùng mã chứng từ
            errorMessageCode: "",
            // Dữ liệu form thay đổi
            formDataChange: false,
            // Dialog đóng
            closeDialog: false,
            // Nội dung dialog đóng form thêm mới
            closeAddDialogMessage: this.$_MISAResources.assetsManagementIncrease.dialog.closeAdd,
            // Nội dung dialog đóng form sửa
            closeEditDialogMessage: this.$_MISAResources.assetsManagementIncrease.dialog.closeEdit,
            // Loading overlay
            loadingOverlay: false,
            // Lỗi ngày ghi tăng
            increaseDateError: null,
        };
    },
    computed: {
        /**
         * Dữ liệu bảng
         * Author: vtahoang (29/08/2023)
         */
        tableData() {
            let tableData = JSON.parse(JSON.stringify(this.$store.state.voucherForm.assetsList));
            // tìm kiếm
            tableData = tableData.filter((asset) => {
                return (
                    asset.fixed_asset_code.toLowerCase().includes(this.searchValue.toLowerCase()) ||
                    asset.fixed_asset_name.toLowerCase().includes(this.searchValue.toLowerCase())
                );
            });

            this.totalRecords = tableData.length;

            // phân trang
            tableData = tableData.slice(
                (this.pageIndex - 1) * this.pageSize,
                this.pageIndex * this.pageSize
            );
            return tableData;
        },
        /**
         * Dữ liệu form
         * Author: vtahoang (29/08/2023)
         */
        formData() {
            return this.$store.state.voucherForm.formData;
        },
    },
    watch: {
        /**
         * Kiểm tra dữ liệu thay đổi
         * Author: vtahoang (05/09/2023)
         */
        formData: {
            handler: function () {
                this.formDataChange = true;
            },
            deep: true,
        },
        /**
         * Kiểm tra dữ liệu thay đổi
         * Author: vtahoang (05/09/2023)
         */
        tableData: {
            handler: function () {
                this.formDataChange = true;
            },
            deep: true,
        },
    },
};
</script>
