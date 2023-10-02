<template>
    <div class="login__container">
        <div class="login__pagetitle"></div>
        <div class="login__copyright">Copyright © 2020 - 2023 MISA JSC</div>
        <div class="login__box">
            <div class="box__left"></div>
            <div class="box__right" tabindex="-1" @keydown="keyDownEvent($event)" v-focusloop="0">
                <div class="logo__group">
                    <div class="logo"></div>
                    <div class="logo__text" v-html="$_MISAResources.login.logo_text"></div>
                </div>
                <div class="input__group">
                    <div class="input__account">
                        <input
                            type="text"
                            class="login__input account"
                            :placeholder="$_MISAResources.login.accountPlaceholder"
                            v-model="$store.state.auth.account"
                            @focusin="focusInput($event)"
                            ref="account"
                            :tabindex="1"
                        />
                    </div>
                    <div class="input__password">
                        <input
                            :type="showPassword ? 'text' : 'password'"
                            class="login__input password"
                            :placeholder="$_MISAResources.login.passwordPlaceholder"
                            v-model="$store.state.auth.password"
                            @focusin="focusInput($event)"
                            ref="password"
                            :tabindex="2"
                        />
                        <div class="password__icon" @click.prevent="showPassword = !showPassword">
                            <MISAIcon :class="showPassword ? 'on-eye' : 'off-eye'" />
                        </div>
                    </div>
                    <button class="submit" @click="submit()" tabindex="3">
                        {{ $_MISAResources.login.login }}
                    </button>
                    <div class="forgot-password">{{ $_MISAResources.login.forgot_password }}</div>
                </div>
            </div>
        </div>
        <Teleport to="#app">
            <Transition name="toast-message">
                <MISAToastMessage
                    :type="'error'"
                    :message="$store.state.auth.errorMessage"
                    :icon="'error'"
                    v-if="$store.state.auth.errorMessage"
                ></MISAToastMessage>
            </Transition>
            <MISALoadingOverlay :show="$store.state.auth.loading" />
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./Login.css);
</style>

<script>
import MISAToastMessage from "@/components/base/MISAToastMessage/MISAToastMessage.vue";
import MISALoadingOverlay from "@/components/base/MISALoadingOverlay/MISALoadingOverlay.vue";

export default {
    name: "Login",
    components: {
        MISAToastMessage,
        MISALoadingOverlay,
    },
    data() {
        return {
            // Hiển thị password
            showPassword: false,
        };
    },
    methods: {
        // Xử lý khi focus vào input
        focusInput(event) {
            event.target.classList.remove("input--error");
        },
        // submit form
        submit() {
            let isValidate = true;
            if (this.$store.state.auth.account == "") {
                this.$refs.account.classList.add("input--error");
                isValidate = false;
            }
            if (this.$store.state.auth.password == "") {
                this.$refs.password.classList.add("input--error");
                isValidate = false;
            }

            if (!isValidate) {
                this.$store.dispatch(
                    "auth/setErrorMessage",
                    this.$_MISAResources.login.error_message
                );
                return;
            }

            this.$store.dispatch("auth/submit");
        },
        /**
         * Sự kiện key down
         * @param {*} event
         * Author: vtahoang (16/09/2023)
         */
        keyDownEvent(event) {
            if (event.key == "Enter") {
                this.submit();
            }
        },
    },
};
</script>
