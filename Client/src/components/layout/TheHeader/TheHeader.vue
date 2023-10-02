<template>
    <div class="h-header">
        <div class="h-header__left">
            <div class="h-header__title">{{ $_MISAResources.header.title }}</div>
        </div>
        <div class="h-header__right">
            <div
                class="h-header__btn h-header__dropdown"
                @click.stop="showUserBox = !showUserBox"
            ></div>
            <div
                class="h-header__btn h-header__avatar"
                @click.stop="showUserBox = !showUserBox"
            ></div>
            <div class="h-header__btn h-header__support" @click="showTM"></div>
            <div class="h-header__btn h-header__menu" @click="showTM"></div>
            <div class="h-header__btn h-header__notification" @click="showTM"></div>
            <div class="h-header__btn h-header__year">
                <div class="h-year__text">{{ $_MISAResources.header.year }}</div>
                <div class="h-year__selected">{{ currentYear }}</div>
                <div class="h-year__icon">
                    <div class="h-year__icon--up" @click="currentYear += 1"></div>
                    <div class="h-year__icon--down" @click="currentYear -= 1"></div>
                </div>
            </div>
            <div class="h-header__department">{{ $_MISAResources.header.department }}</div>
        </div>
        <Teleport to="#app">
            <Transition name="user">
                <div
                    class="user__container"
                    v-if="showUserBox"
                    v-clickoutsideevent="
                        () => {
                            showUserBox = false;
                        }
                    "
                >
                    <div class="infor container__item">
                        <div class="user__name">
                            {{ $store.state.global.user ? $store.state.global.user.name : "" }}
                        </div>
                        <div class="user__email">
                            {{ $store.state.global.user ? $store.state.global.user.email : "" }}
                        </div>
                    </div>
                    <div
                        class="logout container__item"
                        @click="
                            () => {
                                showUserBox = false;
                                $store.dispatch('auth/logout');
                            }
                        "
                    >
                        <div class="logout__icon">
                            <MISAIcon :icon="'logout'"></MISAIcon>
                        </div>
                        <div class="logout__text">{{ $_MISAResources.header.logout }}</div>
                    </div>
                </div>
            </Transition>
        </Teleport>
    </div>
</template>
<style scoped>
@import url(./TheHeader.css);
</style>

<script>
export default {
    name: "TheHeader",
    methods: {
        /**
         * Hiện toast message
         * Author: vtahoang (16/09/2023)
         */
        showTM: function () {
            this.$emit("showTM");
        },
        test() {
            console.log(this.$store.state.global.user);
        },
    },
    data() {
        return {
            currentYear: 2023,
            showUserBox: false,
        };
    },
};
</script>
