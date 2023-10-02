<template>
    <button class="h-btn" :class="classBtn" :tabindex="tabindex" @click="clickBtn" :style="style">
        <div class="h-btn__icon" v-if="icon">
            <MISAIcon :icon="icon" v-if="!loading"></MISAIcon>
            <MISAIcon :icon="subIcon" v-if="!loading && subIcon"></MISAIcon>
        </div>
        <div class="h-btn__text" v-if="!loading">{{ text }}</div>
        <MISAIcon icon="btn--loading" v-if="loading"></MISAIcon>
    </button>
</template>

<style scoped>
@import url(./MISAButton.css);
</style>

<script>
export default {
    name: "MISAButton",
    props: {
        icon: {
            type: String,
            default: "",
        },
        // icon phụ
        subIcon: {
            type: String,
            default: "",
        },
        tabindex: {
            type: Number,
            default: -1,
        },
        // kiểu nút: main, sub, outline
        type: {
            type: String,
            default: "main",
        },
        disabled: {
            type: Boolean,
            default: false,
        },
        text: {
            type: String,
            default: "",
        },
        style: {
            type: Object,
            default: () => ({}),
        },
        // trạng thái loading
        loading: {
            type: Boolean,
            default: false,
        },
    },
    computed: {
        classBtn() {
            let className = `h-btn--${this.type}`;
            if (this.disabled) className += " h-btn--disabled";
            return className;
        },
    },
    methods: {
        clickBtn() {
            if (!this.disabled && !this.loading) this.$emit("click-btn");
        },
    },
};
</script>
