<template>
    <div class="h-checkbox" @click.stop="clickCheckbox">
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
    methods: {
        clickCheckbox() {
            this.$emit("update:checked", !this.checked);
        },
    },
};
</script>
