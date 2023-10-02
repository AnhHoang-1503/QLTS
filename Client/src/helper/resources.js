import handler from "./handler";

const MISAresources = {
    errorMsg: {
        required: "Trường này không được để trống",
        invalid: "Trường này không hợp lệ",
        dateNotGreaterThanCurrentDate: "Ngày không được lớn hơn ngày hiện tại",
        positiveNumber: "Trường này phải lớn hơn 0",
        percent: "Trường này phải nhỏ hơn 100",
        invalidDepreciationValue: "Trường này phải nhỏ hơn nguyên giá",
    },
    form: {
        formTitle: {
            add: "Thêm tài sản",
            edit: "Sửa tài sản",
            duplicate: "Thêm tài sản",
        },
        formInputLabel: {
            assetId: "Mã tài sản",
            assetName: "Tên tài sản",
            assetCategoryCode: "Mã loại tài sản",
            assetCategoryName: "Tên loại tài sản",
            departmentCode: "Mã bộ phận sử dụng",
            departmentName: "Tên bộ phận sử dụng",
            cost: "Nguyên giá",
            quantity: "Số lượng",
            lifeTime: "Số năm sử dụng",
            depreciationRate: "Tỷ lệ hao mòn (%)",
            depreciationValue: "Giá trị hao mòn năm",
            yearTracking: "Năm theo dõi",
            purchasedDate: "Ngày mua",
            startUsingDate: "Ngày sử dụng",
        },
        formInputPlaceholder: {
            assetId: "Nhập mã tài sản",
            assetName: "Nhập tên tài sản",
            assetCategoryCode: "Chọn mã loại tài sản",
            departmentCode: "Chọn mã bộ phận sử dụng",
            purchasedDate: "Nhập ngày mua",
            startUsingDate: "Nhập ngày sử dụng",
        },
        formButtonText: {
            cancel: "Huỷ",
            save: "Lưu",
        },
    },
    fixed_asset_category: [],
    department: [],
    recordPerPage: [5, 10, 20, 50, 100],
    dialog: {
        add: {
            message: "Bạn có muốn huỷ bỏ khai báo tài sản này?",
            btnCancel: "Không",
            btnConfirm: "Huỷ bỏ",
        },
        edit: {
            message:
                "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?",
            btnCancel: "Huỷ bỏ",
            btnSave: "Lưu",
            btnUnsave: "Không lưu",
        },
        duplicate: {
            message: "Bạn có muốn huỷ bỏ khai báo tài sản này?",
            btnCancel: "Không",
            btnConfirm: "Huỷ bỏ",
        },
        delete: {
            btnCancel: "Không",
            btnConfirm: "Xóa",
            deleteMessage(code, name) {
                return `Bạn có muốn xoá tài sản <span class="bold-text">${code} - ${name}</span> không?`;
            },
            deleteVoucherMessage(code) {
                return `Bạn có muốn xoá chứng từ có mã <span class="bold-text">${code}</span> không?`;
            },
            deleteManyMessage(count) {
                return `<span class="bold-text">${count}</span> tài sản đã được chọn. Bạn có muốn xoá các tài sản này khỏi danh sách?`;
            },
            deleteManyVouchersMessage(count) {
                return `<span class="bold-text">${count}</span> chứng từ đã được chọn. Bạn có muốn xoá các chứng từ này khỏi danh sách?`;
            },
        },
        error: {
            close: "Đóng",
            accept: "Đồng ý",
        },
    },
    contextMenu: {
        addAssets: "Thêm tài sản",
        editAssets: "Sửa tài sản",
        deleteAssets: "Xóa tài sản",
        duplicateAssets: "Nhân bản",
        addVouchers: "Thêm chứng từ",
        editVouchers: "Sửa chứng từ",
        deleteVouchers: "Xóa chứng từ",
        duplicateVouchers: "Nhân bản",
    },
    dropdown: {
        emptyItem: "Không có dữ liệu phù hợp",
    },
    excelBox: {
        export: "Xuất dữ liệu",
        import: "Nhập dữ liệu",
        example: "Tải file mẫu",
        upload: "Tải file lên",
        exportAll: "Xuất toàn bộ dữ liệu",
        exportCurrent: "Xuất dữ liệu hiện tại",
    },
    header: {
        title: "Danh sách tài sản",
        department: "Sở tài chính",
        year: "năm",
        logout: "Đăng xuất",
    },
    navBar: {
        appName: "MISA QLTS",
        overview: "Tổng quan",
        assets: "Tài sản",
        infrastructureAssets: "Tài sản HT - ĐB",
        tool: "Công cụ sử dụng",
        categories: "Danh mục",
        search: "Tra cứu",
        report: "Báo cáo",
        assetItems: {
            increase: "Ghi tăng",
            changeInformation: "Thay đổi thông tin",
            transfer: "Điều chuyển tài sản",
            decrease: "Ghi giảm",
            inventory: "Kiểm kê",
            other: "Khác",
            reEvalute: "Đánh giá lại",
            depreciation: "Tính hao mòn",
        },
    },
    assetsManagement: {
        toolbar: {
            searchPlaceholder: "Tìm kiếm tài sản",
            searchTooltip: "Tìm kiếm theo tên và mã tài sản",
            assetCategoryPlaceholder: "Loại tài sản",
            departmentPlaceholder: "Bộ phận sử dụng",
            addBtnText: "Thêm tài sản",
            addBtnTooltip: "Ctrl + N",
            excelBtnTooltip: "Chuột phải để xem thêm tuỳ chọn",
            deleteBtnTooltip: "Xóa tài sản",
            totalSelectedRecords(count) {
                return `Đã chọn <span class="bold-text">${count}</span> bản ghi.`;
            },
            clearSelected: "Bỏ chọn.",
        },
        table: {
            index: "STT",
            indexTooltip: "Số thứ tự",
            assetCode: "Mã tài sản",
            assetName: "Tên tài sản",
            assetCategoryName: "Loại tài sản",
            departmentName: "Bộ phận sử dụng",
            quantity: "Số lượng",
            cost: "Nguyên giá",
            accumulatedDepreciation: "HM/KH lũy kế",
            accumulatedDepreciationTooltip: "Hao mòn, khấu hao lũy kế",
            remainingValue: "Giá trị còn lại",
            tool: "Chức năng",
            editTooltip: "Sửa tài sản",
            duplicateTooltip: "Nhân bản tài sản",
            empty: "Không có bản ghi phù hợp.",
            total: "Tổng số",
            records: "bản ghi",
            totalRecords(count) {
                return `Tổng số: <span class="bold-text">${count}</span> bản ghi`;
            },
        },
        dialog: {
            cantDeleteAssets(assetsCode) {
                return (
                    "Tài sản <span class='bold-text'>" +
                    assetsCode.join(", ") +
                    "</span> đã có phát sinh chứng từ. Bạn không thể xoá tài sản này."
                );
            },
            hideDetail: "Ẩn chi tiết phát sinh",
            showDetail: "Xem chi tiết phát sinh",
            detailVoucher(voucherCode) {
                return `- Chứng từ ghi tăng <span class="bold-text">${voucherCode}</span>`;
            },
        },
    },
    toastMessage: {
        addSuccess: "Lưu dữ liệu thành công",
        developing: "Tính năng đang phát triển",
        deleteSuccess: "Xóa dữ liệu thành công",
        error: "Lỗi đường truyền mạng",
    },
    excelExport: {
        sheetName: "Tài sản",
        fileName: "Danh sách tài sản ",
        header: {
            fixed_asset_code: "Mã tài sản",
            fixed_asset_name: "Tên tài sản",
            fixed_asset_category_name: "Loại tài sản",
            department_name: "Bộ phận sử dụng",
            quantity: "Số lượng",
            cost: "Nguyên giá",
            life_time: "Số năm sử dụng",
            purchase_date: "Ngày mua",
            start_using_date: "Ngày sử dụng",
            accumulated_depreciation: "HM/KH lũy kế",
            remaining_value: "Giá trị còn lại",
        },
        exampleFileName: "Mẫu nhập liệu.xlsx",
    },
    assetsManagementIncrease: {
        title: "Ghi tăng tài sản",
        add: "Thêm",
        delete: "Xoá chứng từ",
        print: "In",
        options: "Tuỳ chọn",
        refresh: "Cập nhật dữ liệu",
        layout: "Bố cục",
        layoutOptions: {
            oneTable: "Một bảng",
            stacked: "Hai bảng",
            side_by_side: "option 2",
        },
        table: {
            index: "STT",
            indexTooltip: "Số thứ tự",
            assetCode: "Mã tài sản",
            assetName: "Tên tài sản",
            department: "Bộ phận sử dụng",
            cost: "Nguyên giá",
            depreciationValueYear: "Hao mòn năm",
            remainingValue: "Giá trị còn lại",
            ref_no: "Số chứng từ",
            ref_date: "Ngày chứng từ",
            increase_date: "Ngày ghi tăng",
            totalCost: "Tổng nguyên giá",
            description: "Nội dung",
        },
        detailTable: {
            title: "Thông tin chi tiết",
        },
        searchPlaceholder: "Tìm kiếm theo số chứng từ, nội dung",
        form: {
            addTitle: "Thêm chứng từ ghi tăng",
            editTitle: "Sửa chứng từ ghi tăng",
            saveBtn: "Lưu",
            cancelBtn: "Huỷ",
            detailTitle: "Thông tin chi tiết",
            inforTitle: "Thông tin chứng từ",
            voucherCode: "Mã chứng từ",
            voucherCodePlaceholer: "Nhập mã chứng từ",
            startUsingDate: "Ngày chứng từ",
            increaseDate: "Ngày ghi tăng",
            note: "Nội dung",
            selectAssets: "Chọn tài sản",
            searchPlaceholder: "Tìm kiếm theo mã, tên tài sản",
        },
        selectAssetsForm: {
            title: "Chọn tài sản ghi tăng",
            searchPlaceholder: "Tìm kiếm theo mã, tên tài sản",
        },
        editAssetForm: {
            title(asset) {
                return `Sửa tài sản ${asset}`;
            },
            department: "Bộ phận sử dụng",
            source: "Nguồn hình thành",
            cost: "Nguyên giá",
            value: "Giá trị",
            total: "Tổng",
            sourcePlaceholder: "Chọn nguồn hình thành",
            duplicateError(listBudgetName) {
                return `Tài sản này đã được ghi tăng từ nguồn <span class="bold-text">${listBudgetName.join(
                    ", "
                )}.</span> Vui lòng chọn nguồn khác.`;
            },
            add: "Thêm nguồn hình thành",
            delete: "Xoá nguồn hình thành",
        },
        dialog: {
            noAsset: "Chọn ít nhất 1 tài sản",
            increaseDateError: "Ngày ghi tăng phải lớn hơn hoặc bằng ngày chứng từ",
            closeAdd: "Bạn có muốn huỷ bỏ khai báo chứng từ này?",
            closeEdit:
                "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?",
        },
        toast: {
            add: "Thêm chứng từ thành công",
            edit: "Thay đổi chứng từ thành công",
            delete: "Xóa chứng từ thành công",
        },
    },
    table: {
        delete: "Xoá",
        edit: "Sửa",
    },
    login: {
        logo_text: "Đăng nhập để làm việc với <span class='medium'>MISA QLTS</span>",
        accountPlaceholder: "Tên đăng nhập, email hoặc số điện thoại",
        passwordPlaceholder: "Mật khẩu",
        login: "Đăng nhập",
        forgot_password: "Quên mật khẩu?",
        error_message: "Vui lòng nhập đầy đủ thông tin đăng nhập",
    },
};

export default MISAresources;
