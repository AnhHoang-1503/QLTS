<template>
    <div>
        <Teleport to="#app">
            <div class="h-dialog" @keydown="keyDownEvent($event)" :tabindex="-1" v-focusloop="0">
                <div class="h-dialog__container">
                    <div class="h-dialog__header"></div>
                    <div class="h-dialog__body">
                        <div class="h-body__icon">
                            <MISAIcon :icon="'warning_dialog'"></MISAIcon>
                        </div>
                        <div class="h-body__message">
                            <div class="h-message__message" v-html="message"></div>
                            <div class="h-message__detail">
                                <div
                                    class="h-detail__toggle unselectable"
                                    @click="showDetail = !showDetail"
                                >
                                    {{ showDetail ? detailToggleText[0] : detailToggleText[1] }}
                                </div>
                                <div class="h-detail__content" v-if="showDetail">
                                    <div
                                        class="h-content__item"
                                        v-for="(item, index) in detailData"
                                        v-html="item"
                                    ></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="h-dialog__footer">
                        <div class="h-footer__btn">
                            <slot name="left-btn"></slot>
                        </div>
                        <div class="h-footer__btn">
                            <slot name="middle-btn"></slot>
                        </div>
                        <div class="h-footer__btn">
                            <slot name="right-btn"></slot>
                        </div>
                    </div>
                </div>
            </div>
        </Teleport>
    </div>
</template>

<style scoped>
@import url("./MISADialog.css");
</style>

<script>
export default {
    name: "MISADialog",
    data() {
        return {};
    },
    props: {
        // text trong dialog
        message: {
            type: String,
            default: "",
        },
        // text ẩn hiện detail dialog [ẩn, hiện]
        detailToggleText: {
            type: Array,
            default: () => [],
        },
        showDetail: {
            type: Boolean,
            default: false,
        },
        detailData: {
            type: Array,
            default: () => [],
        },
    },
    methods: {
        // trở lại
        cancel() {
            this.$emit("cancel");
        },
        // xác nhận
        accept() {
            this.$emit("accept");
        },
        /**
         * Sự kiện key down
         * Author: vtahoang (30/07/2023)
         */
        keyDownEvent(event) {
            try {
                switch (event.key) {
                    case "Escape":
                        this.$emit("cancel");
                        break;
                    default:
                        break;
                }
            } catch (error) {
                console.log("keyDownEvent ~ error:", error);
            }
        },
    },
};
</script>
