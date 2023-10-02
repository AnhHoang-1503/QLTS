/**
 * Xử lý dữ liệu số
 * @param {Number} value giá trị cần xử lý
 * @param {String} type kiểu dữ liệu cần xử lý (int || float)
 * Author: vtahoang - (23/06/2023)
 */
function numberHandler(value, type = "int") {
    try {
        // Xử lý dữ liệu số nguyên
        if (value != null && type == "int") {
            value = value.toString();
            // xoá các ký tự không phải số
            value = value.replace(/[^0-9]/g, "");
            // xoá số 0 đầu tiên
            if (value.length > 1) value = value.replace(/^0+/, "");
            var parts = value.split(".");
            //thêm dấu . sau mỗi 3 số
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ".");
            let result = parts.join(".");
            return result;
        }
        // Xử lý dữ liệu số thực
        if (value != null && type == "float") {
            value = value.toString();

            // xoá số 0 đầu tiên
            if (value.length > 1) value = value.replace(/^0+/, "");

            // thay dấu . thành ,
            value = value.replaceAll(".", ",");

            // lấy index của dấu ,
            const lastCommaIndex = value.lastIndexOf(",");

            // xoá các ký tự không phải số
            value = value.replace(/[^0-9]/g, "");

            // thêm trả lại dấu ,
            if (lastCommaIndex != -1) {
                value =
                    value.slice(0, lastCommaIndex) +
                    "," +
                    value.slice(lastCommaIndex, lastCommaIndex + 2);
            }

            let result = value;
            if (result[0] == ",") result = "0" + result;
            return result;
        }
        return "0";
    } catch (error) {
        console.log(error);
        return "";
    }
}

/**
 * Không nhận event từ thẻ cha
 * Author: vtahoang (06/07/2023)
 */
function stopPropagation(event) {
    try {
        event.stopPropagation();
    } catch (error) {
        console.log("stopPropagation ~ error:", error);
    }
}

/**
 * Tạo chuỗi ngẫu nhiên
 * @param {*} length chiều dài chuỗi
 * @returns chuỗi ngẫu nhiên
 * Author: vtahoang (06/07/2023)
 */
function makeid(length) {
    let result = "";
    const characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    const charactersLength = characters.length;
    let counter = 0;
    while (counter < length) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
        counter += 1;
    }
    return result;
}

/**
 * Hàm lấy index của số cuối trong chuỗi
 * @param {*} str
 * @returns index của số cuối cùng trong str
 */
function getLastNumberIndex(str) {
    const regex = /\d/g; // Match với bất kỳ số nào
    let lastNumberIndex = -1;
    let match;
    // tìm số cuối
    while ((match = regex.exec(str)) !== null) {
        lastNumberIndex = match.index;
    }

    return lastNumberIndex + 1;
}

/**
 * xử lý dữ liệu text
 * @param {*} value text
 * @param {int} maxLength độ dài tối đa
 * @returns text đã được xử lý
 */
function textHandler(value, maxLength = 15) {
    if (value.length > maxLength) return value.slice(0, maxLength) + "...";
    return value;
}

/**
 * Chuyển date object thành ISOString
 * @param {Date} value Định dạng Date object
 * @returns {string} Định dạng ISOString
 * Author: vtahoang (26/07/2023)
 */
function toISOStringHandler(value) {
    value = new Date(value).getTime();
    var tzoffset = new Date().getTimezoneOffset() * 60000; //offset in milliseconds
    var localISOTime = new Date(value - tzoffset).toISOString();
    return localISOTime;
}

/**
 * Lấy ngày giờ hiện tại
 * @returns {String} Ngày giờ hiện tại dạng dd-mm-yyyy hh:mm:ss
 */
function getCurrentDateTime() {
    var today = new Date();
    var date = today.getDate() + "-" + (today.getMonth() + 1) + "-" + today.getFullYear();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var dateTime = date + " " + time;
    return dateTime;
}

/**
 * Chuyển về dạng string date
 * @param {*} date
 */
function toDateTime(isoString) {
    const date = new Date(isoString);
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0"); // Tháng trong JavaScript bắt đầu từ 0
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
}

/**
 * Hàm debounce
 * @param {*} func callback
 * @param {*} timeout thời gian chờ
 * @returns
 * Author: vtahoang (25/08/2023)
 */
function debounce(func, timeout = 300) {
    let timer;
    return (...args) => {
        clearTimeout(timer);
        timer = setTimeout(() => {
            func.apply(this, args);
        }, timeout);
    };
}

export default {
    numberHandler,
    stopPropagation,
    makeid,
    getLastNumberIndex,
    textHandler,
    toISOStringHandler,
    getCurrentDateTime,
    toDateTime,
    debounce,
};
