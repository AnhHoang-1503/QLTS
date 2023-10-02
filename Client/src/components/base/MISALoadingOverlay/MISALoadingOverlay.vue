<template>
    <div class="h-loading" v-if="show">
        <MISAIcon icon="loading" :style="{ width: '70px', height: '70px' }"></MISAIcon>
    </div>
</template>

<style scoped>
@import url(./MISALoadingOverlay.css);
</style>

<script>
export default {
    name: "MISALoadingOverlay",
    props: {
        show: {
            type: Boolean,
            default: false,
        },
    },
};
</script>
