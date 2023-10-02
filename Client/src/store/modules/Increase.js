import resources from "@/helper/resources.js";
import increase_voucherApi from "@/api/increase_voucher-api";
import handler from "@/helper/handler.js";
import budget_categoryApi from "@/api/budget_category-api";

var defaultFirstTableColumns = [
    {
        title: resources.assetsManagementIncrease.table.ref_no,
        field: "ref_no",
        width: "10%",
        color: "#1aa4c8",
    },
    {
        title: resources.assetsManagementIncrease.table.ref_date,
        field: "ref_date",
        align: "center",
        type: "date",
        width: "13%",
    },
    {
        title: resources.assetsManagementIncrease.table.increase_date,
        field: "increase_date",
        align: "center",
        type: "date",
        width: "13%",
    },
    {
        title: resources.assetsManagementIncrease.table.totalCost,
        field: "total_cost",
        align: "right",
        type: "number",
        width: "13%",
    },
    {
        title: resources.assetsManagementIncrease.table.description,
        field: "note",
        width: "500px",
    },
];

var defaultSecondTableColumns = [
    {
        title: resources.assetsManagementIncrease.table.assetCode,
        field: "fixed_asset_code",
        width: "10%",
    },
    {
        title: resources.assetsManagementIncrease.table.assetName,
        field: "fixed_asset_name",
    },
    {
        title: resources.assetsManagementIncrease.table.department,
        field: "department_name",
    },
    {
        title: resources.assetsManagementIncrease.table.cost,
        field: "cost",
        align: "right",
        type: "number",
        width: "13%",
    },
    {
        title: resources.assetsManagementIncrease.table.depreciationValueYear,
        field: "depreciation_value_year",
        align: "right",
        type: "number",
        width: "13%",
    },
    {
        title: resources.assetsManagementIncrease.table.remainingValue,
        field: "remaining_value",
        align: "right",
        type: "number",
        width: "13%",
    },
];

