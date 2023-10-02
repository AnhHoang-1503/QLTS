<template>
    <div class="h-paging__container unselectable">
        <div class="h-paging__recordPerPage">
            <MISADropdown
                placeholder="20"
                :iconRight="'expand'"
                :dataList="$_MISAResources.recordPerPage"
                :dataTop="true"
                :showCheck="false"
                :inputDisable="true"
                :style="{ height: '26px', 'font-size': '12px' }"
                :itemHeight="26"
                v-model="recordPerPageValue"
                :defaultValue="20"
            />
        </div>
        <!-- page list nếu chỉ có 1 trang -->
        <div class="h-paging__pageList" v-if="totalPage <= 0">
            <div class="h-pageblock__backicon h-pageblock--disabled">
                <MISAIcon :icon="'back__black'"></MISAIcon>
            </div>
            <div class="h-pageblock__page h-pageblock__page--selected">1</div>
            <div class="h-pageblock__nexticon h-pageblock--disabled">
                <MISAIcon :icon="'next__black'"></MISAIcon>
            </div>
        </div>
        <!-- page list nếu có khả năng hiển thị hết -->
        <div class="h-paging__pageList" v-if="totalPage <= 7 && totalPage > 0">
            <div class="h-pageblock__backicon" :class="isDisableDecrease" @click="decrease">
                <MISAIcon :icon="'back__black'"></MISAIcon>
            </div>
            <div
                class="h-pageblock__page"
                v-for="n in totalPage"
                @click="select(n)"
                :class="selectedPage(n)"
            >
                {{ n }}
            </div>
            <div class="h-pageblock__nexticon" :class="isDisableIncrease" @click="increase">
                <MISAIcon :icon="'next__black'"></MISAIcon>
            </div>
        </div>
        <div class="h-paging__pageList" v-if="totalPage > 7">
            <div class="h-pageblock__backicon" :class="isDisableDecrease" @click="decrease">
                <MISAIcon :icon="'back__black'"></MISAIcon>
            </div>
            <div class="h-pageblock__page" @click="select(1)" :class="selectedPage(1)">1</div>
            <div
                class="h-pageblock__page"
                @click="select(afterFirst, 'afterFirst')"
                :class="selectedPage(afterFirst)"
            >
                {{ afterFirst }}
            </div>
            <div
                class="h-pageblock__page"
                @click="select(middle - 1)"
                :class="selectedPage(middle - 1)"
            >
                {{ middle - 1 }}
            </div>
            <div class="h-pageblock__page" @click="select(middle)" :class="selectedPage(middle)">
                {{ middle }}
            </div>
            <div
                class="h-pageblock__page"
                @click="select(middle + 1)"
                :class="selectedPage(middle + 1)"
            >
                {{ middle + 1 }}
            </div>
            <div
                class="h-pageblock__page"
                @click="select(beforeLast, 'beforeLast')"
                :class="selectedPage(beforeLast)"
            >
                {{ beforeLast }}
            </div>
            <div
                class="h-pageblock__page"
                @click="select(totalPage)"
                :class="selectedPage(totalPage)"
            >
                {{ totalPage }}
            </div>

            <div class="h-pageblock__nexticon" :class="isDisableIncrease" @click="increase">
                <MISAIcon :icon="'next__black'"></MISAIcon>
            </div>
        </div>
    </div>
</template>

<style scoped>
@import url(./MISAPaging.css);
</style>

<script>
import MISADropdown from "../MISADropdown/MISADropdown.vue";

