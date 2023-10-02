<template>
    <div
        class="h-sort"
        ref="container"
        @mouseover="mouseOver = true"
        @mouseleave="mouseOver = false"
    >
        <div class="h-sort__content">
            <slot name="content"></slot>
        </div>
        <Teleport to="#app">
            <div
                class="h-sort__btn"
                :style="style"
                :class="showSortBtn ? 'h-sort__btn--show' : ''"
                @mouseover="mouseOver = true"
                @mouseleave="mouseOver = false"
                @click="status = (status + 1) % 3"
            >
                <div class="h-sort__ASC" v-show="!(status == 2)">
                    <MISAIcon icon="arrow-up" />
                </div>
                <div class="h-sort__DESC" v-show="!(status == 1)">
                    <MISAIcon icon="arrow-down" />
                </div>
            </div>
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./MISASort.css);
</style>

<script>
import MISAIcon from "../MISAIcon/MISAIcon.vue";
import { reactive } from "vue";

export default {
    name: "MISASort",
    components: { MISAIcon },
    methods: {
        /**
         * Cập nhật vị trí của button sort
         * Author: vtahoang (01/08/2023)
         */
        updatePosition() {
            try {
                let parentPosition = this.$refs.container.getBoundingClientRect();
                this.style.left =
                    parentPosition.left + this.$refs.container.offsetWidth - 12 + "px";
                this.style.top =
                    parentPosition.bottom - this.$refs.container.offsetHeight / 2 + "px";
            } catch (error) {
                console.log("updatePosition ~ error:", error);
            }
        },
    },
    props: {
        alignment: {
            type: String,
            default: "left",
        },
        sortField: {
            type: String,
            default: "",
        },
        sortType: {
            type: String,
            default: "ASC",
        },
        value: {
            type: String,
            default: "",
        },
    },
    data() {
        return {
            style: {
                left: "",
                top: "",
                right: "",
                bottom: "",
            },
            mouseOver: false,
            // 0 - chưa sắp xếp, 1 - sắp xếp tăng dần, 2 - sắp xếp giảm dần
            status: 0,
            // cờ kiểm tra cập nhật
            flag: false,
        };
    },
    watch: {
        /**
         * Cập nhật trạng thái sort nếu thay đổi từ bên trong
         * Author: vtahoang (08/08/2023)
         */
        status(value) {
            // cập nhật kiểu sort
            switch (value) {
                case 1:
                    this.$emit("update:sortField", this.value);
                    this.$emit("update:sortType", "ASC");
                    break;
                case 2:
                    this.$emit("update:sortField", this.value);
                    this.$emit("update:sortType", "DESC");
                    break;
                case 0:
                    // chỉ cập nhật nếu do thay đổi bên trong
                    if (!this.flag) {
                        this.$emit("update:sortField", null);
                        this.$emit("update:sortType", null);
                    }
                    break;
                default:
                    break;
            }
        },
        /**
         * Cập nhật lại trạng thái sort nếu thay đổi từ bên ngoài
         * Author: vtahoang (08/08/2023)
         */
        sortField(value) {
            // cập nhật trường sort
            if (value != this.value) {
                this.flag = true;
                this.status = 0;
                this.$nextTick(() => {
                    this.flag = false;
                });
            }
        },
    },
    computed: {
        /** Hiện thị button sort nếu hover hoặc sắp xếp
         * Author: vtahoang (08/08/2023)
         */
        showSortBtn() {
            return this.status !== 0 || this.mouseOver;
        },
    },
    updated() {
        this.updatePosition();
    },
    mounted() {
        this.updatePosition();
    },
};
</script>
