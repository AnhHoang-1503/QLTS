import enums from "@/helper/enums.js";

/**
 * Lấy ra danh sách element có thuộc tính tabindex
 * @param {*} element element được gắn directive
 * @returns danh sách các element có thuộc tính tabindex
 * Author: vtahoang (30/07/2023)
 */
function getInputElements(element) {
    let inputElements = element.querySelectorAll("[tabindex]:not([tabindex='-1'])");
    // sắp xếp theo thứ tự index
    inputElements = Array.from(inputElements).sort((a, b) => a.tabIndex - b.tabIndex);

    return inputElements;
}

/**
 * Directive focus loop
 * @param {*} element element được gắn directive
 * @param {*} binding {value: index của element focus đầu tiên}
 * Author: vtahoang (30/07/2023)
 */
function mounted(element, binding) {
    // lấy tất cả các element có thuộc tính tabindex
    let inputElements = getInputElements(element);
    if (inputElements.length == 0) return;
    // có giá trị truyền vào thì focus ô đó
    if (typeof binding.value == "number") {
        inputElements[binding.value].focus();
    } else {
        // không thì focus element
        element.focus();
    }

    // sự kiện keydown tab
    const tabEvent = new KeyboardEvent("keydown", { key: enums.keyCode.tab });

    inputElements.forEach((inputElement, index) => {
        inputElement.addEventListener("keydown", (event) => {
            if (event.key == "Tab") {
                event.preventDefault();
                // shift + tab
                if (event.shiftKey) {
                    if (index == 0) {
                        // disable thì foscus phần tử tiếp theo
                        if (inputElements[inputElements.length - 1].disabled)
                            inputElements[inputElements.length - 1].dispatchEvent(tabEvent);
                        else inputElements[inputElements.length - 1].focus();
                    } else {
                        // disable thì foscus phần tử tiếp theo
                        if (inputElements[index - 1].disabled)
                            inputElements[index - 1].dispatchEvent(tabEvent);
                        else inputElements[index - 1].focus();
                    }
                }
                //tab
                else {
                    if (index == inputElements.length - 1) {
                        // disable thì foscus phần tử tiếp theo
                        if (inputElements[0].disabled) inputElements[0].dispatchEvent(tabEvent);
                        else inputElements[0].focus();
                    } else {
                        // disable thì foscus phần tử tiếp theo
                        if (inputElements[index + 1].disabled)
                            inputElements[index + 1].dispatchEvent(tabEvent);
                        else inputElements[index + 1].focus();
                    }
                }
            }
        });
    });
}

export default { mounted };
