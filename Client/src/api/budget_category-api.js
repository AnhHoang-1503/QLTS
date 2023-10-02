import apiClient from "./api-client";

const path = "/BudgetCategory";

/**
 * Lấy tất cả danh sách nguồn hình thành
 * @returns Danh sách nguồn hình thành
 * Author: vtahoang (29/08/2023)
 */
async function getAll() {
    let url = path;
    let budgetCategory = await apiClient.get(url);
    return budgetCategory.data;
}

export default { getAll };
