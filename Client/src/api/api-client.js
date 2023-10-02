import axios from "axios";
import { reactive } from "vue";
import apiConfig from "./api-config";
import router from "../router/router";

const apiClient = axios.create(apiConfig);

var apiError = reactive({ errors: null });

// Thêm interceptor cho request
apiClient.interceptors.request.use(
    function (config) {
        // Xử lý trước khi request được gửi lên server
        // Lấy token từ localStorage
        const token = localStorage.getItem("token");
        // Thêm token vào header nếu có
        if (token) {
            config.headers["Authorization"] = token;
        }
        // Xoá các lỗi cũ
        apiError.errors = null;
        return config;
    },
    function (error) {
        // Xử lý khi request bị lỗi
        return Promise.reject(error);
    }
);

// Thêm interceptor cho response
apiClient.interceptors.response.use(
    function (response) {
        //  Xử lý dữ liệu trả về từ server
        return response;
    },
    function (error) {
        // Xử lý khi response bị lỗi
        // Lỗi nhập liệu từ người dùng
        if (error.response && error.response.status == 400) {
            throw error;
        }
        // Lỗi chưa đăng nhập
        if (error.response && error.response.status == 401) {
            localStorage.removeItem("token");
            router.push("/login");
        }
        // Các lỗi khác đưa vào apiError
        apiError.errors = error;
        return Promise.reject(error);
    }
);

export default apiClient;

export { apiError };
