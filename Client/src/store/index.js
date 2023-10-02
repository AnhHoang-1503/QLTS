import voucherForm from "./modules/voucherForm.js";
import selectAssetsForm from "./modules/selectAssetsForm.js";
import increase from "./modules/increase.js";
import editAssetForm from "./modules/editAssetForm.js";
import auth from "./modules/auth.js";
import global from "./modules/global.js";
import { createStore } from "vuex";

const store = createStore({
    modules: {
        voucherForm,
        selectAssetsForm,
        increase,
        editAssetForm,
        auth,
        global,
    },
});

export default store;