export default {
    name: "MISAPaging",
    components: {
        MISADropdown,
    },
    props: {
        totalRecords: { type: Number, default: 0 }, // Tổng số bản ghi
        recordPerPage: { Number, default: 20 }, // Số bản ghi trên 1 trang
        currentPage: { Number, default: 1 }, // Trang hiện tại
    },
    data() {
        return {
            recordPerPageValue: this.recordPerPage, // Số bản ghi trên 1 trang
            currentPageValue: this.currentPage, // Trang hiện tại
            totalPage: Math.ceil(this.totalRecords / this.recordPerPage), // Tổng số trang
            beforeLast: "...", // Ký tự hiển thị trước trang cuối
            afterFirst: 2, // Ký tự hiển thị sau trang đầu
            middle: 4, // Ký tự hiển thị ở giữa
        };
    },
    watch: {
        recordPerPageValue(value) {
            this.$emit("update:recordPerPage", value);
        },
        currentPageValue(value) {
            this.$emit("update:currentPage", value);
            if (value >= 4 && value <= this.totalPage - 3) {
                this.middle = value;
            } else if (value < 4) {
                this.middle = 4;
            } else if (value > this.totalPage - 3) {
                this.middle = this.totalPage - 3;
            }
        },
        totalRecords(value) {
            this.totalPage = Math.ceil(value / this.recordPerPageValue);
            this.currentPageValue = 1;

            // cập nhật giá trị trước cuối và sau đầu
            if (this.middle <= 4) {
                this.afterFirst = 2;
            } else {
                this.afterFirst = "...";
            }
            if (this.middle >= this.totalPage - 3) {
                this.beforeLast = this.totalPage - 1;
            } else {
                this.beforeLast = "...";
            }
        },
        recordPerPage(value) {
            this.recordPerPageValue = value;
            this.totalPage = Math.ceil(this.totalRecords / this.recordPerPageValue);
            this.currentPageValue = 1;
            this.middle = 4;

            // cập nhật giá trị trước cuối và sau đầu
            if (this.middle <= 4) {
                this.afterFirst = 2;
            } else {
                this.afterFirst = "...";
            }
            if (this.middle >= this.totalPage - 3) {
                this.beforeLast = this.totalPage - 1;
            } else {
                this.beforeLast = "...";
            }
        },
        middle(value) {
            if (value <= 4) {
                this.afterFirst = 2;
            } else {
                this.afterFirst = "...";
            }
            if (value >= this.totalPage - 3) {
                this.beforeLast = this.totalPage - 1;
            } else {
                this.beforeLast = "...";
            }
        },
    },
    methods: {
        /**
         * Sang trang tiếp theo
         * Author: vtahoang (11/07/2021)
         */
        increase() {
            try {
                if (this.currentPage < this.totalPage) this.currentPageValue += 1;
            } catch (error) {
                console.log("increase ~ error:", error);
            }
        },
        /**
         * Về trang trước đó
         * Author: vtahoang (11/07/2021)
         */
        decrease() {
            try {
                if (this.currentPage > 1) this.currentPageValue -= 1;
            } catch (error) {
                console.log("decrease ~ error:", error);
            }
        },
        /**
         * Tô viền page hiện tại
         * @param {*} page
         * Author: vtahoang (11/07/2021)
         */
        selectedPage(page) {
            try {
                if (page === this.currentPage) return "h-pageblock__page--selected";
                return "";
            } catch (error) {
                console.log("selectedPage ~ error:", error);
            }
        },
        /**
         * Chọn page click vào
         * @param {*} page
         * @param {string} position vị trí click vào "afterFirst" hoặc "beforeLast"
         * Author: vtahoang (11/07/2021)
         */
        select(page, position = "") {
            try {
                if (page === "...") {
                    switch (position) {
                        case "afterFirst":
                            this.middle = this.middle - 3;
                            if (this.middle <= 4) this.middle = 4;
                            break;
                        case "beforeLast":
                            this.middle = this.middle + 3;
                            if (this.middle >= this.totalPage - 3) this.middle = this.totalPage - 3;
                            break;
                        default:
                            break;
                    }
                    return;
                }
                this.currentPageValue = page;
            } catch (error) {
                console.log("select ~ error:", error);
            }
        },
    },
    computed: {
        isDisableIncrease() {
            return this.currentPage >= this.totalPage ? "h-pageblock--disabled" : "";
        },
        isDisableDecrease() {
            return this.currentPage <= 1 ? "h-pageblock--disabled" : "";
        },
    },
};
</script>
