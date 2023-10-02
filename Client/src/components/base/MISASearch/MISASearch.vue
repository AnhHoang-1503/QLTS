<template>
    <label class="h-search" for="toolbar-search">
        <div v-if="isLoadingSearch" class="h-search__icon--loading"></div>
        <div v-if="!isLoadingSearch" class="h-search__icon--search"></div>
        <input
            id="toolbar-search"
            type="text"
            class="h-search__input"
            :placeholder="placeholder"
            v-model="inputValue"
            :tabindex="tabindex"
            autocomplete="off"
            ref="input"
        />
    </label>
</template>

<style scoped>
@import url(./MISASearch.css);
</style>

<script>
export default {
    data() {
        return {
            inputValue: this.modelValue, // giá trị input
            delay: null, // delay trước khi cập nhật giá trị tìm kiếm
            status: "default", // trạng thái của input
        };
    },
    props: {
        // placeholder truyền vào
        placeholder: {
            type: String,
            default: "",
        },
        modelValue: {
            type: String,
            default: "",
        },
        tabindex: {
            type: Number,
            default: -1,
        },
        isLoadingSearch: {
            type: Boolean,
            default: false,
        },
        width: {
            type: String,
            default: null,
        },
    },
    watch: {
        modelValue(value) {
            this.inputValue = value;
        },
        inputValue(value) {
            // delay 1s trước khi emit
            clearTimeout(this.delay);
            this.delay = setTimeout(() => {
                this.$emit("update:modelValue", value);
                this.status = "default";
            }, 1000);
        },
    },
    mounted() {
        if (this.width) this.$refs.input.style.width = this.width;
    },
};
</script>
