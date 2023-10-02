<template>
    <div class="h-textfield">
        <label v-if="label" @click="focus" :class="disable ? '' : 'h-textfield__label'"
            >{{ label }} <span v-if="required">*</span></label
        >
        <div class="h-textfield__icon--loading" v-if="isLoading">
            <MISAIcon icon="btn--loading"></MISAIcon>
        </div>
        <div v-if="icon" class="h-textfield__icon">
            <MISAIcon :icon="icon"></MISAIcon>
            <div
                v-if="icon == 'up_down_arrows'"
                class="h-icon__arrow h-icon__arrow-up"
                @click="changeInputValue('increase')"
            ></div>
            <div
                v-if="icon == 'up_down_arrows'"
                class="h-icon__arrow h-icon__arrow-down"
                @click="changeInputValue('decrease')"
            ></div>
        </div>
        <input
            :textRight="textRight"
            :type="inputType"
            :placeholder="placeholder"
            :disabled="disable"
            v-model="inputValue"
            ref="input"
            :tabindex="tabindex"
            @keydown="changeValueWithKey($event)"
            @input="inputHandler(inputValue)"
            min="0"
            max="1000000000000"
            minlength="0"
            :maxlength="type == 'number' ? 15 : maxLength"
            @focusin="removeDefaultValue"
            @focusout="focusOutEvent"
        />
        <div v-if="errormsg" class="h-textfield__errormsg">{{ errormsg }}</div>
    </div>
</template>

<style scoped>
@import url(./MISATextfield.css);
</style>

