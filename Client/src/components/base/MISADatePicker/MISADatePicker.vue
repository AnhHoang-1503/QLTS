<template>
    <div class="h-datepicker">
        <label @click="focus" :class="'h-datepicker__label'"
            >{{ label }} <span v-if="required">*</span></label
        >

        <div v-if="errormsg" class="h-datepicker__errormsg">{{ errormsg }}</div>
        <div class="h-datepicker__input" :class="errormsg ? 'h-datepicker--error' : ''">
            <ElConfigProvider :locale="locale">
                <ElDatePicker
                    v-model="inputValue"
                    type="date"
                    :placeholder="placeholder"
                    :tabindex="tabindex"
                    format="DD/MM/YYYY"
                    :clearable="false"
                    :default-value="new Date()"
                    :popper-options="{ boundariesElement: 'body' }"
                    :class="elDatePickerClass"
                    ref="elDatePicker"
                    @keydown="keyDownEvent($event)"
                >
                </ElDatePicker>
            </ElConfigProvider>
            <div v-if="icon" class="h-datepicker__icon">
                <MISAIcon :icon="icon" @click="focus"></MISAIcon>
            </div>
        </div>
    </div>
</template>

<style scoped>
@import url("./MISADatePicker.css");
</style>

<style>
@import url("./ELDatePicker.css");
</style>

<script>
import "element-plus/dist/index.css";
import { ElDatePicker, ElConfigProvider } from "element-plus";

import vi from "element-plus/dist/locale/vi.mjs";
import "dayjs/locale/vi";

vi.el.datepicker.year = "";

export default {
    name: "MISADatePicker",
    components: {
        ElDatePicker,
        ElConfigProvider,
    },
    data() {
        return {
            inputValue: this.modelValue, // giá trị người dùng nhập vào
            errormsg: this.modelErrormsg, // thông báo lỗi
            showDatePicker: null, // hiển thị bảng chọn ngày tháng
            elDatePickerClass: "",
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
        // bắt buộc nhập
        required: {
            type: Boolean,
            default: false,
        },
        value: {
            type: String,
            default: "",
        },
        // disable input
        disable: {
            type: Boolean,
            default: false,
        },
        // canh lề phải
        textRight: {
            type: Boolean,
            default: false,
        },
        icon: {
            type: String,
            default: "",
        },
        modelValue: {},
        // thông báo lỗi
        modelErrormsg: {
            type: String,
            default: "",
        },
        tabindex: {
            type: Number,
            default: 0,
        },
    },
    watch: {
        modelValue: function (value) {
            this.inputValue = value;
        },
        inputValue: function (value) {
            if (this.isValid()) {
                this.$emit("update:modelValue", value);
            }
        },
        modelErrormsg() {
            this.errormsg = this.modelErrormsg;
            console.log("model", this.modelErrormsg);
        },
        errormsg() {
            this.$emit("update:modelErrormsg", this.errormsg);
            console.log("msg", this.errormsg);
        },
    },
    methods: {
        /**
         * kiểm tra đã hợp lệ chưa
         * Author: vtahoang (04/07/2023)
         */
        isValid() {
            try {
                let value = this.inputValue;
                // error message khi validate dữ liệu
                this.errormsg = this.validator(value);
                // cập nhật lại error message
                this.$emit("update:modelErrormsg", this.errormsg);
                if (!this.errormsg) {
                    // không có lỗi thì cập nhật lại giá trị và xoá class error
                    this.$emit("update:modelValue", value);
                    this.elDatePickerClass = "";
                    return true;
                } else {
                    // có lỗi thì thêm class error
                    this.elDatePickerClass = "h-datepicker--error";
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
                if (value - new Date() > 0) {
                    return this.$_MISAResources.errorMsg.dateNotGreaterThanCurrentDate;
                }
                return "";
            } catch (error) {
                console.log(error);
                return "";
            }
        },
        /**
         * focus vào input
         * Author: vtahoang (01/08/2023)
         */
        focus() {
            try {
                this.$refs.elDatePicker.focus();
            } catch (error) {
                console.log("focus ~ error:", error);
            }
        },
        /**
         * Xử lý sự kiện keydown
         * @param {*} event event
         */
        keyDownEvent(event) {
            if (
                event.key === "ArrowUp" ||
                event.key === "ArrowRight" ||
                event.key === "ArrowLeft"
            ) {
                event.preventDefault();
                this.$refs.elDatePicker.handleOpen();
            }
        },
    },
    // Đổi ngôn ngữ datepicker
    setup() {
        return {
            locale: vi,
        };
    },
};
</script>