var increase = {
    namespaced: true,
    state: () => {
        return {
            // Danh sách cột bảng 1
            firstTableColumns:
                JSON.parse(localStorage.getItem("increase__firstTableColumns")) ||
                JSON.parse(JSON.stringify(defaultFirstTableColumns)),
            // Danh sách cột bảng 2
            secondTableColumns:
                JSON.parse(localStorage.getItem("increase__secondTableColumns")) ||
                JSON.parse(JSON.stringify(defaultSecondTableColumns)),
            // Danh sách chứng từ
            vouchers: [],
            // id chứng từ hiển thị chi tiết
            currentVoucherId: "",
            // Chi tiết chứng từ
            details: [],
            // Tổng nguyên giá
            totalCost: 0,
            // loading bảng 1
            firstTableLoading: false,
            // loading bảng 2
            secondTableLoading: false,
            // Danh sách nguồn hình thành
            budgetCategory: [],
            // Sô lượng chứng từ
            totalVouchers: 0,
            // Danh sách chứng từ đã chọn
            selectedVouchers: {},
            // Hiển thị toast thành công
            showSuccessToast: false,
            // Nội dung thông báo
            toastContent: "testtttt",
        };
    },
    mutations: {
        setVouchers(state, vouchers) {
            state.vouchers = vouchers;
        },
        setDetails(state, details) {
            state.details = details;
        },
        setCurrentVoucherId(state, id) {
            state.currentVoucherId = id;
        },
        setTotalCost(state, totalCost) {
            state.totalCost = totalCost;
        },
        setFirstTableLoading(state, loading) {
            state.firstTableLoading = loading;
        },
        setSecondTableLoading(state, loading) {
            state.secondTableLoading = loading;
        },
        setBudgetCategory(state, budgetCategory) {
            state.budgetCategory = budgetCategory;
        },
        updateSelectedVouchers(state, payload) {
            state.selectedVouchers = { ...state.selectedVouchers, ...payload };
        },
        setSelectedVouchers(state, selectedVouchers) {
            state.selectedVouchers = selectedVouchers;
        },
        setShowSuccessToast(state, showSuccessToast) {
            state.showSuccessToast = showSuccessToast;
        },
        setToastContent(state, toastContent) {
            state.toastContent = toastContent;
        },
    },
    actions: {
        /**
         * Lấy danh sách chứng từ
         * @param {*} search giá trị tìm kiếm
         * @param {*} sortField trường sắp xếp
         * @param {*} sortType kiểu sắp xếp
         * @param {*} pageSize số bản ghi một trang
         * @param {*} pageIndex trang hiện tại
         * Author: vtahoang (24/08/2023)
         */
        async getVouchers(
            { commit, dispatch, state },
            { search, sortField, sortType, pageSize, pageIndex }
        ) {
            commit("setVouchers", []);
            commit("setSecondTableLoading", true);
            commit("setFirstTableLoading", true);
            try {
                let vouchers = await increase_voucherApi.getFilter({
                    search,
                    sortField,
                    sortType,
                    pageSize,
                    pageIndex,
                });

                // Tính tổng nguyên giá
                let totalCost = 0;
                vouchers.vouchers.forEach((element) => {
                    totalCost += element.total_cost;
                });

                totalCost = handler.numberHandler(totalCost);

                commit("setTotalCost", totalCost);
                commit("setVouchers", vouchers.vouchers);
                state.totalVouchers = vouchers.totalRecords;
                // Lấy detail chứng từ đầu tiên
                if (vouchers.vouchers.length > 0) {
                    dispatch("getDetails", { id: vouchers.vouchers[0].ref_id });
                } else {
                    commit("setDetails", []);
                }
            } catch (error) {
                console.log("getVouchers ~ error:", error);
            } finally {
                commit("setFirstTableLoading", false);
                commit("setSecondTableLoading", false);
            }
        },
        /**
         * Lấy chi tiết chứng từ
         * @param {*} param0
         * @param {*} param1
         */
        async getDetails({ commit }, { id }) {
            commit("setCurrentVoucherId", id);
            commit("setDetails", []);
            commit("setSecondTableLoading", true);
            try {
                let details = await increase_voucherApi.getDetails(id);
                commit("setDetails", details);
            } catch (error) {
                console.log("getDetails ~ error:", error);
            } finally {
                commit("setSecondTableLoading", false);
            }
        },
        /**
         * Lấy danh sách nguồn hình thành
         * @param {*} param0
         */
        async getBudgetCategory({ commit }) {
            try {
                let budgetCategory = await budget_categoryApi.getAll();
                commit("setBudgetCategory", budgetCategory);
            } catch (error) {
                console.log("getBudgetCategory ~ error:", error);
            }
        },
        /**
         * Hiển thị toast thành công
         * @param {*} param0
         * @param {*} toastType Loại toast success (edit || add || delete)
         */
        showSuccessToast({ commit, state }, toastType) {
            commit("setToastContent", resources.assetsManagementIncrease.toast[toastType]);
            commit("setShowSuccessToast", true);

            handler.debounce(() => {
                commit("setShowSuccessToast", false);
            }, 3000)();
        },
        /**
         * Cập nhật danh sách cột bảng 1
         * @param {*} param0
         * @param {Object} columns Danh sách colums mới
         * Author: vtahoang (06/09/2023)
         */
        updateFirstTableColumns({ commit, state }, columns) {
            state.firstTableColumns = columns;
            localStorage.setItem("increase__firstTableColumns", JSON.stringify(columns));
        },
        /**
         * Cập nhật danh sách cột bảng 2
         * @param {*} param0
         * @param {Object} columns Danh sách colums mới
         * Author: vtahoang (06/09/2023)
         */
        updateSecondTableColumns({ commit, state }, columns) {
            state.secondTableColumns = columns;
            localStorage.setItem("increase__secondTableColumns", JSON.stringify(columns));
        },
    },
};

export default increase;
