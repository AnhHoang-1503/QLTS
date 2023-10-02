<template>
    <div class="h-contextmenu" :style="style" v-clickoutsideevent="clickOutSide">
        <div class="h-contextmenu__item" @click="addAsset" v-if="tool.add">
            <MISAIcon icon="btn--add"></MISAIcon>
            <span>{{ $_MISAResources.contextMenu[`add${item}`] }}</span>
        </div>
        <div class="h-contextmenu__item" @click="editAsset" v-if="tool.edit">
            <MISAIcon icon="btn--edit"></MISAIcon>
            <span>{{ $_MISAResources.contextMenu[`edit${item}`] }}</span>
        </div>
        <div class="h-contextmenu__item" @click="deleteAsset" v-if="tool.delete">
            <MISAIcon icon="btn--delete"></MISAIcon>
            <span>{{ $_MISAResources.contextMenu[`delete${item}`] }}</span>
        </div>
        <div class="h-contextmenu__item" @click="duplicateAsset" v-if="tool.duplicate">
            <MISAIcon icon="btn--clone"></MISAIcon>
            <span>{{ $_MISAResources.contextMenu[`duplicate${item}`] }}</span>
        </div>
    </div>
</template>

<style scoped>
@import url(./MISAContextMenu.css);
</style>

<script>
export default {
    name: "MISAContextMenu",
    props: {
        x: Number,
        y: Number,
        show: {
            type: Boolean,
            default: false,
        },
        tool: {
            add: false,
            edit: false,
            delete: false,
            duplicate: false,
        },
        // tên đối tượng Assets | Vouchers
        item: {
            type: String,
            default: "Assets",
        },
    },
    data() {
        return {
            style: {
                left: "",
                top: "",
            },
        };
    },
    watch: {
        x() {
            this.getCoordinates();
        },
        y() {
            this.getCoordinates();
        },
    },
    methods: {
        clickOutSide() {
            this.$emit("click-out-side");
        },
        addAsset() {
            this.$emit("addAsset");
            this.$emit("update:show", false);
        },
        editAsset() {
            this.$emit("editAsset");
            this.$emit("update:show", false);
        },
        deleteAsset() {
            this.$emit("deleteAsset");
            this.$emit("update:show", false);
        },
        duplicateAsset() {
            this.$emit("duplicateAsset");
            this.$emit("update:show", false);
        },
        /**
         * Đặt toạ độ cho context menu
         * Author: vtahoang (27/07/2023)
         */
        getCoordinates() {
            let x = this.x;
            if (this.x + 150 > window.innerWidth) x = x - 150;
            this.style.left = x + "px";

            let y = this.y;
            if (this.y + 158 > window.innerHeight) y = y - 158;
            this.style.top = y + "px";
        },
    },
    mounted() {
        this.getCoordinates();
    },
};
</script>
