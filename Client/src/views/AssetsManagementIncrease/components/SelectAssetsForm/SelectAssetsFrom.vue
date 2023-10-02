<template>
    <div
        class="selectassetsform__container"
        v-focusloop="0"
        @keydown.stop="keyDownEvent($event)"
        tabindex="-1"
        v-autofocus
    >
        <div class="container__form">
            <div class="form__header">
                <div class="header__title">
                    {{ $_MISAResources.assetsManagementIncrease.selectAssetsForm.title }}
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
                <div class="body__toolbar">
                    <div class="toolbar__search">
                        <MISASearch
                            :placeholder="
                                $_MISAResources.assetsManagementIncrease.selectAssetsForm
                                    .searchPlaceholder
                            "
                            v-model="searchValue"
                            :tabindex="0"
                        ></MISASearch>
                    </div>
                    <div class="toolbar__total-selected-records" v-if="selectedCount">
                        <div
                            class="total-selected-records__number"
                            v-html="
                                $_MISAResources.assetsManagement.toolbar.totalSelectedRecords(
                                    selectedCount
                                )
                            "
                        ></div>
                        <div class="total-selected-records__clear" @click="clearSelectedItems">
                            {{ $_MISAResources.assetsManagement.toolbar.clearSelected }}
                        </div>
                    </div>
                </div>
                <div class="body__table">
                    <Table
                        :showFooter="false"
                        :checkBoxKey="'fixed_asset_id'"
                        :columns="$store.state.selectAssetsForm.tableColumns"
                        :tableData="tableData"
                        :isLoading="tableLoading"
                        :selectedList="$store.state.selectAssetsForm.selectedList"
                        ref="table"
                        :clickRowToSelect="true"
                        @update-selected-list="updateSelectedList"
                        :offset="pageSize * (pageIndex - 1)"
                        @update-columns="
                            (columns) => {
                                $store.dispatch('selectAssetsForm/updateTableColumns', columns);
                            }
                        "
                    >
                    </Table>
                </div>
                <div class="body__paging">
                    <p
                        class="paging__totalrecords"
                        v-html="$_MISAResources.assetsManagement.table.totalRecords(totalRecords)"
                    ></p>
                    <MISAPaging
                        :totalRecords="totalRecords"
                        v-model:recordPerPage="pageSize"
                        v-model:currentPage="pageIndex"
                    />
                </div>
            </div>
            <div class="form__footer">
                <MISAToolTip text="ESC">
                    <template #content>
                        <div class="footer__btn footer__btn--close">
                            <MISAButton
                                type="sub"
                                :text="$_MISAResources.assetsManagementIncrease.form.cancelBtn"
                                @click-btn="closeForm"
                                :tabindex="2"
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
                                :tabindex="1"
                            ></MISAButton>
                        </div>
                    </template>
                </MISAToolTip>
            </div>
        </div>
        <Teleport to="#app"> <MISALoadingOverlay :show="loadingOverlay" /> </Teleport>
    </div>
</template>

<style scoped>
@import url(./SelectAssetsFrom.css);
</style>

<script>
import MISASearch from "@/components/base/MISASearch/MISASearch.vue";
import Table from "../Table/Table.vue";
import MISAPaging from "@/components/base/MISAPaging/MISAPaging.vue";
import MISALoadingOverlay from "@/components/base/MISALoadingOverlay/MISALoadingOverlay.vue";

