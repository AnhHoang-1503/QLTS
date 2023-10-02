/**
 * Tạo json object filter cho api
 * @param {int} pageSize số bản ghi 1 trang
 * @param {int} pageIndex trang hiện tại
 * @param {string} search giá trị tìm kiếm
 * @param {string} sortField trường sắp xếp, mặc định là ngày sửa
 * @param {string} sortType kiểu sắp xếp, mặc định là tăng dần`
 * @returns json object filter
 * Author: vtahoang (30/07/2023)
 */
function createFilterParam(search, sortField, sortType, pageSize, pageIndex) {
    let filterObject = {
        Limit: pageSize ? pageSize : 20,
        Offset: (pageIndex - 1) * pageSize ? (pageIndex - 1) * pageSize : 0,
        Columns: ["ref_id", "ref_no", "ref_date", "increase_date", "note", "total_cost"],
        SortField: sortField ? sortField : "modified_date",
        SortType: sortType ? sortType : "DESC",
        Filter: [],
        Search: [
            {
                Field: "ref_no",
                Value: search ? search : "",
                OperatorType: "LIKE",
            },
            {
                Field: "note",
                Value: search ? search : "",
                OperatorType: "LIKE",
            },
        ],
    };

    filterObject.Columns = filterObject.Columns.join(", ");

    return "filterJson=" + JSON.stringify(filterObject);
}

export { createFilterParam };
