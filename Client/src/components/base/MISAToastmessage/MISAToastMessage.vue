<template>
    <div class="h-tm" :class="'h-tm--' + type">
        <div v-if="icon" class="h-tm__icon">
            <MISAIcon :icon="icon"></MISAIcon>
        </div>
        <div class="h-tm__text">
            {{ message }}
        </div>
    </div>
</template>

<style scoped>
@import url("./MISAToastMessage.css");
</style>

<script>
import MISAIcon from "../MISAIcon/MISAIcon.vue";

export default {
    name: "MISAToastMessage",
    components: {
        MISAIcon,
    },
    props: {
        // loại: success, error, warning
        type: {
            type: String,
            default: "",
        },
        // icon truyền vào
        icon: {
            type: String,
            default: "",
        },
        //message
        message: {
            type: String,
            default: "",
        },
    },
};
</script>
