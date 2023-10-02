import resources from "@/helper/resources";

var editAssetForm = {
    namespaced: true,
    state: () => {
        return {
            // Tài sản sửa
            fixedAsset: {},
            // Thông báo lỗi
            errorMessages: "",
        };
    },
    mutations: {
        setFixedAsset(state, fixedAsset) {
            state.fixedAsset = fixedAsset;
        },
        setErrorMessages(state, errorMessages) {
            state.errorMessages = errorMessages;
        },
        setBudget(state, budget) {
            state.fixedAsset.budget = budget;
        },
    },
    actions: {
        /**
         * Tài sản sửa
         * @param {*} param0
         * @param {*} fixedAsset Tài sản sửa
         */
        setFixedAsset({ commit, rootState }, fixedAsset) {
            fixedAsset = JSON.parse(JSON.stringify(fixedAsset));
            // Mặc định là nguồn 1
            if (!fixedAsset.budget) {
                fixedAsset.budget = [{ ...rootState.increase.budgetCategory[0] }];
                fixedAsset.budget[0].cost = fixedAsset.cost;
            }
            if (typeof fixedAsset.budget === "string") {
                fixedAsset.budget = JSON.parse(fixedAsset.budget);
            }

            commit("setFixedAsset", fixedAsset);
        },
        submitForm({ commit, state, rootState, dispatch }) {
            let asset = JSON.parse(JSON.stringify(state.fixedAsset));
            let budgetCategories = rootState.increase.budgetCategory;
            asset.cost = 0;
            asset.budget.forEach((item, index) => {
                // bổ sung thông tin nguồn vốn
                let budget = budgetCategories.find(
                    (budget) => budget.budget_category_name === item.budget_category_name
                );
                if (budget) {
                    asset.budget[index] = { ...item, ...budget };
                }
                // Tính lại tổng nguyên giá
                if (item.cost) {
                    asset.cost += parseInt(item.cost);
                }
            });
            asset.budget = JSON.stringify(asset.budget);

            dispatch("voucherForm/updateAssets", asset, { root: true });
        },
    },
};

export default editAssetForm;