export default {
    name: "SelectAssetsFrom",
    components: {
        MISASearch,
        Table,
        MISAPaging,
        MISALoadingOverlay,
    },
    props: {},
    data() {
        return {
            // Tổng số bản ghi
            totalRecords: 0,
            // Số bản ghi 1 trang
            pageSize: 20,
            // Trang hiện tại
            pageIndex: 1,
            // Giá trị tìm kiếm
            searchValue: "",
            // Dữ liệu bảng
            tableData: [],
            // Loading bảng
            tableLoading: false,
            // Danh sách tài sản đã chọn
            selectedList: {},
            // Loading overlay
            loadingOverlay: false,
        };
    },
    methods: {
        /**
         * Đóng form
         * Author: vtahoang (17/08/2023)
         */
        closeForm() {
            this.$store.commit("selectAssetsForm/hideForm");
            this.$emit("close-form");
        },
        /**
         * Lấy dữ liệu tài sản
         * Author: vtahoang (21/08/2023)
         */
        async getAssetsList() {
            this.tableLoading = true;
            this.tableData = [];
            try {
                // Lấy danh sách tài sản chưa ghi tăng
                let data = await this.$_MISAApi.fixedAssetApi.getAssetsUnIncrease(
                    this.searchValue,
                    null,
                    null,
                    "fixed_asset_code",
                    "ASC",
                    this.pageIndex,
                    this.pageSize
                );

                // let data = await this.$_MISAApi.fixedAssetApi.getAll(
                //     this.searchValue,
                //     null,
                //     null,
                //     null,
                //     null,
                //     this.pageIndex,
                //     this.pageSize
                // );
                this.tableData = data.assets;
                this.totalRecords = data.totalRecords;
            } catch (error) {
                console.log("getAssetsList ~ error:", error);
            } finally {
                this.tableLoading = false;
            }
        },
        /**
         * Lưu form
         * Author: vtahoang (21/08/2023)
         */
        async saveForm() {
            this.loadingOverlay = true;

            try {
                this.$store.dispatch(
                    "voucherForm/setNewSelectedList",
                    this.$store.state.selectAssetsForm.assetsList
                );

                this.closeForm();
            } catch (error) {
                console.log("saveForm ~ error:", error);
            } finally {
                this.loadingOverlay = false;
            }
        },
        /**
         * Xóa danh sách tài sản đã chọn
         * Author: vtahoang (21/08/2023)
         */
        clearSelectedItems() {
            this.$store.dispatch("selectAssetsForm/clearSelectedList");
            let ids = Object.keys(this.$store.state.selectAssetsForm.selectedList).filter(
                (e) => this.$store.state.selectAssetsForm.selectedList[e]
            );
            this.$refs.table.clearSelectedItems(ids);
        },
        /**
         * Cập nhật danh sách tài sản đã chọn
         * @param {*} selectedList
         * Author: vtahoang (22/08/2023)
         */
        updateSelectedList(selectedList) {
            this.$store.commit("selectAssetsForm/updateSelectedList", selectedList);

            let allSelected = this.$store.state.selectAssetsForm.selectedList;
            //Cập nhật danh sách tài sản trong store
            let selectedAssetsListId = Object.keys(allSelected).filter((e) => allSelected[e]);
            let selectedAssetsList = this.tableData.filter((e) =>
                selectedAssetsListId.includes(e.fixed_asset_id)
            );
            this.$store.commit("selectAssetsForm/pushAssetsList", selectedAssetsList);

            let unSelectedAssetsList = Object.keys(allSelected).filter((e) => !allSelected[e]);
            this.$store.commit("selectAssetsForm/removeAssetsList", unSelectedAssetsList);
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
                this.saveForm();
            }
        },
    },
    async mounted() {
        try {
            // Lấy danh sách tài sản đã chọn trước đó
            let selectedList = this.$store.getters["voucherForm/getListIds"];

            this.$store.commit(
                "selectAssetsForm/setAssetsList",
                this.$store.state.voucherForm.assetsList
            );
            this.$store.commit("selectAssetsForm/setSelectedList", selectedList);

            await this.getAssetsList();
        } catch (error) {
            console.log("mounted ~ error:", error);
        }
    },
    watch: {
        /**
         * Cập nhật dữ liệu khi thay đổi số bản ghi 1 trang
         * Author: vtahoang (21/08/2023)
         */
        async pageSize() {
            this.pageIndex = 1;
            await this.getAssetsList();
        },
        /**
         * Cập nhật dữ liệu khi thay đổi số bản ghi 1 trang
         * Author: vtahoang (21/08/2023)
         */
        async pageIndex() {
            await this.getAssetsList();
        },
        /**
         * Cập nhật dữ liệu khi thay đổi số bản ghi 1 trang
         * Author: vtahoang (21/08/2023)
         */
        async searchValue() {
            this.pageIndex = 1;
            await this.getAssetsList();
        },
    },
    computed: {
        /**
         * Số bản ghi được chọn
         * Author: vtahoang (27/08/2023)
         */
        selectedCount() {
            let ids = Object.keys(this.$store.state.selectAssetsForm.selectedList).filter(
                (e) => this.$store.state.selectAssetsForm.selectedList[e]
            );

            let originalIds = this.$store.state.voucherForm.originalList.map(
                (e) => e.fixed_asset_id
            );

            ids = ids.filter((e) => !originalIds.includes(e));

            return ids.length;
        },
    },
};
</script>
