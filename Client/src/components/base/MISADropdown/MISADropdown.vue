<template>
    <div
        ref="container"
        class="dropdown__container"
        :tabindex="-1"
        @focusout="hideDataList"
        :style="style"
        @keydown="selectWithKey($event)"
        @mousemove="dataFocus = -1"
    >
        <label v-if="label" :class="inputDisable ? '' : 'h-dropdown__label'" @click="focus"
            >{{ label }} <span v-if="required">*</span></label
        >
        <div ref="dropdown" class="h-dropdown" @click="toggleDataList">
            <div v-if="iconLeft" class="h-dropdown__icon">
                <MISAIcon :icon="iconLeft" />
            </div>
            <input
                ref="input"
                class="h-dropdown__current"
                :placeholder="placeholder"
                v-model="currentValue"
                :tabindex="tabindex"
                :disabled="inputDisable"
                @focus="showDataList"
                @focusout="hideDataList"
            />
            <div v-if="iconRight" class="h-dropdown__expand">
                <MISAIcon :icon="iconRight" />
            </div>
            <div v-if="errormsg" class="h-dropdown__errormsg">{{ errormsg }}</div>
        </div>
        <Teleport to="#app">
            <div class="h-dataList__conatiner" v-show="showList" :style="styleDataList">
                <div
                    class="h-dropdown__dataList"
                    @mouseover="mouseover = true"
                    @mouseleave="mouseover = false"
                    ref="dataList"
                >
                    <div
                        class="h-dataList__item"
                        :class="selectedItem(item, index)"
                        v-for="(item, index) in dataListShown"
                        :key="index"
                        @click="itemClick(item)"
                        :style="itemStyle"
                    >
                        <div v-if="showCheck" class="h-item__check">
                            <div class="check-icon"></div>
                        </div>
                        <div class="h-item__name">{{ item }}</div>
                    </div>
                    <div
                        class="h-dataList__item h-item__empty"
                        v-if="empty"
                        @click="itemClick('empty')"
                    >
                        <div class="h-item__name">{{ $_MISAResources.dropdown.emptyItem }}</div>
                    </div>
                    <div class="h-dataList__item h-item__loading" v-if="isLoading">
                        <MISAIcon
                            icon="loading"
                            :style="{ width: '40px', height: '40px' }"
                        ></MISAIcon>
                    </div>
                </div>
            </div>
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./MISADropdown.css);
</style>

