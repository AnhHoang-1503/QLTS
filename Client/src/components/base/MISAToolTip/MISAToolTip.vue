<template>
    <div
        class="h-tooltip__container"
        ref="container"
        @mouseover="showToolTip = true"
        @mouseout="showToolTip = false"
        :class="overflow ? 'overflowable' : ''"
    >
        <div class="h-tooltip__content" ref="content">
            <slot name="content"></slot>
        </div>
        <Teleport to="#app">
            <div
                v-if="show && showToolTip"
                class="h-tooltip__textbox"
                :class="toolTipPosition + ' ' + visiable"
                :style="style"
                ref="textbox"
            >
                {{ text }}
            </div>
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./MISAToolTip.css);
</style>

<script>
export default {
    name: "MISAToolTip",
    props: {
        // mô tả trong tooltip
        text: {
            default: "",
        },
        // hiển thị tooltip
        show: {
            type: Boolean,
            default: true,
        },
        // ẩn text khi quá dài
        overflow: {
            type: Boolean,
            default: false,
        },
    },
    data() {
        return {
            toolTipPosition: "bottom",
            style: {
                left: "",
                top: "",
                right: "",
                bottom: "",
            },
            showToolTip: false,
        };
    },
    updated() {
        // vị trí tooltip
        let parentPosition = this.$refs.container.getBoundingClientRect();

        if (this.$refs.textbox && parentPosition.left + 100 > window.innerWidth) {
            this.toolTipPosition = "left";
        }

        if (this.toolTipPosition == "left") {
            this.style.left = parentPosition.left - 10 + "px";
            this.style.top = parentPosition.bottom - this.$refs.container.offsetHeight / 2 + "px";
        } else {
            this.style.left = parentPosition.left + this.$refs.container.offsetWidth / 2 + "px";
            this.style.top = parentPosition.bottom + 15 + "px";
        }
    },
    computed: {
        // hiển thị tooltip
        visiable() {
            return this.showToolTip ? "h-tooltip__textbox--visiable" : "";
        },
    },
};
</script>
