import resources from "@/helper/resources.js";

var defaultTableColumns = [
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
        width: "14%",
    },
    {
        title: resources.assetsManagementIncrease.table.depreciationValueYear,
        field: "depreciation_value_year",
        align: "right",
        type: "number",
        width: "14%",
    },
    {
        title: resources.assetsManagementIncrease.table.remainingValue,
        field: "remaining_value",
        align: "right",
        type: "number",
        width: "14%",
    },
];

var selectAssetsForm = {
    namespaced: true,
    state: () => {
        return {
            // id tài sản đã chọn
            selectedList: {},
            // Danh sách tài sản
            assetsList: [],
            // Danh sách cột trong bảng
            tableColumns:
                JSON.parse(localStorage.getItem("selectAssets__tableColumns")) ||
                JSON.parse(JSON.stringify(defaultTableColumns)),
        };
    },
    mutations: {
        /**
         * cập nhật danh sách id tài sản đã chọn
         * @param {*} state
         * @param {*} payload
         */
        updateSelectedList(state, payload) {
            state.selectedList = { ...state.selectedList, ...payload };
        },
        /**
         * Đặt mới danh sách tài sản đã chọn
         * @param {*} state
         * @param {*} payload
         */
        setSelectedList(state, payload) {
            state.selectedList = payload;
        },
        /**
         * Thêm tài sản vào danh sách
         * @param {*} state
         * @param {*} payload
         * Author: vtahoang (22/08/2023)
         */
        pushAssetsList(state, payload) {
            // bỏ các phần tử trùng lặp
            payload = payload.filter(
                (item) =>
                    !state.assetsList.some((asset) => asset.fixed_asset_id == item.fixed_asset_id)
            );
            state.assetsList = [...state.assetsList, ...payload];
        },
        setAssetsList(state, payload) {
            state.assetsList = payload;
        },
        /**
         * Xoá tài sản khỏi danh sách
         * @param {*} state
         * @param {*} id
         * Author: vtahoang (22/08/2023)
         */
        removeAssetsList(state, ids) {
            state.assetsList = state.assetsList.filter(
                (item) => !ids.includes(item.fixed_asset_id)
            );
        },
        /**
         * Ẩn form
         * @param {*} state
         */
        hideForm(state) {
            state.selectedList = {};
            state.assetsList = [];
        },
    },
    getters: {
        /**
         * Trả về mảng id tài sản đã chọn
         * @param {*} state
         * @returns Array
         */
        getListIdsSelected: (state) => {
            return Object.keys(state.selectedList).filter((key) => state.selectedList[key]);
        },
    },
    actions: {
        /**
         * Xoá danh sách tài sản đã chọn
         * Author: vtahoang (27/08/2023)
         */
        clearSelectedList({ state, rootState }) {
            let originalId = rootState.voucherForm.originalList.map((item) => item.fixed_asset_id);

            state.assetsList = state.assetsList.filter((item) =>
                originalId.includes(item.fixed_asset_id)
            );
            let ids = state.assetsList.map((item) => item.fixed_asset_id);
            state.selectedList = {};
            ids.forEach((item) => {
                state.selectedList[item] = true;
            });
        },
        /**
         * Cập nhật thứ tự cột bảng
         * @param {*} param0
         * @param {*} columns
         * Author: vtahoang (06/09/2023)
         */
        updateTableColumns({ commit, state }, columns) {
            state.tableColumns = columns;
            localStorage.setItem("selectAssets__tableColumns", JSON.stringify(columns));
        },
    },
};

export default selectAssetsForm;