<script>
export default {
    name: "MISADropdown",
    components: {},
    data() {
        let currentValue;
        if (this.modelValue) {
            currentValue = this.modelValue;
        }
        return {
            showList: false, //hiển thị data list
            currentValue, // giá trị hiện tại của dropdown
            mouseover: false, // chuột đang trên data list
            errormsg: "", // thông báo lỗi
            dataListShown: this.dataList, // danh sách hiển thị
            // giá trị lọc
            filterValue: "",
            // danh sách hiển thị trống
            empty: false,
            // index dữ liệu đang được focus
            dataFocus: -1,
            // style của item
            itemStyle: {
                flex: `0 0 ${this.itemHeight}px`,
            },
            // trạng thái loading
            isLoading: true,
            // cờ focus
            inputFocus: false,
            styleDataList: {
                top: 0,
                left: "",
                width: "",
            },
        };
    },
    props: {
        // nhãn
        label: {
            type: String,
            default: "",
        },
        // bắt buộc nhập
        required: {
            type: Boolean,
            default: false,
        },
        // placeholder
        placeholder: {
            type: String,
            default: "",
        },
        // danh sách dữ liệu
        dataList: {
            type: Object,
            default: () => {},
        },
        modelValue: "",
        // icon trái
        iconLeft: {
            type: String,
            default: "",
        },
        // icon phải
        iconRight: {
            type: String,
            default: "",
        },
        // thứ tự tab
        tabindex: {
            type: Number,
            default: -1,
        },
        //data list hiện lên trên
        dataTop: {
            type: Boolean,
            default: false,
        },
        //hiện check trên dataList
        showCheck: {
            type: Boolean,
            default: true,
        },
        //disable input
        inputDisable: {
            type: Boolean,
            default: false,
        },
        //style
        style: {
            type: Object,
            default: () => {},
        },
        // height của item
        itemHeight: {
            type: Number,
            default: null,
        },
        //giá trị mặc định khi không có dữ liệu
        defaultValue: {
            default: null,
        },
        // Danh sách giá trị ban đầu
        originalList: {
            type: Array,
            default: () => [],
        },
    },
    watch: {
        currentValue(value) {
            // Kiểm tra xem giá trị hợp lệ chưa
            if (this.isValid()) {
                if (Object.values(this.dataList).includes(value) || value == null) {
                    // giá trị hợp lệ
                    this.$emit("update:modelValue", value);
                }
            }
            //cập nhật lại danh sách hiển thị
            if (Object.values(this.dataListShown).includes(value)) {
                this.filterValue = "";
                this.mouseover = true;
            } else {
                this.mouseover = false;
                this.filterValue = value;
            }
            this.shownList();
        },
        modelValue(value) {
            this.currentValue = value;
        },
        dataList(value) {
            this.dataListShown = value;
        },
        showList() {
            let inputPosition = this.$refs.input.getBoundingClientRect();

            if (this.dataTop) {
                this.styleDataList.top = inputPosition.top - 24 * 7 + 8 + "px";
            } else {
                this.styleDataList.top = inputPosition.bottom + 8 + "px";
            }
            this.styleDataList.left = inputPosition.left + "px";
            this.styleDataList.width = this.$refs.input.offsetWidth + "px";
        },
    },
    methods: {
        /**
         * Hiển thị danh sách dữ liệu khi click vào dropdown
         * Author: vtahoang (04/08/2023)
         */
        toggleDataList() {
            this.showDataList();
            // if (!this.inputFocus) {
            // } else {
            //     this.showList = !this.showList;
            // }
        },
        /**
         * Xử lý khi click vào 1 item trong danh sách
         * @param {*} item thẻ được click
         * Author: vtahoang (04/07/2023)
         */
        itemClick(item) {
            try {
                // nếu item được chọn là item hiện tại hoặc ô dữ liệu trống thì xoá giá trị
                if (this.currentValue == item || item == "empty")
                    this.currentValue = this.defaultValue;
                // ngược lại thì cập nhật lại giá trị
                else {
                    this.currentValue = item;
                }
                //ẩn danh sách
                this.showList = false;
                this.mouseover = false;
            } catch (error) {
                console.log("itemClick ~ error:", error);
            }
        },
        /**
         * Hiển thị danh sách dữ liệu khi focus vào input
         * Author: vtahoang (04/07/2023)
         */
        showDataList() {
            try {
                this.$refs.input.focus();
                this.showList = true;
                // focus vào ô input
                setTimeout(() => {
                    this.inputFocus = true;
                }, 200);
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * Ẩn danh sách dữ liệu khi click ra ngoài
         * Author: vtahoang (04/07/2023)
         */
        hideDataList() {
            try {
                if (!this.mouseover) {
                    // đặt các giá trị về mặc định
                    this.showList = false;
                    this.dataFocus = -1;
                    this.inputFocus = false;
                    // validate dữ liệu
                    this.isValid();
                }
            } catch (error) {
                console.log("hideDataList ~ error:", error);
            }
        },
        /**
         * Validate dữ liệu
         * @param {String} value giá trị của input
         * Author: vtahoang (04/07/2023)
         */
        validator(value) {
            try {
                if (this.required && (value == "" || value == null || value == undefined)) {
                    return this.$_MISAResources.errorMsg.required;
                }
                if (this.required && !Object.values(this.dataList).includes(value)) {
                    if (!this.originalList.includes(value))
                        return this.$_MISAResources.errorMsg.invalid;
                }

                return "";
            } catch (error) {
                console.log(error);
                return "";
            }
        },
        /**
         * kiểm tra đã hợp lệ chưa
         * Author: vtahoang (04/07/2023)
         */
        isValid() {
            try {
                let value = this.currentValue;
                // error message khi validate dữ liệu
                this.errormsg = this.validator(value);
                // cập nhật lại error message
                this.$emit("update:modeErrormsg", this.errormsg);
                if (!this.errormsg) {
                    // không có lỗi thì cập nhật lại giá trị và xoá class error
                    if (Object.values(this.dataList).includes(value) || value == "") {
                        // giá trị hợp lệ
                        this.$emit("update:modelValue", value);
                    }
                    this.$refs.container.classList.remove("h-dropdown--error");
                    return true;
                } else {
                    // có lỗi thì thêm class error
                    this.$refs.container.classList.add("h-dropdown--error");
                    return false;
                }
            } catch (error) {
                console.log(error);
                return false;
            }
        },
        /**
         *  Kiểm tra item có được chọn hay không
         * @param {*} value gias trị của item
         * Author: vtahoang (04/07/2023)
         */
        selectedItem(value, index) {
            let addClass = "";
            // focus khi di chuyển bằng phím
            if (index == this.dataFocus) {
                addClass += "focus ";
            }
            // hiển thị tick được chọn
            if (value == this.currentValue) {
                addClass += "selected";
                // không hiển thị tick thì thêm background
                if (!this.showCheck) addClass += " selected--nocheck";
            }
            return addClass;
        },
        /**
         * lọc danh sách chọn
         * Author: vtahoang (09/07/2023)
         */
        shownList() {
            try {
                // lọc danh sách hiển thị theo giá trị nhập vào
                if (
                    this.filterValue != "" &&
                    this.filterValue != null &&
                    this.filterValue != undefined
                ) {
                    this.dataListShown = Object.values(this.dataList).filter((item) => {
                        return item
                            .toString()
                            .toLowerCase()
                            .includes(this.filterValue.toString().toLowerCase());
                    });
                } else this.dataListShown = this.dataList;
                // Hiện thị item trống nếu danh sách hiển thị rỗng
                if (this.dataListShown.length <= 0) {
                    this.empty = true;
                } else this.empty = false;
            } catch (error) {
                console.log("shownList ~ error:", error);
            }
        },
        /**
         * Di chuyển với button
         * Author: vtahoang (11/07/2023)
         */
        selectWithKey(event) {
            try {
                switch (event.key) {
                    case "ArrowDown":
                        event.stopPropagation();
                        if (this.dataFocus < Object.values(this.dataListShown).length - 1) {
                            this.dataFocus++;
                        }
                        break;
                    case "ArrowUp":
                        event.stopPropagation();
                        if (this.dataFocus > 0) {
                            this.dataFocus--;
                        }
                        break;
                    case "Enter":
                        let data = Object.values(this.dataListShown)[this.dataFocus];
                        if (this.currentValue == data) {
                            this.currentValue = this.defaultValue;
                        } else
                            this.currentValue = Object.values(this.dataListShown)[this.dataFocus];
                        this.showList = !this.showList;
                        break;
                    case "Tab":
                        this.showList = false;
                        break;
                    default:
                        break;
                }

                this.$refs.dataList.scrollTop = (this.dataFocus - 3) * 36;
            } catch (error) {
                console.log("selectWithKey ~ error:", error);
            }
        },
        /**
         * Focus vào ô input
         * Author: vtahoang (30/07/2023)
         */
        focus() {
            try {
                this.$refs.input.focus();
                this.$refs.input.select();
            } catch (error) {
                console.log("focus ~ error:", error);
            }
        },
    },
    updated() {
        if (Object.keys(this.dataList).length > 0) {
            this.isLoading = false;
        }
    },
};
</script>
