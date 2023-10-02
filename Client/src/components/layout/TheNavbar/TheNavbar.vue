<template>
    <div class="h-navbar" ref="h-navbar">
        <div class="h-navbar__logo">
            <div class="h-logo__img"></div>
            <div class="h-logo__text">{{ $_MISAResources.navBar.appName }}</div>
        </div>
        <router-link
            to="/overview"
            class="h-navbar__btn--icon h-navbar__overview"
            @mouseover="mouseOver"
            @mouseout="mouseOut"
        >
            <div class="h-overview__icon"></div>
            <div class="h-navbar__btn--text">{{ $_MISAResources.navBar.overview }}</div>
        </router-link>
        <router-link
            to="/assets-management"
            class="h-navbar__btn--icon h-navbar__assets"
            :class="activeClass"
        >
            <div class="h-assets__icon"></div>
            <div class="h-navbar__btn--text">{{ $_MISAResources.navBar.assets }}</div>
            <div class="h-btn__icon--expand" @click.prevent="assetsExpand = !assetsExpand">
                <MISAIcon icon="arrow-down-2"></MISAIcon>
            </div>
        </router-link>
        <Transition name="navbarItemsExpand">
            <div class="h-navbar__assets--expand" v-if="navBarExpand && assetsExpand">
                <router-link
                    to="/assets-management/increase"
                    class="h-assets__item"
                    :class="activeItem"
                    >{{ $_MISAResources.navBar.assetItems.increase }}</router-link
                >
                <div class="h-assets__item">
                    {{ $_MISAResources.navBar.assetItems.changeInformation }}
                </div>
                <div class="h-assets__item">{{ $_MISAResources.navBar.assetItems.reEvalute }}</div>
                <div class="h-assets__item">
                    {{ $_MISAResources.navBar.assetItems.depreciation }}
                </div>
                <div class="h-assets__item">{{ $_MISAResources.navBar.assetItems.transfer }}</div>
                <div class="h-assets__item">{{ $_MISAResources.navBar.assetItems.decrease }}</div>
                <div class="h-assets__item">{{ $_MISAResources.navBar.assetItems.inventory }}</div>
                <div class="h-assets__item">{{ $_MISAResources.navBar.assetItems.other }}</div>
            </div>
        </Transition>
        <router-link
            to="/"
            class="h-navbar__btn--icon h-navbar__road"
            @click="showTM"
            @mouseover="mouseOver"
            @mouseout="mouseOut"
        >
            <div class="h-road__icon"></div>
            <div class="h-navbar__btn--text">{{ $_MISAResources.navBar.infrastructureAssets }}</div>
            <div class="h-btn__icon--expand">
                <MISAIcon icon="arrow-down-2"></MISAIcon>
            </div>
        </router-link>
        <router-link
            to="/"
            class="h-navbar__btn--icon h-navbar__tool"
            @click="showTM"
            @mouseover="mouseOver"
            @mouseout="mouseOut"
        >
            <div class="h-tool__icon"></div>
            <div class="h-navbar__btn--text">{{ $_MISAResources.navBar.tool }}</div>
            <div class="h-btn__icon--expand">
                <MISAIcon icon="arrow-down-2"></MISAIcon>
            </div>
        </router-link>
        <router-link
            to="/"
            class="h-navbar__btn--icon h-navbar__categories"
            @click="showTM"
            @mouseover="mouseOver"
            @mouseout="mouseOut"
        >
            <div class="h-categories__icon"></div>
            <div class="h-navbar__btn--text">{{ $_MISAResources.navBar.categories }}</div>
        </router-link>
        <router-link
            to="/"
            class="h-navbar__btn--icon h-navbar__search"
            @click="showTM"
            @mouseover="mouseOver"
            @mouseout="mouseOut"
        >
            <div class="h-search__icon"></div>
            <div class="h-navbar__btn--text">{{ $_MISAResources.navBar.search }}</div>
            <div class="h-btn__icon--expand">
                <MISAIcon icon="arrow-down-2"></MISAIcon>
            </div>
        </router-link>
        <router-link
            to="/"
            class="h-navbar__btn--icon h-navbar__report"
            @click="showTM"
            @mouseover="mouseOver"
            @mouseout="mouseOut"
        >
            <div class="h-report__icon"></div>
            <div class="h-navbar__btn--text">{{ $_MISAResources.navBar.report }}</div>
            <div class="h-btn__icon--expand">
                <MISAIcon icon="arrow-down-2"></MISAIcon>
            </div>
        </router-link>
        <div class="h-navbar__bottom" @click="navbarToggle">
            <div class="h-navbar__btn--shrink"></div>
        </div>
    </div>
</template>

<style scoped>
@import url(./TheNavbar.css);
</style>

<script>
export default {
    name: "TheNavbar",
    data() {
        return {
            activeClass: "h-navbar__btn--icon--active",
            navBarExpand: false,
            // Mở rộng thẻ tài sản
            assetsExpand: false,
        };
    },
    methods: {
        /**
         * Ẩn hiện navbar khi click
         * Author: vtahoang - (21/06/2023)
         */
        navbarToggle() {
            try {
                const navbar = this.$refs["h-navbar"];
                navbar.classList.toggle("h-navbar--expand");
                this.navBarExpand = !this.navBarExpand;
                this.$emit("navbar-toggle");
            } catch (error) {
                console.log(error);
            }
        },
        showTM: function () {
            this.$emit("showTM");
        },
        /**
         * trả lại trạng thái btn trên navbar khi bỏ hover
         * Author: vtahoang - (21/06/2023)
         */
        mouseOut() {
            try {
                this.activeClass = "h-navbar__btn--icon--active";
            } catch (error) {
                console.log("hover ~ error:", error);
            }
        },
        /**
         * Xoá trạng thái btn trên navbar khi hover btn khác
         * Author: vtahoang - (21/06/2023)
         */
        mouseOver() {
            try {
                this.activeClass = "";
            } catch (error) {
                console.log("hover ~ error:", error);
            }
        },
    },
    watch: {
        routerLink() {
            if (this.routerLink.includes("Assets")) this.assetsExpand = true;
            else this.assetsExpand = false;
        },
    },
    computed: {
        activeItem() {
            if (this.$route.path.includes("increase")) return "h-assets__item--active";
        },
        routerLink() {
            return this.$route.name;
        },
    },
};
</script>
