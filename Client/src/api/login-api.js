import apiClient from "./api-client";

const path = "Login/";

/**
 * Đăng nhập
 * @param {*} data tài khoản và mật khẩu
 * @returns token
 * Author: vtahoang (16/09/2023)
 */
async function login(data) {
    let url = path;

    let response = await apiClient.post(url, data);

    localStorage.setItem("token", response.data);

    return response.data;
}

/**
 * Kiểm tra đã đăng nhập hay chưa
 * @returns
 * Author: vtahoang (16/09/2023)
 */
async function loggedIn() {
    let url = path;
    try {
        await apiClient.get(url);
    } catch (error) {
        return false;
    }
    return true;
}

export default { login, loggedIn };
