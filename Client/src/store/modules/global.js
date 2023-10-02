import jwtDecode from "jwt-decode";

var global = {
    namespaced: true,
    state: () => {
        return {
            // thông tin user
            user: localStorage.getItem("token") ? jwtDecode(localStorage.getItem("token")) : null,
        };
    },
};

export default global;
