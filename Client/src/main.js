import "./css/main.css";
import TheHeader from "@/components/layout/TheHeader/TheHeader.vue";
import TheNavbar from "@/components/layout/TheNavbar/TheNavbar.vue";
import TheMain from "@/components/layout/TheMain/TheMain.vue";
import MISAButton from "@/components/base/MISAButton/MISAButton.vue";
import MISAIcon from "@/components/base/MISAIcon/MISAIcon.vue";
import MISAToolTip from "@/components/base/MISAToolTip/MISAToolTip.vue";
import MISALoadingOverlay from "@/components/base/MISALoadingOverlay/MISALoadingOverlay.vue";

import { createApp } from "vue";
import App from "./App.vue";
import MISAEnums from "./helper/enums.js";
import MISAResources from "./helper/resources";
import handler from "./helper/handler";

import departmentApi from "./api/department-api";
import fixedAssetCategoryApi from "./api/fixed_asset_category-api";
import fixedAssetApi from "./api/fixed_asset-api";
import increaseVoucherApi from "./api/increase_voucher-api";
import { apiError } from "./api/api-client";

import focusLoop from "./directives/focusLoop";
import autoFocus from "./directives/autoFocus";
import clickOutsideEvent from "./directives/clickOutsideEvent";

import router from "@/router/router";

import store from "@/store/index.js";

const app = createApp(App);

// Components
app.component("TheNavbar", TheNavbar)
    .component("TheHeader", TheHeader)
    .component("TheMain", TheMain)
    .component("MISAIcon", MISAIcon)
    .component("MISAToolTip", MISAToolTip)
    .component("MISAButton", MISAButton)
    .component("MISALoadingOverlay", MISALoadingOverlay);

// Global properties
app.config.globalProperties.$_MISAResources = MISAResources;
app.config.globalProperties.$_MISAEnums = MISAEnums;
app.config.globalProperties.$_MISAApi = {
    departmentApi,
    fixedAssetCategoryApi,
    fixedAssetApi,
    increaseVoucherApi,
    apiError,
};
app.config.globalProperties.handler = handler;

// Directives
app.directive("focusloop", focusLoop)
    .directive("autofocus", autoFocus)
    .directive("clickoutsideevent", clickOutsideEvent);

// Router
app.use(router);

// Store
app.use(store);

app.mount("#app");
