import apiClient from "./api-client";

const path = "FixedAssetCategory";

/**
 * Lấy danh sách tài sản
 * @returns {Promise} fixedAssetCategory
 * Author: vtahoang (25/07/2023)
 */
async function getAll() {
    try {
        let url = path;
        let fixedAssetCategory = await apiClient.get(url);
        return fixedAssetCategory.data;
    } catch (error) {
        console.log("getAll ~ error:", error);
    }
}

export default { getAll };
