<template>
    <div class="h-container">
        <TheNavbar
            @showTM="showTMDeveloping()"
            @navbar-toggle="navbarExpand = !navbarExpand"
        ></TheNavbar>
        <div class="h-container__right">
            <TheHeader @showTM="showTMDeveloping()"></TheHeader>
            <div class="h-page">
                <router-view></router-view>
            </div>
        </div>
        <Teleport to="#app">
            <MISAToastMessage
                :type="tmType"
                :message="tmMessage"
                :icon="tmType"
                v-if="toastMessageTime > 0"
            ></MISAToastMessage>
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./TheMain.css);
</style>

<script>
import MISAToastMessage from "@/components/base/MISAToastmessage/MISAToastMessage.vue";
import { ref } from "vue";

export default {
    name: "TheMain",
    components: {
        MISAToastMessage,
    },
    data() {
        return {
            // Thông báo trên toast message
            tmMessage: "",
            // Loại toast message
            tmType: "",
            // Thời gian hiển thị toast message
            toastMessageTime: 0,
            // Đếm ngược thời gian hiển thị toast message
            countDownToastMessage: null,
            // navbar mở rộng hay thu gọn
            navbarExpand: false,
        };
    },
    methods: {
        /**
         * Hiện toast message
         * Author: vtahoang (01/08/2023)
         */
        showTm() {
            try {
                this.toastMessageTime = 3;
                clearInterval(this.countDownToastMessage);
                this.countDownToastMessage = setInterval(() => {
                    this.toastMessageTime -= 1;
                    if (this.toastMessageTime <= 0) {
                        clearInterval(this.countDownToastMessage);
                    }
                }, 1000);
            } catch (error) {
                console.log("showTm ~ error:", error);
            }
        },
        /**
         * Hiện toast message đang phát triển
         * Author: vtahoang - (06/07/2023)
         */
        showTMDeveloping() {
            try {
                this.tmMessage = this.$_MISAResources.toastMessage.developing;
                this.tmType = "warning";
                this.showTm();
            } catch (error) {
                console.log("showTM ~ error:", error);
            }
        },
    },
};
</script>
