import Enums from "@/helper/enums.js";
import resources from "@/helper/resources.js";
import handler from "@/helper/handler.js";
import increase_voucherApi from "@/api/increase_voucher-api";

var fixed_asset = {
    fixed_asset_id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    fixed_asset_code: "string",
    fixed_asset_name: "string",
    department_id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    department_code: "string",
    department_name: "string",
    fixed_asset_category_id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    fixed_asset_category_code: "string",
    fixed_asset_category_name: "string",
    purchase_date: "2023-08-27T14:50:46.065Z",
    start_using_date: "2023-08-27T14:50:46.065Z",
    cost: 0,
    quantity: 0,
    depreciation_rate: 0,
    depreciation_value_year: 0,
    tracked_year: 0,
    remaining_value: 0,
    life_time: 0,
    budget: "string",
};

var defaultFormData = {
    ref_id: "",
    ref_no: "",
    ref_date: new Date(),
    increase_date: new Date(),
    note: "",
    total_cost: 0,
};

var defaultColumnsList = [
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

var voucherForm = {
    namespaced: true,
    state: () => {
        return {
            // Danh sách tài sản ban đầu
            originalList: [],
            // Danh sách tài sản hiện tại
            assetsList: [],
            // Danh sách tài sản chọn mới
            newSelectedList: [],
            formMode: Enums.formMode.hide,
            formData: JSON.parse(JSON.stringify(defaultFormData)),
            // Danh sách cột trong bảng
            tableColumns:
                JSON.parse(localStorage.getItem("voucherForm__tableColumns")) ||
                JSON.parse(JSON.stringify(defaultColumnsList)),
            // Chi tiết thay đổi ghi tăng
            details: [],
            defaultCode: "",
            showEditAssetForm: false,
            showSelectAssetsForm: false,
            // Danh sách tài sản sửa
            editList: [],
            // Tiêu đề form
            formTitle: resources.assetsManagementIncrease.form.addTitle,
            // Hiển thị dialog đóng
            closeDialog: false,
        };
    },
    mutations: {
        setShowEditAssetForm(state, payload) {
            state.showEditAssetForm = payload;
        },
        setShowSelectAssetsForm(state, payload) {
            state.showSelectAssetsForm = payload;
        },
        /**
         * Ẩn form
         * @param {*} state
         * Author: vtahoang (23/08/2023)
         */
        hideForm(state) {
            state.formMode = Enums.formMode.hide;
            state.formData = JSON.parse(JSON.stringify(defaultFormData));
            state.assetsList = [];
            state.newSelectedList = [];
            state.editList = [];
            state.originalList = [];
        },
        /**
         * Hiện form
         * @param {*} state
         * @param {*} payload
         * Author: vtahoang (23/08/2023)
         */
        showForm(state, payload) {
            state.formMode = payload;
            state.formTitle =
                payload === Enums.formMode.add
                    ? resources.assetsManagementIncrease.form.addTitle
                    : resources.assetsManagementIncrease.form.editTitle;
        },
        /**
         * Cập nhật danh sách tài sản mới
         * @param {*} state
         * @param {*} payload
         */
        setNewSelectedList(state, payload) {
            state.newSelectedList = payload;
        },
        /**
         * Cập nhật danh sách tài sản
         * @param {*} state
         * @param {*} payload
         * Author: vtahoang (23/08/2023)
         */
        setAssetsList(state, payload) {
            state.assetsList = payload;
        },
        /**
         * Cập nhật chi tiết ghi tăng
         * @param {*} state
         * Author: vtahoang (23/08/2023)
         */
        updateDetails(state) {
            // Danh sách tài sản ban đầu của chứng từ
            let originalId = state.originalList.map((item) => item.fixed_asset_id);

            state.details = [];
            state.formData.total_cost = 0;
            state.assetsList.forEach((asset) => {
                // Cập nhật tổng
                state.formData.total_cost += asset.cost;

                // Những tài sản không thay đổi thì không gửi lên server
                if (asset.budget === undefined) return;
                // Nếu tài sản không nằm trong danh sách ban đầu thì thêm mới
                if (!originalId.includes(asset.fixed_asset_id)) {
                    state.details.push({
                        fixedAsset: asset,
                        editMode: Enums.editMode.add,
                    });

                    //Ngược lại thì là sửa
                } else {
                    state.details.push({
                        fixedAsset: asset,
                        editMode: Enums.editMode.edit,
                    });
                }
            });
            // Những tài sản trong originalId mà không có trong assetsList thì xoá
            originalId.forEach((id) => {
                if (!state.assetsList.find((item) => item.fixed_asset_id === id)) {
                    let asset = { ...fixed_asset };
                    asset.fixed_asset_id = id;
                    state.details.push({
                        fixedAsset: asset,
                        editMode: Enums.editMode.delete,
                    });
                }
            });
        },
        /**
         * Cập nhật dữ liệu form
         * @param {*} state
         * @param {*} payload
         * Author: vtahoang (23/08/2023)
         */
        updateFormData(state, payload) {
            state.formData = { ...state.formData, ...payload };
        },
        setOriginalList(state, payload) {
            state.originalList = payload;
        },
        updateAssetsList(state, payload) {
            state.assetsList = { ...state.assetsList, ...payload };
        },
        setCloseDialog(state, payload) {
            state.showConfirmDialog = payload;
        },
    },
    getters: {
        /**
         * Lấy danh sách id tài sản
         * @param {*} state
         * @returns Danh sách id
         * Author: vtahoang (23/08/2023)
         */
        getListIds(state) {
            let obj = {};
            state.assetsList.forEach((element) => {
                obj[element.fixed_asset_id] = true;
            });
            return obj;
        },
    },
    actions: {
        /**
         * Cập nhật danh sách tài sản chọn mới
         * @param {*} param0
         * @param {*} payload
         * Author: vtahoang (23/08/2023)
         */
        setNewSelectedList({ commit }, payload) {
            commit("setNewSelectedList", payload);
            commit("setAssetsList", payload);
        },
        /**
         * Xoá tài sản khỏi danh sách chọn mới
         * @param {*} param0
         * @param {*} payload Danh sách id xoá
         * Author: vtahoang (23/08/2023)
         */
        removeFromSelectedList({ commit, state }, payload) {
            let newList = state.assetsList.filter((item) => !payload.includes(item.fixed_asset_id));
            commit("setNewSelectedList", newList);
            commit("setAssetsList", newList);
        },
        async submitForm({ commit, state, dispatch }) {
            commit("updateDetails");
            let object = state.formData;
            object.details = state.details;

            // Chuyển giá trị ngày tháng thành ISOString
            object.ref_date = handler.toISOStringHandler(object.ref_date);
            object.increase_date = handler.toISOStringHandler(object.increase_date);

            try {
                if (state.formMode === Enums.formMode.add) {
                    await increase_voucherApi.createVoucher(object);
                    // Hiển thị toast
                    dispatch("increase/showSuccessToast", "add", { root: true });
                }
                if (state.formMode === Enums.formMode.edit) {
                    await increase_voucherApi.updateVoucher(object);
                    // Hiển thị toast
                    dispatch("increase/showSuccessToast", "edit", { root: true });
                }
            } catch (error) {
                console.log("submitForm ~ error:", error);
                throw error;
            }
        },
        /**
         * Hiển thị form
         * @param {*} param0
         * Author: vtahoang (24/08/2023)
         */
        async showForm({ commit }, payload) {
            // Nếu là thêm mới hoặc nhân bản thì lấy mã sinh tự động
            if (payload === Enums.formMode.add || payload === Enums.formMode.duplicate) {
                let code = await increase_voucherApi.getDefaultCode();
                commit("updateFormData", { ref_no: code });
            }

            commit("showForm", payload);
        },
        /**
         * Cập nhật chi tiết tài sản (ghi tăng)
         * @param {*} param0
         * @param {*} fixed_asset Tài sản
         */
        async updateAssets({ commit, state }, fixed_asset) {
            let assetsList = state.assetsList;
            let index = assetsList.findIndex(
                (item) => item.fixed_asset_id === fixed_asset.fixed_asset_id
            );
            if (index > -1) {
                assetsList[index] = fixed_asset;
            } else {
                state.editList.push(fixed_asset);
            }

            commit("setAssetsList", assetsList);
        },
        /**
         * Mở form để sửa tài sản
         * @param {*} param0
         * @param {*} asset tài sản sửa
         * Author: vtahoang (25/08/2023)
         */
        openEditForm({ commit, rootState }, voucher) {
            commit("updateFormData", voucher);
            commit("setAssetsList", rootState.increase.details);
            commit("setOriginalList", rootState.increase.details);
            commit("showForm", Enums.formMode.edit);
        },
        /**
         * Cập nhật thứ tự cột bảng
         * @param {*} param0
         * @param {*} columns
         * Author: vtahoang (06/09/2023)
         */
        updateTableColumns({ commit, state }, columns) {
            state.tableColumns = columns;
            localStorage.setItem("voucherForm__tableColumns", JSON.stringify(columns));
        },
    },
};

export default voucherForm;
