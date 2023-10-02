import apiClient from "./api-client";

const path = "Department";

/**
 * Lấy danh sách phòng ban
 * @returns {Promise} department
 * Author: vtahoang (25/07/2023)
 */
async function getAll() {
    try {
        let url = path;
        let department = await apiClient.get(url);
        return department.data;
    } catch (error) {
        console.log("getAll ~ error:", error);
    }
}

export default { getAll };
