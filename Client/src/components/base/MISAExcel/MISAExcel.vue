<template>
    <div class="h-excelbox">
        <div
            class="h-excelbox__btn"
            ref="button"
            @contextmenu="toggleExcelBox($event)"
            @click="hideExcelBox"
            @mouseover="mouseover = true"
            @mouseleave="mouseover = false"
        >
            <slot name="btn"></slot>
        </div>
        <Teleport to="#app">
            <div
                class="h-excelbox__itembox"
                v-clickoutsideevent="hideExcelBox"
                :style="style"
                v-if="isShow"
            >
                <div class="h-excelbox__item" @click="exportToExcel">
                    <span>{{ $_MISAResources.excelBox.export }}</span>
                </div>
                <div class="h-excelbox__item" @click="uploadFile">
                    <span>{{ $_MISAResources.excelBox.import }}</span>
                    <input
                        type="file"
                        class="h-fileinput"
                        ref="excelFileInput"
                        @change="importFromExcel($event)"
                        accept=".xlsx, .xls"
                    />
                </div>
                <div class="h-excelbox__item" @click="exampleFile">
                    <span>{{ $_MISAResources.excelBox.example }}</span>
                </div>
            </div>
        </Teleport>
        <Teleport to="#app">
            <MISADialog v-if="showDialog" :message="errorMessage" @cancel="showDialog = false">
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.error.close"
                        @click-btn="showDialog = false"
                        :tabindex="1"
                    ></MISAButton>
                </template>
            </MISADialog>
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./MISAExcel.css);
</style>

<script>
import MISADialog from "../MISADialog/MISADialog.vue";

export default {
    name: "MISAExcel",
    components: {
        MISADialog,
    },
    data() {
        return {
            style: {
                left: "",
                top: "",
            },
            // hiện thị excelbox
            isShow: this.showExcelBox,
            // hover button
            mouseover: false,
            // loading
            isLoading: this.isLoadingExcel,
            // Thông báo lỗi
            errorMessage: "",
            // Hiện dialog
            showDialog: false,
        };
    },
    props: {
        showExcelBox: {
            type: Boolean,
            default: false,
        },
        isLoadingExcel: {
            type: Boolean,
            default: false,
        },
    },
    methods: {
        /**
         * Đặt toạ độ cho excelbox
         * Author: vtahoang (05/08/2023)
         */
        getCoordinates() {
            let parentPosition = this.$refs.button.getBoundingClientRect();
            this.style.left = parentPosition.left + this.$refs.button.offsetWidth / 2 + "px";
            this.style.top = parentPosition.top + parentPosition.height + 10 + "px";
        },
        /**
         * Toggle excelbox
         * @param {*} event
         * Author: vtahoang (05/08/2023)
         */
        toggleExcelBox(event) {
            event.preventDefault();
            this.isShow = !this.isShow;
        },
        /**
         * Ẩn excelbox
         * Author: vtahoang (05/08/2023)
         */
        hideExcelBox() {
            if (!this.mouseover) this.isShow = false;
        },
        /**
         * Tải file lên
         * Author: vtahoang (08/08/2023)
         */
        uploadFile() {
            this.$refs.excelFileInput.click();
            this.hideExcelBox();
        },
        /**
         * Xuất dữ liệu ra file excel
         * Author: vtahoang (08/08/2023)
         */
        exportToExcel() {
            this.$emit("export-to-excel");
        },
        /**
         * Tải file mẫu nhập liệu
         * Author: vtahoang (08/08/2023)
         */
        async exampleFile() {
            try {
                if (!this.isLoading) {
                    this.isLoading = true;

                    await this.$_MISAApi.fixedAssetApi.exampleFileExcel();
                }
            } catch (error) {
                console.log("downloadExampleFile ~ error:", error);
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Nhập dữ liệu từ file excel
         * Author: vtahoang (08/08/2023)
         */
        async importFromExcel(event) {
            try {
                this.isLoading = true;

                await this.$_MISAApi.fixedAssetApi.importFromExcel(event.target.files[0]);

                // Gửi tín hiệu import thành công
                this.$emit("import-success");
            } catch (error) {
                this.errorMessage = error.response.data.UserMessage;
                this.showDialog = true;
            } finally {
                this.isLoading = false;
            }
        },
    },
    watch: {
        isShow(value) {
            this.$emit("update:showExcelBox", value);
        },
        showExcelBox(value) {
            this.isShow = value;
        },
        isLoadingExcel(value) {
            this.isLoading = value;
        },
        isLoading(value) {
            this.$emit("update:isLoadingExcel", value);
        },
    },
    updated() {
        // cập nhật vị trí của excelbox
        this.getCoordinates();
    },
};
</script>
