import apiClient from "./api-client";
import { createFilterParam, createExcelOptionsParam } from "./api-helper/fixed_asset-helper";
import resources from "@/helper/resources";
import handler from "../helper/handler";

const path = "FixedAsset/";

/**
 * Lấy danh sách tài sản
 * @param {*} search Giá trị tìm kiếm
 * @param {*} departmentId Phòng ban
 * @param {*} fixedAssetCategoryId Loại tài sản
 * @param {*} pageIndex trang hiện tại
 * @param {*} pageSize số bản ghi trên 1 trang
 * @returns {Promise} object fixedAsset và totalRecords
 * Author: vtahoang (25/07/2023)
 */
async function getAll(
    search,
    departmentId,
    fixedAssetCategoryId,
    sortField,
    sortType,
    pageIndex = 1,
    pageSize = 20
) {
    // tạo query params
    let params =
        "?" +
        createFilterParam(
            search,
            departmentId,
            fixedAssetCategoryId,
            sortField,
            sortType,
            pageSize,
            pageIndex
        );

    let getAssetsUrl = path + "filter" + params;
    let getTotalRecordsUrl = path + "total-records/filter" + params;
    let fixedAsset = await apiClient.get(getAssetsUrl);
    let totalRecords = await apiClient.get(getTotalRecordsUrl);
    return { assets: fixedAsset.data, totalRecords: totalRecords.data };
}

/**
 * Lấy danh sách tài sản chưa ghi tăng
 * @param {*} search Giá trị tìm kiếm
 * @param {*} departmentId Phòng ban
 * @param {*} fixedAssetCategoryId Loại tài sản
 * @param {*} pageIndex trang hiện tại
 * @param {*} pageSize số bản ghi trên 1 trang
 * @returns {Promise} object fixedAsset và totalRecords
 * Author: vtahoang (27/08/2023)
 */
async function getAssetsUnIncrease(
    search,
    departmentId,
    fixedAssetCategoryId,
    sortField,
    sortType,
    pageIndex = 1,
    pageSize = 20
) {
    // tạo query params
    let params =
        "?" +
        createFilterParam(
            search,
            departmentId,
            fixedAssetCategoryId,
            sortField,
            sortType,
            pageSize,
            pageIndex
        );

    let getAssetsUrl = path + "filter/unincrease" + params;
    let getTotalRecordsUrl = path + "total-records/unincrease" + params;
    let fixedAsset = await apiClient.get(getAssetsUrl);
    let totalRecords = await apiClient.get(getTotalRecordsUrl);
    return { assets: fixedAsset.data, totalRecords: totalRecords.data };
}

/**
 * Xuất excel
 * @param {*} search Giá trị tìm kiếm
 * @param {*} departmentId Phòng ban
 * @param {*} fixedAssetCategoryId Loại tài sản
 * @param {*} sortField trường sắp xếp, mặc định là ngày sửa
 * @param {*} sortType kiểu sắp xếp, mặc định là tăng dần`
 * @returns {Promise} object fixedAsset và totalRecords
 * Author: vtahoang (01/08/2023)
 */
async function exportToExcel(search, departmentId, fixedAssetCategoryId, sortField, sortType) {
    // tạo query params
    let params =
        "?" +
        createFilterParam(search, departmentId, fixedAssetCategoryId, sortField, sortType) +
        "&" +
        createExcelOptionsParam();

    let url = path + "excel" + params;

    let response = await apiClient.get(url, { responseType: "blob" });

    const fileUrl = window.URL.createObjectURL(
        new Blob([response.data], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        })
    );

    // tên file tải xuống
    let fileName = resources.excelExport.fileName + handler.getCurrentDateTime() + ".xlsx";

    // tạo link tải xuống file excel và click vào link
    const link = document.createElement("a");
    link.href = fileUrl;
    link.setAttribute("download", fileName);
    document.body.appendChild(link);
    link.click();
    window.URL.revokeObjectURL(fileUrl);
    document.body.removeChild(link);
}

/**
 * Xuất file excel mẫu
 * Author: vtahoang (01/08/2023)
 */
async function exampleFileExcel() {
    let url = path + "excel/example";

    let response = await apiClient.get(url, { responseType: "blob" });

    const fileUrl = window.URL.createObjectURL(
        new Blob([response.data], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        })
    );

    // tên file tải xuống
    let fileName = resources.excelExport.exampleFileName;

    // Tao link tai xuong file excel va click vao link
    const link = document.createElement("a");
    link.href = fileUrl;
    link.setAttribute("download", fileName);
    document.body.appendChild(link);
    link.click();
    window.URL.revokeObjectURL(fileUrl);
    document.body.removeChild(link);
}

/**
 * Nhập dữ liệu từ file excel
 * @param {Object} file File excel
 * Author: vtahoang (08/08/2023)
 */
async function importFromExcel(file) {
    let url = path + "excel";
    // thêm file vào form data
    var formData = new FormData();
    formData.append("file", file);

    await apiClient.post(url, formData, {
        headers: {
            "Content-Type": "multipart/form-data",
        },
    });
}

/**
 * Tạo mới tài sản
 * @param {*} data Tài sản tạo mới
 * Author: vtahoang (25/07/2023)
 */
async function createAsset(data) {
    let url = path;

    await apiClient.post(url, data);
}

/**
 * Sửa tài sản
 * @param {*} data Tài sản cần sửa
 * Author: vtahoang (25/07/2023)
 */
async function editAsset(data) {
    let url = path;
    await apiClient.put(url, data);
}

/**
 * Xoá tài sản
 * @param {*} assetsId id tài sản
 * Author: vtahoang (25/07/2023)
 */
async function deleteAsset(assetsId) {
    let url = path + assetsId;
    await apiClient.delete(url);
}

/**
 * Xoá nhiều tài sản
 * @param {*} deleteList Danh sách id tài sản cần xoá
 * Author: vtahoang (25/07/2023)
 */
async function deleteManyAsset(deleteList) {
    let url = path;
    await apiClient.delete(url, { data: deleteList });
}

/**
 * Mã tài sản mặc định theo lần tạo mới cuối cùng
 * @returns Mã tài sản mặc định
 * Author: vtahoang (25/07/2023)
 */
async function getDefaultFixedAssetCode() {
    let url = path + "default-code";
    let fixedAssetDefaultCode = await apiClient.get(url);
    console.log(fixedAssetDefaultCode);
    return fixedAssetDefaultCode.data;
}

/**
 * Lấy danh sách tài sản theo list id
 * @param {Array} ids Danh sách id
 * @returns danh sách tài sản
 * Author: vtahoang (22/08/2023)
 */
async function getListAssetByIds(ids) {
    let url = path + "list-id?idsJson=" + JSON.stringify(ids);
    let fixedAssets = await apiClient.get(url);
    return fixedAssets.data;
}

export default {
    getAll,
    editAsset,
    createAsset,
    deleteAsset,
    deleteManyAsset,
    getDefaultFixedAssetCode,
    exportToExcel,
    exampleFileExcel,
    importFromExcel,
    getListAssetByIds,
    getAssetsUnIncrease,
};