<script>
export default {
    name: "MISATextfield",
    data() {
        var inputValue;
        switch (this.type) {
            case "number":
                inputValue = this.handler.numberHandler(this.modelValue);
                break;
            case "percent":
                inputValue = this.handler.numberHandler(this.modelValue, "float");
                break;
            default:
                inputValue = this.modelValue;
                break;
        }

        return {
            // giá trị input
            inputValue,
            // error message khi validate dữ liệu
            errormsg: this.modelErrormsg,
            // cờ kiểm tra giá trị cập nhật là tự động hay do nhập
            flag: false,
        };
    },
    props: {
        label: {
            type: String,
            default: "",
        },
        placeholder: {
            type: String,
            default: "",
        },
        required: {
            type: Boolean,
            default: false,
        },
        value: {
            type: String,
            default: "",
        },
        disable: {
            type: Boolean,
            default: false,
        },
        textRight: {
            type: Boolean,
            default: false,
        },
        icon: {
            type: String,
            default: "",
        },
        modelValue: {},
        modelErrormsg: {
            type: String,
            default: "",
        },
        number: {
            type: Boolean,
            default: false,
        },
        tabindex: {
            type: Number,
            default: -1,
        },
        // kiểu dữ liệu: text, number, percent
        type: {
            type: String,
            default: "text",
        },
        maxLength: {
            type: Number,
            default: 255,
        },
        // trạng thái loading
        isLoading: {
            type: Boolean,
            default: false,
        },
    },
    computed: {
        inputType() {
            switch (this.type) {
                case "number":
                    return "tel";
                case "percent":
                    return "tel";
                default:
                    return this.type;
            }
        },
    },
    methods: {
        /**
         * focus vào input
         * Author: vtahoang (04/07/2023)
         */
        focus() {
            try {
                this.$refs.input.focus();
                this.$refs.input.select();
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * kiểm tra đã hợp lệ chưa
         * Author: vtahoang (04/07/2023)
         */
        isValid() {
            try {
                let value = this.inputValue;
                if (this.type == "number") value = this.inputValue.toString().replaceAll(".", "");
                if (this.type == "percent") value = this.inputValue.toString().replaceAll(",", ".");
                // error message khi validate dữ liệu
                this.errormsg = this.validator(value);
                // cập nhật lại error message
                this.$emit("update:modelErrormsg", this.errormsg);
                // nếu không có input thì return false
                if (!this.$refs.input) return false;

                if (!this.errormsg) {
                    // không có lỗi thì xoá class error
                    this.$refs.input.classList.remove("h-textfield--error");
                    return true;
                } else {
                    // có lỗi thì thêm class error
                    this.$refs.input.classList.add("h-textfield--error");
                    return false;
                }
            } catch (error) {
                console.log(error);
                return false;
            }
        },
        /**
         * Validate dữ liệu
         * @param {String} value
         * Author: vtahoang (04/07/2023)
         */
        validator(value) {
            try {
                if (this.required && (value === "" || value == null || value == undefined)) {
                    return this.$_MISAResources.errorMsg.required;
                }
                if (this.type == "number" || this.type == "percent") {
                    if (value <= 0) return this.$_MISAResources.errorMsg.positiveNumber;
                }
                if (this.type == "percent") {
                    if (value > 100) return this.$_MISAResources.errorMsg.percent;
                }
                return "";
            } catch (error) {
                console.log(error);
                return "";
            }
        },
        /**
         * Tăng giảm giá trị với button
         * Author: vtahoang (12/07/2023)
         */
        changeValueWithKey(event) {
            try {
                // nếu input là rỗng thì gán giá trị mặc định là 0
                if ((this.type == "number" || this.type == "percent") && this.inputValue == "")
                    this.inputValue = 0;
                // nếu input là số thì tăng giảm giá trị
                if (this.type == "number") {
                    let value = this.inputValue.toString().replaceAll(".", "");
                    switch (event.key) {
                        case "ArrowUp":
                            event.preventDefault();
                            value = parseInt(value) + 1;
                            break;
                        case "ArrowDown":
                            event.preventDefault();
                            if (value > 0) value = parseInt(value) - 1;
                            break;
                        default:
                            break;
                    }

                    this.inputValue = this.handler.numberHandler(value);
                }
                // nếu input là số thì tăng giảm giá trị
                if (this.type == "percent") {
                    let value = this.inputValue.toString().replaceAll(",", ".");
                    switch (event.key) {
                        case "ArrowUp":
                            event.preventDefault();
                            value = parseFloat(value) + 1;
                            break;
                        case "ArrowDown":
                            event.preventDefault();
                            if (value > 0) value = parseFloat(value) - 1;
                            break;
                        default:
                            break;
                    }

                    this.inputValue = this.handler.numberHandler(value, "float");
                }
            } catch (error) {
                console.log("changeValueWithKey ~ error:", error);
            }
        },
        /**
         * Xử lý dữ liệu khi nhập số
         * Author: vtahoang (12/07/2023)
         */
        inputHandler(value) {
            try {
                if (this.type == "number" || this.type == "percent") {
                    // lưu lại vị trí con trỏ
                    let preCursorIndex = this.inputValue.length - this.$refs.input.selectionStart;
                    this.inputValue = this.handler.numberHandler(
                        value,
                        this.type == "percent" ? "float" : "int"
                    );
                    let newCursorIndex = this.inputValue.length - preCursorIndex;
                    // cập nhật lại vị trí con trỏ
                    this.$nextTick(() => {
                        this.$refs.input.selectionStart = newCursorIndex;
                        this.$refs.input.selectionEnd = newCursorIndex;
                    });
                }
            } catch (error) {
                console.log("inputHandler ~ error:", error);
            }
        },
        /**
         * Xóa giá trị mặc định
         * Author: vtahoang (29/07/2023)
         */
        removeDefaultValue() {
            try {
                this.flag = true;
                if (this.inputValue == "0") this.inputValue = "";
                this.$nextTick(() => {
                    this.flag = false;
                });
            } catch (error) {
                console.log("removeDefaultValue ~ error:", error);
            }
        },
        /**
         * Thay đổi giá trị input
         * @param {*} type tăng hoặc giảm (increase, decrease)
         */ changeInputValue(type) {
            // nếu input là số thì tăng giảm giá trị
            this.focus();
            // Xử lý cho number
            if ((this.type == "number" || this.type == "percent") && this.inputValue == "")
                this.inputValue = 0;
            if (this.type == "number") {
                let value = this.inputValue.toString().replaceAll(".", "");
                switch (type) {
                    case "increase":
                        value = parseInt(value) + 1;
                        break;
                    case "decrease":
                        if (value > 0) value = parseInt(value) - 1;
                        break;
                    default:
                        break;
                }
                this.inputValue = this.handler.numberHandler(value);
            }
            // Xử lý cho phần trăm
            if (this.type == "percent") {
                let value = this.inputValue.toString().replaceAll(",", ".");
                switch (type) {
                    case "increase":
                        value = parseFloat(value) + 1;
                        break;
                    case "decrease":
                        if (value > 0) value = parseFloat(value) - 1;
                        break;
                    default:
                        break;
                }
                this.inputValue = this.handler.numberHandler(value);
            }
        },
        /**
         * Xử lý sự kiện focus out
         * Author: vtahoang (29/07/2023)
         */
        focusOutEvent() {
            try {
                // thêm 2 số sau  dấu phẩy
                if (this.type == "percent") {
                    if (this.inputValue != 0) {
                        let value = this.inputValue.toString().replaceAll(",", ".");
                        value = parseFloat(value).toFixed(2);
                        this.inputValue = this.handler.numberHandler(value, "float");
                    }
                }
                // validate dữ liệu
                this.isValid();
            } catch (error) {
                console.log("focusOutEvent ~ error:", error);
            }
        },
    },
    watch: {
        inputValue() {
            if (!this.flag && this.isValid()) {
                this.flag = true;
                // chuyển đổi kiểu dữ liệu và gán giá trị cho model
                switch (this.type) {
                    case "number":
                        this.$emit(
                            "update:modelValue",
                            parseInt(this.inputValue.toString().replaceAll(".", ""))
                        );
                        break;
                    case "percent":
                        this.$emit(
                            "update:modelValue",
                            parseFloat(this.inputValue.toString().replaceAll(",", "."))
                        );
                        break;
                    default:
                        this.$emit("update:modelValue", this.inputValue);
                        break;
                }
                this.$nextTick(() => {
                    this.flag = false;
                });
            }
        },
        modelValue(value) {
            if (!this.flag) {
                // chuyển đổi kiểu dữ liệu và gán giá trị cho input
                if (this.type == "number" || this.type == "percent") {
                    this.inputValue = this.handler.numberHandler(
                        value,
                        this.type == "percent" ? "float" : "int"
                    );
                } else this.inputValue = value;
            }
        },
        modelErrormsg(value) {
            // kiểm tra error message
            this.errormsg = value;
            if (value) this.$refs.input.classList.add("h-textfield--error");
            else this.$refs.input.classList.remove("h-textfield--error");
        },
    },
};
</script>
