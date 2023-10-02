/**
 * Directive tự động focus khi focus vào body
 * Author: vtahoang (05/08/2023)
 */
function updated(el) {
    if (document.activeElement.tagName == "BODY") {
        el.focus();
    }
}

/**
 * Directive tự động focus khi focus vào body
 * Author: vtahoang (05/08/2023)
 */
function mounted(el) {
    if (document.activeElement.tagName == "BODY") {
        el.focus();
    }
}

export default { updated, mounted };
