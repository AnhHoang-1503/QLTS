/**
 * Directive xử lý sự kiện click ngoài element
 * @param {*} el element
 * @param {*} binding {value: function xử lý sự kiện clickOutsideEvent}
 * Author: vtahoang (05/08/2023)
 */
function beforeMount(el, binding) {
    el.clickOutsideEvent = function (event) {
        // nếu sự kiện click không nằm trong element và các element con của nó thì gọi hàm binding.value
        if (!(el === event.target || el.contains(event.target))) {
            binding.value();
        }
    };

    // thêm sự kiện click cho document khi directive được mounted
    document.body.addEventListener("click", el.clickOutsideEvent);
    document.body.addEventListener("contextmenu", el.clickOutsideEvent);
}

/**
 * Xoá sự kiện click cho document khi directive bị unmounted
 * @param {*} el element
 * Author: vtahoang (05/08/2023)
 */
function unmounted(el) {
    // xóa sự kiện click cho document khi directive bị unmounted
    document.body.removeEventListener("click", el.clickOutsideEvent);
    document.body.removeEventListener("contextmenu", el.clickOutsideEvent);
}

export default { beforeMount, unmounted };
