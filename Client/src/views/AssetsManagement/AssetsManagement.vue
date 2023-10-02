<template>
    <div
        class="h-container"
        @mousemove="selectedRow = 0"
        @keydown="keyDownEvent($event)"
        @keyup="keyUpEvent($event)"
        tabindex="-1"
        v-focusloop
        v-autofocus
        ref="container"
    >
        <!-- Toolbar begin -->
        <div class="h-page__toolbar">
            <div class="h-toolbar__left">
                <div class="h-toolbar__search">
                    <MISAToolTip :text="$_MISAResources.assetsManagement.toolbar.searchTooltip">
                        <template #content>
                            <MISASearch
                                :placeholder="
                                    $_MISAResources.assetsManagement.toolbar.searchPlaceholder
                                "
                                v-model="searchValue"
                                :tabindex="1"
                                :isLoadingSearch="isLoadingSearch"
                            ></MISASearch>
                        </template>
                    </MISAToolTip>
                </div>
                <div class="h-toolbar__assettype">
                    <MISADropdown
                        :placeholder="
                            $_MISAResources.assetsManagement.toolbar.assetCategoryPlaceholder
                        "
                        :dataList="
                            this.$_MISAResources.fixed_asset_category.map(
                                (item) => item.fixed_asset_category_name
                            )
                        "
                        :iconLeft="'filter'"
                        :iconRight="'expand'"
                        v-model="typeValue"
                        :tabindex="2"
                    ></MISADropdown>
                </div>
                <div class="h-toolbar__department">
                    <MISADropdown
                        :placeholder="
                            $_MISAResources.assetsManagement.toolbar.departmentPlaceholder
                        "
                        :dataList="
                            this.$_MISAResources.department.map((item) => item.department_name)
                        "
                        :iconLeft="'filter'"
                        :iconRight="'expand'"
                        v-model="departmentValue"
                        :tabindex="3"
                    ></MISADropdown>
                </div>
                <div
                    class="h-toolbar__total-selected-records"
                    v-if="Object.values(allSelectedItems).filter((e) => e).length"
                >
                    <div
                        class="h-total-selected-records__number"
                        v-html="
                            $_MISAResources.assetsManagement.toolbar.totalSelectedRecords(
                                Object.values(allSelectedItems).filter((e) => e).length
                            )
                        "
                    ></div>
                    <div class="h-total-selected-records__clear" @click="clearAllSelectedItems">
                        {{ $_MISAResources.assetsManagement.toolbar.clearSelected }}
                    </div>
                </div>
            </div>
            <div class="h-toolbar__right">
                <div class="h-toolbar__btn--delete">
                    <MISAToolTip :text="$_MISAResources.assetsManagement.toolbar.deleteBtnTooltip">
                        <template #content>
                            <MISAButton
                                type="sub"
                                :disabled="disabledDeleteManyBtn"
                                icon="delete"
                                @click-btn="showDeleteManyDialog = true"
                                :tabindex="6"
                                :loading="isLoadingDeleteMany"
                            >
                            </MISAButton>
                        </template>
                    </MISAToolTip>
                </div>
                <div class="h-toolbar__btn--excel">
                    <MISAExcel
                        @export-to-excel="exportToExcel"
                        @import-success="onImportSuccess"
                        v-model:showExcelBox="showExcelBox"
                        v-model:isLoadingExcel="isLoadingExcel"
                    >
                        <template #btn>
                            <MISAToolTip
                                :text="$_MISAResources.assetsManagement.toolbar.excelBtnTooltip"
                            >
                                <template #content>
                                    <MISAButton
                                        type="sub"
                                        :icon="'excel'"
                                        :loading="isLoadingExcel"
                                        @click-btn="exportToExcel"
                                        :tabindex="5"
                                    >
                                    </MISAButton>
                                </template>
                            </MISAToolTip>
                        </template>
                    </MISAExcel>
                </div>
                <div class="h-toolbar__btn--add">
                    <MISAToolTip text="Ctrl + B">
                        <template #content>
                            <MISAButton
                                :icon="'add--white'"
                                @click="showForm()"
                                :text="$_MISAResources.assetsManagement.toolbar.addBtnText"
                                :tabindex="4"
                            ></MISAButton>
                        </template>
                    </MISAToolTip>
                </div>
            </div>
        </div>
        <!-- Toolbar end -->

        <!-- Table begin -->
        <div class="h-page__table" :class="displayTextSelection ? 'unselectable' : ''">
            <div class="h-table__main" ref="tableMain">
                <table>
                    <thead>
                        <th class="h-column__checkbox" @click="selectAll">
                            <MISACheckBox :checked="selectAllStatus" ref="selectAll"></MISACheckBox>
                        </th>
                        <th class="h-column__index">
                            <div class="h-column__name">
                                <MISAToolTip
                                    :text="$_MISAResources.assetsManagement.table.indexTooltip"
                                >
                                    <template #content>{{
                                        $_MISAResources.assetsManagement.table.index
                                    }}</template>
                                </MISAToolTip>
                            </div>
                        </th>
                        <th class="h-column__id">
                            <div class="h-column__name">
                                <MISASort
                                    v-model:sortField="sortField"
                                    v-model:sortType="sortType"
                                    value="fixed_asset_code"
                                >
                                    <template #content>
                                        <span>
                                            {{ $_MISAResources.assetsManagement.table.assetCode }}
                                        </span>
                                    </template>
                                </MISASort>
                            </div>
                        </th>
                        <th class="h-column__name">
                            <div class="h-column__name">
                                <MISASort
                                    v-model:sortField="sortField"
                                    v-model:sortType="sortType"
                                    value="fixed_asset_name"
                                >
                                    <template #content>
                                        <span>
                                            {{ $_MISAResources.assetsManagement.table.assetName }}
                                        </span>
                                    </template>
                                </MISASort>
                            </div>
                        </th>
                        <th class="h-column__type">
                            <div class="h-column__name">
                                <MISASort
                                    v-model:sortField="sortField"
                                    v-model:sortType="sortType"
                                    value="fixed_asset_category_name"
                                >
                                    <template #content>
                                        <span>
                                            {{
                                                $_MISAResources.assetsManagement.table
                                                    .assetCategoryName
                                            }}
                                        </span>
                                    </template>
                                </MISASort>
                            </div>
                        </th>
                        <th class="h-column__department">
                            <div class="h-column__name">
                                <MISASort
                                    v-model:sortField="sortField"
                                    v-model:sortType="sortType"
                                    value="department_name"
                                >
                                    <template #content>
                                        <span>
                                            {{
                                                $_MISAResources.assetsManagement.table
                                                    .departmentName
                                            }}
                                        </span>
                                    </template>
                                </MISASort>
                            </div>
                        </th>
                        <th class="h-column__amount">
                            <div class="h-column__name">
                                <MISASort
                                    v-model:sortField="sortField"
                                    v-model:sortType="sortType"
                                    value="quantity"
                                >
                                    <template #content>
                                        <span>
                                            {{ $_MISAResources.assetsManagement.table.quantity }}
                                        </span>
                                    </template>
                                </MISASort>
                            </div>
                        </th>
                        <th class="h-column__originalprice">
                            <div class="h-column__name">
                                <MISASort
                                    v-model:sortField="sortField"
                                    v-model:sortType="sortType"
                                    value="cost"
                                >
                                    <template #content>
                                        <span>
                                            {{ $_MISAResources.assetsManagement.table.cost }}
                                        </span>
                                    </template>
                                </MISASort>
                            </div>
                        </th>
                        <th class="h-column__accumulated">
                            <div class="h-column__name">
                                <MISAToolTip
                                    :text="
                                        $_MISAResources.assetsManagement.table
                                            .accumulatedDepreciationTooltip
                                    "
                                >
                                    <template #content
                                        ><MISASort
                                            v-model:sortField="sortField"
                                            v-model:sortType="sortType"
                                            value="accumulated_depreciation"
                                        >
                                            <template #content>
                                                <span>
                                                    {{
                                                        $_MISAResources.assetsManagement.table
                                                            .accumulatedDepreciation
                                                    }}
                                                </span>
                                            </template>
                                        </MISASort></template
                                    >
                                </MISAToolTip>
                            </div>
                        </th>
                        <th class="h-column__remaining">
                            <div class="h-column__name">
                                <MISASort
                                    v-model:sortField="sortField"
                                    v-model:sortType="sortType"
                                    value="remaining_value"
                                >
                                    <template #content>
                                        <span>
                                            {{
                                                $_MISAResources.assetsManagement.table
                                                    .remainingValue
                                            }}
                                        </span>
                                    </template>
                                </MISASort>
                            </div>
                        </th>
                        <th class="h-column__tool">
                            {{ $_MISAResources.assetsManagement.table.tool }}
                        </th>
                    </thead>
                    <tbody>
                        <tr
                            v-for="(item, index) in assetsList"
                            :key="item.fixed_asset_id"
                            @dblclick="showForm(item.fixed_asset_id)"
                            @click="selectItem(item.fixed_asset_id, $event)"
                            @contextmenu="showContextMenu(item.fixed_asset_id, $event)"
                            :class="index + 1 == selectedRow ? 'h-row--selected' : ''"
                        >
                            <td class="h-column__checkbox" @dblclick="handler.stopPropagation">
                                <MISACheckBox
                                    :checked="allSelectedItems[item.fixed_asset_id]"
                                ></MISACheckBox>
                            </td>
                            <td class="h-column__index">
                                {{ index + 1 + (currentPage - 1) * recordPerPage }}
                            </td>
                            <td class="h-column__id">
                                <MISAToolTip
                                    :text="item.fixed_asset_code"
                                    :show="item.fixed_asset_code.length > 8"
                                >
                                    <template #content>
                                        {{ handler.textHandler(item.fixed_asset_code, 8) }}
                                    </template>
                                </MISAToolTip>
                            </td>
                            <td class="h-column__name">
                                <MISAToolTip
                                    :text="item.fixed_asset_name"
                                    :show="item.fixed_asset_name.length > 20"
                                >
                                    <template #content>
                                        {{ handler.textHandler(item.fixed_asset_name, 20) }}
                                    </template>
                                </MISAToolTip>
                            </td>
                            <td class="h-column__type">
                                {{ item.fixed_asset_category_name }}
                            </td>
                            <td class="h-column__department">{{ item.department_name }}</td>
                            <td class="h-column__amount">
                                {{ handler.numberHandler(item.quantity) }}
                            </td>
                            <td class="h-column__originalprice">
                                {{ handler.numberHandler(item.cost) }}
                            </td>
                            <td class="h-column__accumulated">
                                {{ handler.numberHandler(item.accumulated_depreciation) }}
                            </td>
                            <td class="h-column__remaining">
                                {{ handler.numberHandler(item.remaining_value) }}
                            </td>
                            <td
                                class="h-column__tool"
                                @dblclick="handler.stopPropagation"
                                @click="handler.stopPropagation"
                            >
                                <div class="h-tool__block">
                                    <div
                                        class="h-tool__clone"
                                        @click="showForm(item.fixed_asset_id, 'duplicate')"
                                    >
                                        <MISAToolTip
                                            :text="
                                                $_MISAResources.assetsManagement.table
                                                    .duplicateTooltip
                                            "
                                        >
                                            <template #content>
                                                <MISAIcon icon="btn--clone"></MISAIcon>
                                            </template>
                                        </MISAToolTip>
                                    </div>
                                    <div
                                        class="h-tool__edit"
                                        @click="showForm(item.fixed_asset_id)"
                                    >
                                        <MISAToolTip
                                            :text="
                                                $_MISAResources.assetsManagement.table.editTooltip
                                            "
                                        >
                                            <template #content>
                                                <MISAIcon icon="btn--edit"></MISAIcon>
                                            </template>
                                        </MISAToolTip>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div
                    v-if="isLoadingAssets"
                    class="h-table__loading"
                    :style="{ height: heightOfEmptyAndLoading }"
                >
                    <MISAIcon icon="loading"></MISAIcon>
                </div>
                <div
                    v-if="isEmpty()"
                    class="h-table__empty"
                    :style="{ height: heightOfEmptyAndLoading }"
                >
                    <MISAIcon icon="emptyBox"></MISAIcon>
                    <p class="h-empty__text">
                        {{ $_MISAResources.assetsManagement.table.empty }}
                    </p>
                </div>
            </div>
            <div class="h-table__footer">
                <div class="h-footer__left">
                    <p
                        class="h-footer__total"
                        v-html="$_MISAResources.assetsManagement.table.totalRecords(totalRecords)"
                    ></p>
                    <div class="h-footer__paging">
                        <MISAPaging
                            ref="paging"
                            :totalRecords="totalRecords"
                            v-model:recordPerPage="recordPerPage"
                            v-model:currentPage="currentPage"
                        ></MISAPaging>
                    </div>
                </div>
                <div class="h-footer__right">
                    <div class="h-footer__amounttotal">
                        {{ handler.numberHandler(totalAmount) }}
                    </div>
                    <div class="h-footer__originaltotal">
                        {{ handler.numberHandler(totalOriginal) }}
                    </div>
                    <div class="h-footer__accumulatedtotal">
                        {{ handler.numberHandler(totalAccumulated) }}
                    </div>
                    <div class="h-footer__remaintotal">
                        {{ handler.numberHandler(totalRemaining) }}
                    </div>
                </div>
            </div>
        </div>
        <!-- Table end -->
    </div>
    <Teleport to="#app">
        <MISAContextMenu
            v-if="isShowContextMenu"
            :x="contextMenuPosition.x"
            :y="contextMenuPosition.y"
            v-model:show="isShowContextMenu"
            @addAsset="showForm()"
            @editAsset="showForm(contextMenuTarget)"
            @deleteAsset="showDeleteDialog = true"
            @duplicateAsset="showForm(contextMenuTarget, 'duplicate')"
            @click-out-side="hideContextMenu"
            :tool="{
                add: true,
                edit: true,
                delete: true,
                duplicate: true,
            }"
        ></MISAContextMenu>
        <MISAForm
            v-if="formMode != $_MISAEnums.form.formMode.hide"
            :formMode="formMode"
            v-model:dataObject="dataObject"
            @close-form="onCloseForm"
            @save-form="onSaveForm"
        ></MISAForm>
        <MISAToastMessage
            :type="tmType"
            :message="tmMessage"
            :icon="tmType"
            v-if="toastMessageTime > 0"
        ></MISAToastMessage>
        <MISADialog
            :message="deleteDiaglogMessage"
            v-if="showDeleteDialog"
            @cancel="showDeleteDialog = false"
        >
            <template #middle-btn>
                <MISAButton
                    type="sub"
                    :text="$_MISAResources.dialog.delete.btnCancel"
                    @click-btn="showDeleteDialog = false"
                    :style="{ 'border-color': '#afafaf', 'box-shadow': 'none' }"
                    :tabindex="1"
                ></MISAButton>
            </template>
            <template #right-btn>
                <MISAButton
                    :text="$_MISAResources.dialog.delete.btnConfirm"
                    @click-btn="deleteAsset(contextMenuTarget)"
                    :tabindex="2"
                ></MISAButton>
            </template>
        </MISADialog>
        <MISADialog
            :message="deleteManyDiaglogMessage"
            v-if="showDeleteManyDialog"
            @cancel="showDeleteManyDialog = false"
        >
            <template #middle-btn>
                <MISAButton
                    type="sub"
                    :text="$_MISAResources.dialog.delete.btnCancel"
                    @click-btn="showDeleteManyDialog = false"
                    :style="{ 'border-color': '#afafaf', 'box-shadow': 'none' }"
                    :tabindex="1"
                ></MISAButton>
            </template>
            <template #right-btn>
                <MISAButton
                    :text="$_MISAResources.dialog.delete.btnConfirm"
                    @click-btn="deleteManyAsset"
                    :tabindex="2"
                ></MISAButton>
            </template>
        </MISADialog>
        <Teleport to="#app"
            ><MISADialog
                :message="cantDeleteMessage"
                :detailToggleText="[
                    $_MISAResources.assetsManagement.dialog.hideDetail,
                    $_MISAResources.assetsManagement.dialog.showDetail,
                ]"
                :detailData="detailVoucher"
                v-if="showCantDeleteDialog"
                @cancel="showCantDeleteDialog = false"
            >
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.error.accept"
                        @click-btn="showCantDeleteDialog = false"
                        :tabindex="1"
                    ></MISAButton>
                </template>
            </MISADialog>
        </Teleport>
        <MISALoadingOverlay :show="loadingOverlay" />
    </Teleport>
