import loginApi from "../../api/login-api";
import handler from "../../helper/handler";
import router from "../../router/router";
import jwtDecode from "jwt-decode";

var auth = {
    namespaced: true,
    state: () => {
        return {
            account: "",
            password: "",
            errorMessage: "",
            loading: false,
        };
    },
    mutations: {},
    actions: {
        /**
         * Đăng nhập
         * @param {*} param0
         * Author: vtahoang (16/09/2023)
         */
        async submit({ state, rootState }) {
            let data = {
                account: state.account,
                password: state.password,
            };

            try {
                state.loading = true;
                await loginApi.login(data);
                rootState.global.user = jwtDecode(localStorage.getItem("token"));
                state.account = "";
                state.password = "";
                router.push("/assets-management");
            } catch (error) {
                state.errorMessage = error.response.data.UserMessage;
                handler.debounce(() => {
                    state.errorMessage = "";
                }, 4000)();
            } finally {
                state.loading = false;
            }
        },
        /**
         * Đăng xuất
         * @param {*} param0
         * Author: vtahoang  (16/09/2023)
         */
        async logout({ rootState }) {
            localStorage.removeItem("token");
            router.push("/login");
            rootState.global.user = null;
        },
        /**
         * Hiện toast message
         * @param {*} param0
         * @param {*} message Thông báo
         * Author: vtahoang (16/09/2023)
         */
        setErrorMessage({ state }, message) {
            state.errorMessage = message;
            handler.debounce(() => {
                state.errorMessage = "";
            }, 4000)();
        },
    },
    getters: {},
};

export default auth;
