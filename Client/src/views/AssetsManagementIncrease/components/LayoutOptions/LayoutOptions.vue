<template>
    <div
        class="layout-options__container"
        v-clickoutsideevent="
            () => {
                showList = false;
            }
        "
    >
        <MISAToolTip :text="$_MISAResources.assetsManagementIncrease.layout">
            <template #content>
                <MISAButton
                    type="sub"
                    :icon="currentIcon"
                    subIcon="arrow-down-2"
                    @click-btn="showList = !showList"
                ></MISAButton>
            </template>
        </MISAToolTip>
        <Transition>
            <div class="layout__list" v-if="showList" @click="showList = false">
                <div class="layout__item" @click="setLayout($_MISAEnums.layoutOptions.oneTable)">
                    <MISAIcon icon="layout--one-table"></MISAIcon>
                    <div class="layout__option--text">
                        {{ $_MISAResources.assetsManagementIncrease.layoutOptions.oneTable }}
                    </div>
                </div>
                <div class="layout__item" @click="setLayout($_MISAEnums.layoutOptions.stacked)">
                    <MISAIcon icon="layout--stacked"></MISAIcon>
                    <div class="layout__option--text">
                        {{ $_MISAResources.assetsManagementIncrease.layoutOptions.stacked }}
                    </div>
                </div>
                <!-- <div class="layout__item" @click="setLayout($_MISAEnums.layoutOptions.sideBySide)">
                    <MISAIcon icon="layout--side-by-side"></MISAIcon>
                    <div class="layout__option--text">
                        {{ $_MISAResources.assetsManagementIncrease.layoutOptions.oneTable }}
                    </div>
                </div> -->
            </div>
        </Transition>
    </div>
</template>

<style scoped>
@import url(./LayoutOptions.css);
</style>

<script>
export default {
    name: "LayoutOptions",
    data() {
        return {
            showList: false,
        };
    },
    props: {
        currentOption: {
            type: Number,
            default: 0,
        },
    },
    methods: {
        /**
         * Cập nhật kiểu layout
         * @param {*} type
         * Author: vtahoang (16/08/2023)
         */
        setLayout(type) {
            this.$emit("update:currentOption", type);
        },
    },
    computed: {
        /**
         * Lấy icon theo kiểu layout
         * Author: vtahoang (16/08/2023)
         */
        currentIcon() {
            var icon = "";

            switch (this.currentOption) {
                case 0:
                    icon = "layout--one-table";
                    break;
                case 1:
                    icon = "layout--stacked";
                    break;
                case 2:
                    icon = "layout--side-by-side";
                    break;
            }

            return icon;
        },
    },
};
</script>