</template>

<style scoped>
@import url(./AssetsManagement.css);
</style>

<script>
// import components
import MISASearch from "@/components/base/MISASearch/MISASearch.vue";
import MISADropdown from "@/components/base/MISADropdown/MISADropdown.vue";
import MISAForm from "@/components/base/MISAForm/MISAForm.vue";
import MISAToastMessage from "@/components/base/MISAToastmessage/MISAToastMessage.vue";
import MISATextfield from "@/components/base/MISATextfield/MISATextfield.vue";
import MISAPaging from "@/components/base/MISAPaging/MISAPaging.vue";
import MISAContextMenu from "@/components/base/MISAContextMenu/MISAContextMenu.vue";
import MISADatePicker from "@/components/base/MISADatePicker/MISADatePicker.vue";
import MISACheckBox from "@/components/base/MISACheckBox/MISACheckBox.vue";
import MISADialog from "@/components/base/MISADialog/MISADialog.vue";
import MISASort from "@/components/base/MISASort/MISASort.vue";
import MISAExcel from "@/components/base/MISAExcel/MISAExcel.vue";

export default {
    data() {
        return {
            assetsList: [], //danh sách tài sản
            totalAmount: 0, // tổng số lượng
            totalOriginal: 0, // tổng nguyên giá
            totalAccumulated: 0, // tổng hao mòn lũy kế
            totalRemaining: 0, // tổng giá trị còn lại
            formMode: 0, // trạng thái form
            dataObject: {}, // dữ liệu truyền vào form
            // thời gian hiện toast message
            toastMessageTime: 0,
            // đếm ngược thời gian hiện toast message
            countDownToastMessage: null,
            // Nội dung toast message
            tmMessage: "",
            // Kiểu toast message
            tmType: "success",
            //số bản ghi 1 trang
            recordPerPage: 20,
            //trang hiện tại
            currentPage: 1,
            //trạng thái loading
            isLoadingAssets: true,
            //giá trị tìm kiếm
            searchValue: "",
            //lọc theo loại tài sản
            typeValue: "",
            //lọc theo bộ phận sử dụng
            departmentValue: "",
            //tổng số bản ghi hiện ra
            totalRecords: 0,
            //danh sách tài sản được chọn của trang hiện tại
            selectedItems: {},
            // danh sách bản ghi được chọn
            allSelectedItems: {},
            // dòng được chọn
            selectedRow: 0,
            //ẩn hiện context menu
            isShowContextMenu: false,
            //vị trí hiện context menu
            contextMenuPosition: {
                x: 0,
                y: 0,
            },
            // context menu item
            contextMenuTarget: null,
            // item được chọn trước đó
            preselectedItem: -1,
            // huỷ bôi đen text khi ấn shift click
            displayTextSelection: false,
            // hiển thị dialog xoá
            showDeleteDialog: false,
            // nội dung dialog xoá
            deleteDiaglogMessage: "",
            // hiển thị dialog xoá nhiều
            showDeleteManyDialog: false,
            // nội dung dialog xoá nhiều
            deleteManyDiaglogMessage: "",
            // trường sắp xếp theo
            sortField: null,
            // kiểu sắp xếp
            sortType: null,
            // Hiển thị excel box
            showExcelBox: false,
            // Loading search
            isLoadingSearch: false,
            // loading excel
            isLoadingExcel: false,
            // loading xoá nhiều
            isLoadingDeleteMany: false,
            // hiện loading overlay
            loadingOverlay: false,
            // Hiển thị dialog không thể xoá
            showCantDeleteDialog: false,
            // Nội dung dialog không thể xoá
            cantDeleteMessage: "",
            // Chi tiết chứng từ
            detailVoucher: [],
        };
    },
    methods: {
        /**
         * Ẩn hiện form
         * @param {*} fixed_asset_id
         * @param {*} type mặc định = "" là edit, "duplicate" là nhân bản
         * Author: vtahoang - (23/06/2023)
         */
        showForm(fixed_asset_id, type = "") {
            try {
                // có fixed_asset_id thì là edit hoặc nhân bản, không có thì là add
                if (fixed_asset_id) {
                    if (type == "duplicate") {
                        this.formMode = 3;
                    } else this.formMode = 2;
                    this.assetsList.forEach((item) => {
                        if (item.fixed_asset_id == fixed_asset_id) {
                            this.dataObject = { ...item };
                        }
                    });
                } else {
                    this.formMode = 1;
                    this.dataObject = undefined;
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * Update tổng
         * Author: vtahoang - (29/06/2023)
         */
        updateTotal() {
            try {
                let totalAmount = 0;
                let totalOriginal = 0;
                let totalAccumulated = 0;
                let totalRemaining = 0;
                this.assetsList.forEach((asset) => {
                    totalAmount += asset.quantity;
                    totalOriginal += asset.cost;
                    totalAccumulated += asset.accumulated_depreciation;
                    totalRemaining += asset.remaining_value;
                });
                this.totalAmount = totalAmount;
                this.totalOriginal = totalOriginal;
                this.totalAccumulated = totalAccumulated;
                this.totalRemaining = totalRemaining;
            } catch (error) {
                console.log(error);
            }
        },
        onCloseForm() {
            this.formMode = 0;
        },
        /**
         * Lưu dữ liệu thành công
         * Author: vtahoang - (13/07/2023)
         */
        async onSaveForm() {
            try {
                await this.assetsShown();
                // ẩn form
                this.formMode = 0;

                // hiện toast message thành công
                this.showTMSuccess();
            } catch (error) {
                console.log("onSaveForm ~ error:", error);
            }
        },
        /**
         * Hiện toast message
         * Author: vtahoang (01/08/2023)
         */
        showTm() {
            try {
                this.toastMessageTime = 3;
                clearInterval(this.countDownToastMessage);
                this.countDownToastMessage = setInterval(() => {
                    this.toastMessageTime -= 1;
                    if (this.toastMessageTime <= 0) {
                        clearInterval(this.countDownToastMessage);
                    }
                }, 1000);
            } catch (error) {
                console.log("showTm ~ error:", error);
            }
        },
        /**
         * Hiện toast message đang phát triển
         * Author: vtahoang - (06/07/2023)
         */
        showTMDeveloping() {
            try {
                this.tmMessage = this.$_MISAResources.toastMessage.developing;
                this.tmType = "warning";
                this.showTm();
            } catch (error) {
                console.log("showTM ~ error:", error);
            }
        },
        /**
         * Hiện toast message thêm thành công
         * Author: vtahoang - (06/07/2023)
         */
        showTMSuccess() {
            try {
                this.tmMessage = this.$_MISAResources.toastMessage.addSuccess;
                this.tmType = "success";
                this.showTm();
            } catch (error) {
                console.log("showTM ~ error:", error);
            }
        },
        /**
         * Hiện toast message lỗi
         * Author: vtahoang - (06/07/2023)
         */
        showTMError() {
            try {
                this.tmMessage = this.$_MISAResources.toastMessage.error;
                this.tmType = "error";
                this.showTm();
            } catch (error) {
                console.log("showTM ~ error:", error);
            }
        },
        /**
         * Hiện toast message xoá thành công
         */
        showTMDeleteSuccess() {
            try {
                this.tmMessage = this.$_MISAResources.toastMessage.deleteSuccess;
                this.tmType = "success";
                this.showTm();
            } catch (error) {
                console.log("showTM ~ error:", error);
            }
        },
        /**
         * Kiểm tra danh sách tài sản hiện ra có rỗng không
         * Author: vtahoang - (09/07/2023)
         */
        isEmpty() {
            try {
                if (!this.isLoadingAssets && this.assetsList.length <= 0) return true;
                return false;
            } catch (error) {
                console.log("isEmpty ~ error:", error);
            }
        },
        /**
         * Hiển thị danh sách tài sản theo giá trị lọc
         * Author: vtahoang - (09/07/2023)
         */
        async assetsShown() {
            try {
                this.isLoadingAssets = true;
                // reset danh sách tài sản
                this.assetsList = [];

                // lấy department id
                let department_id;
                this.$_MISAResources.department.forEach((item) => {
                    if (item.department_name == this.departmentValue) {
                        department_id = item.department_id;
                    }
                });

                // lấy fixed_asset_category_id
                let fixed_asset_category_id;
                this.$_MISAResources.fixed_asset_category.forEach((item) => {
                    if (item.fixed_asset_category_name == this.typeValue) {
                        fixed_asset_category_id = item.fixed_asset_category_id;
                    }
                });

                // lọc theo tìm kiếm, loại, bộ phận, phân trang
                let data = await this.$_MISAApi.fixedAssetApi.getAll(
                    this.searchValue,
                    department_id,
                    fixed_asset_category_id,
                    this.sortField,
                    this.sortType,
                    this.currentPage,
                    this.recordPerPage
                );

                // cập nhật asset list và tổng số bản ghi
                this.assetsList = data.assets;
                this.totalRecords = data.totalRecords;

                this.isLoadingAssets = false;

                //cập nhật danh sách bản ghi được chọn
                this.selectedItems = {};

                this.assetsList.forEach((item) => {
                    this.selectedItems[item.fixed_asset_id] =
                        this.allSelectedItems[item.fixed_asset_id];
                });
            } catch (error) {
                console.log("assetsShown ~ error:", error);
            }
        },
        /**
         * click vào dòng thì select hoặc bỏ select checkbox
         * @param {*} item fixed_asset_id của item
         * Author: vtahoang - (09/07/2023)
         */
        selectItem(item, event) {
            try {
                // nếu nhấn shift thì select nhiều
                if (event.shiftKey) {
                    let keys = Object.keys(this.selectedItems);
                    let start = keys.indexOf(item);
                    let end = keys.indexOf(this.preselectedItem);
                    if (start > end) {
                        let temp = start;
                        start = end;
                        end = temp;
                    }
                    for (let i = start; i <= end; i++) {
                        this.selectedItems[keys[i]] = true;
                    }
                } else {
                    this.selectedItems[item] = !this.selectedItems[item];
                    this.preselectedItem = item;
                }
            } catch (error) {
                console.log("selectItem ~ error:", error);
            }
        },
        /**
         * Sự kiện ấn phím
         * Author: vtahoang - (09/07/2023)
         */
        keyDownEvent(event) {
            try {
                switch (event.key) {
                    // di chuyển dòng bằng phím mũi tên
                    case "ArrowDown":
                        if (this.selectedRow < this.assetsList.length) {
                            this.selectedRow += 1;
                        }
                        break;
                    case "ArrowUp":
                        if (this.selectedRow > 1) {
                            this.selectedRow -= 1;
                        }
                        break;
                    // chuyển trang bằng phím mũi tên
                    case "ArrowRight":
                        this.$refs.paging.increase();
                        break;
                    case "ArrowLeft":
                        this.$refs.paging.decrease();
                        break;
                    // chọn dòng bằng enter
                    case "Enter":
                        if (this.selectedRow > 0) {
                            // ctrl enter thì là sửa dòng đó
                            if (event.ctrlKey) {
                                this.showForm(this.assetsList[this.selectedRow - 1].fixed_asset_id);
                            } else {
                                // chọn dòng bằng enter
                                event.preventDefault();
                                this.selectedItems[
                                    this.assetsList[this.selectedRow - 1].fixed_asset_id
                                ] =
                                    !this.selectedItems[
                                        this.assetsList[this.selectedRow - 1].fixed_asset_id
                                    ];
                            }
                        }
                        break;
                    // bỏ text selection khi nhấn shift
                    case "Shift":
                        this.displayTextSelection = true;
                        break;
                    // xoá tài sản bằng nút delete
                    case "Delete":
                        this.contextMenuTarget =
                            this.assetsList[this.selectedRow - 1].fixed_asset_id;
                        this.showDeleteDialog = true;
                        break;
                    // dùng tab thì xoá dòng được chọn
                    case "Tab":
                        this.selectedRow = 0;
                        break;
                    case "b":
                        if (event.ctrlKey) {
                            console.log(1);
                            event.preventDefault();
                            this.showForm();
                        }
                        break;
                }

                // scroll đến dòng được chọn
                this.$refs.tableMain.scrollTop = (this.selectedRow - 18) * 36;
            } catch (error) {
                console.log("keyDownEvent ~ error:", error);
            }
        },
        /**
         * ẩn context menu khi click ra ngoài
         * Author: vtahoang - (12/07/2023)
         */
        hideContextMenu() {
            try {
                this.isShowContextMenu = false;
            } catch (error) {
                console.log("hideContextMenu ~ error:", error);
            }
        },
        /**
         * hiện context menu
         * @param {*} fixed_asset_id fixed_asset_id của item
         * Author: vtahoang - (12/07/2023)
         */
        showContextMenu(fixed_asset_id, event) {
            try {
                event.preventDefault();
                // chặn event click outside
                event.stopPropagation();
                // ẩn excel box
                this.showExcelBox = false;

                // đối tượng đc target
                this.contextMenuTarget = fixed_asset_id;
                // gán vị trí của context menu
                this.contextMenuPosition.x = event.x;

                this.contextMenuPosition.y = event.y;

                this.isShowContextMenu = true;
            } catch (error) {
                console.log("showContextMenu ~ error:", error);
            }
        },

        /**
         * Xoá tài sản
         * @param {*} target tài sản cần xoá
         * Author: vtahoang - (22/07/2023)
         */
        async deleteAsset(target) {
            try {
                this.loadingOverlay = true;

                // ẩn dialog
                this.showDeleteDialog = false;

                // xoá tài sản
                await this.$_MISAApi.fixedAssetApi.deleteAsset(target);

                // hiện toast message xoá thành công
                this.showTMDeleteSuccess();
                this.allSelectedItems[target] = false;
            } catch (error) {
                console.log("deleteAsset ~ error:", error);
                // Hiển thị dialog không thể xoá
                if (error.response.data.ErrorCode == 409) {
                    var data = JSON.parse(error.response.data.DevMessage);

                    this.cantDeleteMessage =
                        this.$_MISAResources.assetsManagement.dialog.cantDeleteAssets(
                            data.assetsCode
                        );

                    this.detailVoucher = data.vouchersCode.map((item) => {
                        return this.$_MISAResources.assetsManagement.dialog.detailVoucher(item);
                    });

                    this.showCantDeleteDialog = true;
                }
            } finally {
                // tắt loading overlay
                this.loadingOverlay = false;
                // cập nhật lại danh sách tài sản
                await this.assetsShown();
            }
        },
        /**
         * Xoá nhiều tài sản
         * Author: vtahoang - (22/07/2023)
         */
        async deleteManyAsset() {
            try {
                // bật loading button và loading overrlay
                this.isLoadingDeleteMany = true;
                this.loadingOverlay = true;

                // ẩn dialog
                this.showDeleteManyDialog = false;

                // lấy danh sách tài sản được chọn
                let selectedList = [];
                for (let key in this.allSelectedItems) {
                    if (this.allSelectedItems[key]) selectedList.push(key);
                }

                // xoá tài sản được chọn
                await this.$_MISAApi.fixedAssetApi.deleteManyAsset(selectedList);

                // cập nhật lại danh sách chọn

                for (let key in this.allSelectedItems) {
                    this.allSelectedItems[key] = false;
                }
                for (let key in this.selectedItems) {
                    this.selectedItems[key] = false;
                }

                // hiện toast message xoá thành công
                this.showTMDeleteSuccess();
            } catch (error) {
                console.log("deleteManyAsset ~ error:", error);
                // Hiển thị dialog không thể xoá
                if (error.response.data.ErrorCode == 409) {
                    var data = JSON.parse(error.response.data.DevMessage);

                    this.cantDeleteMessage =
                        this.$_MISAResources.assetsManagement.dialog.cantDeleteAssets(
                            data.assetsCode
                        );

                    this.detailVoucher = data.vouchersCode.map((item) => {
                        return this.$_MISAResources.assetsManagement.dialog.detailVoucher(item);
                    });

                    this.showCantDeleteDialog = true;
                }
            } finally {
                // tắt loading button và overlay
                this.isLoadingDeleteMany = false;
                this.loadingOverlay = false;
                //cập nhật lại danh sách tài sản
                await this.assetsShown();
            }
        },
        /**
         * Sự kiện thả phím
         * Author: vtahoang - (25/07/2023)
         */
        keyUpEvent(event) {
            try {
                // trả lại text selection khi nhả shift
                if (event.key == "Shift") {
                    this.displayTextSelection = false;
                }
            } catch (error) {
                console.log("keyUpEvent ~ error:", error);
            }
        },
        /**
         * Click vào select all
         * Author: vtahoang - (25/07/2023)
         */
        selectAll() {
            try {
                let selectAllStatus = this.selectAllStatus;
                this.assetsList.forEach((item) => {
                    this.selectedItems[item.fixed_asset_id] = !selectAllStatus;
                });
            } catch (error) {
                console.log("selectAll ~ error:", error);
            }
        },
        /**
         * Xuất dữ liệu ra file excel
         * Author: vtahoang (01/08/2023)
         */
        async exportToExcel() {
            try {
                if (!this.isLoadingExcel) {
                    this.isLoadingExcel = true;
                    // lấy department id
                    let department_id;
                    this.$_MISAResources.department.forEach((item) => {
                        if (item.department_name == this.departmentValue) {
                            department_id = item.department_id;
                        }
                    });

                    // lấy fixed_asset_category_id
                    let fixed_asset_category_id;
                    this.$_MISAResources.fixed_asset_category.forEach((item) => {
                        if (item.fixed_asset_category_name == this.typeValue) {
                            fixed_asset_category_id = item.fixed_asset_category_id;
                        }
                    });

                    // xuất theo tìm kiếm, loại, bộ phận, phân trang
                    await this.$_MISAApi.fixedAssetApi.exportToExcel(
                        this.searchValue,
                        department_id,
                        fixed_asset_category_id,
                        this.sortField,
                        this.sortType
                    );
                }
            } catch (error) {
                console.log("exportToExcel ~ error:", error);
            } finally {
                this.isLoadingExcel = false;
            }
        },
        /**
         * Sự kiện import thành công
         * Author: vtahoang (08/08/2023)
         */
        async onImportSuccess() {
            await this.assetsShown();
            this.showTMSuccess();
        },
        /**
         * Xoá tất cả item được chọn
         * Author: vtahoang (04/08/2023)
         */
        clearAllSelectedItems() {
            // Xoá tất cả các item được chọn
            for (let key in this.allSelectedItems) {
                this.allSelectedItems[key] = false;
            }
            for (let key in this.selectedItems) {
                this.selectedItems[key] = false;
            }
        },
    },
    watch: {
        recordPerPage() {
            this.assetsShown();
        },
        currentPage() {
            this.assetsShown();
            this.selectedRow = 0;
        },
        async searchValue() {
            this.isLoadingSearch = true;
            await this.assetsShown();
            this.isLoadingSearch = false;
            // về trang 1
            this.currentPage = 1;
        },
        typeValue() {
            this.assetsShown();

            // về trang 1
            this.currentPage = 1;
        },
        departmentValue() {
            this.assetsShown();

            // về trang 1
            this.currentPage = 1;
        },
        sortValue() {
            this.assetsShown();

            // về trang 1
            this.currentPage = 1;
        },
        apiError() {
            // Nếu api trả về lỗi thì hiện toast message lỗi
            if (this.$_MISAApi.apiError != null) this.showTMError();
        },
        /**
         * Hiển thị thông báo xoá tài sản
         * Author: vtahoang (08/08/2023)
         */
        showDeleteDialog() {
            // hiển thị thông báo xoá tài sản
            let target = this.assetsList.find((item) => {
                return item.fixed_asset_id == this.contextMenuTarget;
            });
            // Cập nhật message theo tài sản được chọn
            if (target)
                this.deleteDiaglogMessage = this.$_MISAResources.dialog.delete.deleteMessage(
                    target.fixed_asset_code,
                    target.fixed_asset_name
                );
        },
        /**
         * Hiển thị thông báo xoá nhiều tài sản
         * Author: vtahoang (08/08/2023)
         */
        showDeleteManyDialog() {
            // đếm số bản ghi đc chọn
            let count = 0;
            for (let key in this.allSelectedItems) {
                if (this.allSelectedItems[key]) count++;
            }
            // định dạng lại chuỗi
            let stringCount = count.toString().padStart(2, "0");
            this.deleteManyDiaglogMessage = this.deleteDiaglogMessage =
                this.$_MISAResources.dialog.delete.deleteManyMessage(stringCount);
        },
        /**
         * Cập nhật danh sách bản ghi được chọn khi có thay đổi
         * Author: vtahoang (08/08/2023)
         */
        selectedItems: {
            handler: function () {
                // cập nhật danh sách bản ghi được chọn khi có thay đổi
                this.allSelectedItems = { ...this.allSelectedItems, ...this.selectedItems };
            },
            deep: true,
        },
    },
    computed: {
        apiError() {
            return this.$_MISAApi.apiError.errors;
        },
        /**
         * Kiểm tra có item nào được chọn hay không
         * Author: vtahoang (04/08/2023)
         */
        disabledDeleteManyBtn() {
            // Nếu có 1 item được chọn thì không disable nút xoá nhiều
            if (Object.values(this.selectedItems).some((item) => item == true)) return false;
            return true;
        },
        /**
         * Kiểm tra tất cả item có được chọn hay không
         * Author: vtahoang (04/08/2023)
         */
        selectAllStatus() {
            // Nếu tất cả item được chọn thì select all
            if (Object.keys(this.selectedItems).length == 0) return false;
            if (Object.values(this.selectedItems).every((item) => item == true)) return true;
            return false;
        },
        /**
         * Dùng để kiểm tra giá trị sắp xếp có thay đổi không
         * Author: vtahoang (04/08/2023)
         */
        sortValue() {
            return this.sortField + " " + this.sortType;
        },
        /**
         * Tính chiều cao của empty và loading
         * Author: vtahoang (04/08/2023)
         */
        heightOfEmptyAndLoading() {
            let rows = this.recordPerPage;
            if (rows <= 5) rows = 5;
            let height = rows * 38;
            let maxHeight = window.innerHeight - 44 - 60 - 40 - 20 - 40;
            if (height > maxHeight) height = maxHeight;

            return height + "px";
        },
    },
    async beforeCreate() {
        /**
         * Lấy danh sách bộ phận sử dụng
         * Author: vtahoang - (13/07/2023)
         */
        let department = await this.$_MISAApi.departmentApi.getAll();
        this.$_MISAResources.department = department;

        /**
         * Lấy danh sách loại tài sản
         * Author: vtahoang - (20/07/2023)
         */
        let fixed_asset_category = await this.$_MISAApi.fixedAssetCategoryApi.getAll();
        this.$_MISAResources.fixed_asset_category = fixed_asset_category;

        /**
         * Lấy danh sách tài sản
         * Author: vtahoang - (20/07/2023)
         */
        this.assetsShown();
    },
    updated() {
        try {
            // update tổng khi có thay đổi
            this.updateTotal();
        } catch (error) {
            console.log("updated ~ error:", error);
        }
    },
    components: {
        MISASearch,
        MISADropdown,
        MISAForm,
        MISAToastMessage,
        MISATextfield,
        MISAPaging,
        MISAContextMenu,
        MISADatePicker,
        MISACheckBox,
        MISADialog,
        MISASort,
        MISAExcel,
    },
};
</script>
