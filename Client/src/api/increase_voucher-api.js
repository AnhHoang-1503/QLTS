import apiClient from "./api-client";
import { createFilterParam } from "./api-helper/increase_voucher-helper";

const path = "IncreaseVoucher/";

/**
 * Tạo chứng từ
 * @param {*} data Dữ liệu chứng từ
 * Author: vtahoang (23/08/2023)
 */
async function createVoucher(data) {
    let url = path;

    await apiClient.post(url, data);
}

/**
 * Lấy bản ghi theo bộ lọc
 * @param {*} search Giá trị tìm kiếm
 * @param {*} sortField Trường sắp xếp
 * @param {*} sortType Kiểu sắp xếp
 * @param {*} pageSize Số bản ghi trên 1 trang
 * @param {*} pageIndex Trang hiện tại
 * Author: vtahoang (24/08/2023)
 */
async function getFilter({ search, sortField, sortType, pageSize, pageIndex }) {
    // tạo query params
    let params = "?" + createFilterParam(search, sortField, sortType, pageSize, pageIndex);

    let getVoucherUrl = path + "filter" + params;
    let getTotalRecordsUrl = path + "total-records/filter" + params;
    // Lấy danh sách chứng từ
    let voucher = await apiClient.get(getVoucherUrl);
    // Lấy tổng số bản ghi
    let totalRecords = await apiClient.get(getTotalRecordsUrl);
    return { vouchers: voucher.data, totalRecords: totalRecords.data };
}

/**
 * Lấy chi tiết chứng từ
 * @param {*} id id chứng từ
 * @returns Chi tiết chứng từ
 * Author: vtahoang (24/08/2023)
 */
async function getDetails(id) {
    let url = path + "details/" + id;

    let details = await apiClient.get(url);

    return details.data;
}

/**
 * Lấy mã sinh tự động
 * @returns Mã sinh tự động
 * Author: vtahoang (24/08/2023)
 */
async function getDefaultCode() {
    let url = path + "default-code";

    let code = await apiClient.get(url);

    return code.data;
}

/**
 * Cập nhật chứng từ
 * @param {*} data Dữ liệu chứng từ cập nhật
 * Author: vtahoang (27/08/2023)
 */
async function updateVoucher(data) {
    let url = path;

    await apiClient.put(url, data);
}

/**
 * Xoá chứng từ
 * @param {*} id id chứng từ
 * Author: vtahoang (27/08/2023)
 */
async function deleteVoucher(id) {
    let url = path + id;

    await apiClient.delete(url);
}

/**
 * Xoá nhiều chứng từ
 * @param {Array} deleteList Danh sách xoá
 * Author: vtahoang (27/08/2023)
 */
async function deleteManyVoucher(deleteList) {
    let url = path;
    await apiClient.delete(url, { data: deleteList });
}

export default {
    createVoucher,
    getFilter,
    getDetails,
    getDefaultCode,
    updateVoucher,
    deleteVoucher,
    deleteManyVoucher,
};
