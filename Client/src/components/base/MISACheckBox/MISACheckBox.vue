<!-- <template>
    <div class="h-checkbox">
        <MISAIcon v-if="!checked" icon="checkbox--unchecked"></MISAIcon>
        <MISAIcon v-if="checked" icon="checkbox--checked"></MISAIcon>
    </div>
</template> -->

<template>
    <div class="h-checkbox">
        <div class="checkbox" :class="checked ? 'checked' : 'unchecked'">
            <Transition>
                <div class="tickicon" v-if="checked">
                    <MISAIcon icon="newcheckbox--checked"></MISAIcon>
                </div>
            </Transition>
        </div>
    </div>
</template>

<style scoped>
@import url(./MISACheckBox.css);
</style>

<script>
export default {
    name: "MISACheckBox",
    props: {
        checked: {
            type: Boolean,
            default: false,
        },
    },
};
</script>
