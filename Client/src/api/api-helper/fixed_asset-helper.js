import resources from "@/helper/resources.js";
import enums from "@/helper/enums.js";

/**
 * Tạo json object filter cho api
 * @param {int} pageSize số bản ghi 1 trang
 * @param {int} pageIndex trang hiện tại
 * @param {string} search giá trị tìm kiếm
 * @param {string} departmentId id phòng ban
 * @param {string} fixedAssetCategoryId id loại tài sản
 * @param {string} sortField trường sắp xếp, mặc định là ngày sửa
 * @param {string} sortType kiểu sắp xếp, mặc định là tăng dần`
 * @returns json object filter
 * Author: vtahoang (30/07/2023)
 */
function createFilterParam(
    search,
    departmentId,
    fixedAssetCategoryId,
    sortField,
    sortType,
    pageSize,
    pageIndex
) {
    let filterObject = {
        Limit: pageSize ? pageSize : 20,
        Offset: (pageIndex - 1) * pageSize ? (pageIndex - 1) * pageSize : 0,
        Columns: [
            "fixed_asset_id",
            "fixed_asset_code",
            "fixed_asset_name",
            "department_id",
            "department_code",
            "department_name",
            "fixed_asset_category_id",
            "fixed_asset_category_code",
            "fixed_asset_category_name",
            "start_using_date",
            "purchase_date",
            "cost",
            "quantity",
            "depreciation_rate",
            "depreciation_value_year",
            "tracked_year",
            "life_time",
            "production_year",
            "accumulated_depreciation",
            "remaining_value",
            "budget",
        ],
        SortField: sortField ? sortField : "modified_date",
        SortType: sortType ? sortType : "DESC",
        Filter: [
            {
                Field: "department_id",
                Value: departmentId,
                OperatorType: "=",
            },
            {
                Field: "fixed_asset_category_id",
                Value: fixedAssetCategoryId,
                OperatorType: "=",
            },
        ],
        Search: [
            {
                Field: "fixed_asset_code",
                Value: search,
                OperatorType: "LIKE",
            },
            {
                Field: "fixed_asset_name",
                Value: search,
                OperatorType: "LIKE",
            },
        ],
    };

    filterObject.Columns = filterObject.Columns.join(", ");

    return "filterJson=" + JSON.stringify(filterObject);
}

/**
 * Tạo json object excel options cho api
 * Author: vtahoang (05/08/2023)
 */
function createExcelOptionsParam() {
    let excelOptionsObject = {
        SheetName: resources.excelExport.sheetName,
        FileName: resources.excelExport.fileName,
        Columns: [
            {
                ColumnName: resources.excelExport.header.fixed_asset_code,
                FieldName: "fixed_asset_code",
            },
            {
                ColumnName: resources.excelExport.header.fixed_asset_name,
                FieldName: "fixed_asset_name",
            },
            {
                ColumnName: resources.excelExport.header.fixed_asset_category_name,
                FieldName: "fixed_asset_category_name",
                ColumnWidth: 30,
            },
            {
                ColumnName: resources.excelExport.header.department_name,
                FieldName: "department_name",
                ColumnWidth: 30,
            },
            {
                ColumnName: resources.excelExport.header.quantity,
                FieldName: "quantity",
                DataType: enums.excelExport.dataType.number,
            },
            {
                ColumnName: resources.excelExport.header.cost,
                FieldName: "cost",
                DataType: enums.excelExport.dataType.number,
            },

            {
                ColumnName: resources.excelExport.header.accumulated_depreciation,
                FieldName: "accumulated_depreciation",
                DataType: enums.excelExport.dataType.number,
            },
            {
                ColumnName: resources.excelExport.header.remaining_value,
                FieldName: "remaining_value",
                DataType: enums.excelExport.dataType.number,
            },
            {
                ColumnName: resources.excelExport.header.life_time,
                FieldName: "life_time",
                DataType: enums.excelExport.dataType.number,
            },
            {
                ColumnName: resources.excelExport.header.purchase_date,
                FieldName: "purchase_date",
                DataType: enums.excelExport.dataType.date,
            },
            {
                ColumnName: resources.excelExport.header.start_using_date,
                FieldName: "start_using_date",
                DataType: enums.excelExport.dataType.date,
            },
        ],
    };

    return "excelOptionsJson=" + JSON.stringify(excelOptionsObject);
}

export { createFilterParam, createExcelOptionsParam };
