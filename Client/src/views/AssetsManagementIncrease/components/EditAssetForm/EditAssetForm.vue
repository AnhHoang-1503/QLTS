<template>
    <div
        class="editassetform__container"
        v-focusloop="0"
        @keydown.stop="keyDownEvent($event)"
        v-autofocus
        tabindex="-1"
    >
        <div class="container__form">
            <div class="form__header">
                <div class="header__title">
                    {{
                        $_MISAResources.assetsManagementIncrease.editAssetForm.title(
                            $store.state.editAssetForm.fixedAsset.fixed_asset_name
                        )
                    }}
                </div>
                <MISAToolTip text="Esc">
                    <template #content>
                        <div class="header__closebtn" @click="closeForm">
                            <MISAIcon icon="close"></MISAIcon>
                        </div>
                    </template>
                </MISAToolTip>
            </div>
            <div class="form__body">
                <div class="body__department">
                    <MISATextfield
                        :label="$_MISAResources.assetsManagementIncrease.editAssetForm.department"
                        :disable="true"
                        v-model="$store.state.editAssetForm.fixedAsset.department_name"
                    />
                </div>
                <div class="body__cost">
                    <div class="cost__title">
                        {{ $_MISAResources.assetsManagementIncrease.editAssetForm.cost }}
                    </div>
                    <div class="body__row">
                        <div class="body__col body__col--5">
                            {{ $_MISAResources.assetsManagementIncrease.editAssetForm.source }}
                        </div>
                        <div class="body__col body__col--2">
                            {{ $_MISAResources.assetsManagementIncrease.editAssetForm.value }}
                        </div>
                        <div class="body__col body__col--1"></div>
                    </div>
                    <div class="cost__datalist">
                        <div class="body__row" v-for="(item, index) in dataList">
                            <div class="body__col body__col--5">
                                <MISADropdown
                                    :dataList="budgetCategories"
                                    :iconRight="'expand'"
                                    v-model="item.budget_category_name"
                                    ref="budgetCategory"
                                    :required="true"
                                    :originalList="
                                        $store.state.increase.budgetCategory.map(
                                            (item) => item.budget_category_name
                                        )
                                    "
                                    :placeholder="
                                        $_MISAResources.assetsManagementIncrease.editAssetForm
                                            .sourcePlaceholder
                                    "
                                />
                            </div>
                            <div class="body__col body__col--2">
                                <MISATextfield
                                    v-model="item.cost"
                                    type="number"
                                    :textRight="true"
                                    ref="cost"
                                    :required="true"
                                    :maxLength="12"
                                />
                            </div>
                            <div class="body__col body__col--1">
                                <div class="buttonblock">
                                    <MISAToolTip
                                        :text="
                                            $_MISAResources.assetsManagementIncrease.editAssetForm
                                                .add
                                        "
                                    >
                                        <template #content>
                                            <div
                                                class="buttonblock__button button__add"
                                                @click="dataList.push({})"
                                            >
                                                <MISAIcon icon="add-data" />
                                            </div>
                                        </template>
                                    </MISAToolTip>
                                    <MISAToolTip
                                        :text="
                                            $_MISAResources.assetsManagementIncrease.editAssetForm
                                                .delete
                                        "
                                    >
                                        <template #content>
                                            <div class="buttonblock__button button__delete">
                                                <MISAIcon
                                                    icon="delete-data"
                                                    @click="dataList.splice(index, 1)"
                                                    v-if="index > 0"
                                                />
                                            </div>
                                        </template>
                                    </MISAToolTip>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="cost__total">
                        <div class="body__row">
                            <div class="body__col body__col--5">
                                <MISATextfield :disable="true" v-model="totalText" />
                            </div>
                            <div class="body__col body__col--2">
                                <MISATextfield :disable="true" v-model="totalCost" type="number" />
                            </div>
                            <div class="body__col body__col--1"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form__footer">
                <MISAToolTip text="Esc">
                    <template #content>
                        <div class="footer__btn footer__btn--close">
                            <MISAButton
                                type="sub"
                                :text="$_MISAResources.assetsManagementIncrease.form.cancelBtn"
                                @click-btn="closeForm"
                                :tabindex="1"
                            ></MISAButton>
                        </div>
                    </template>
                </MISAToolTip>
                <MISAToolTip text="Ctrl + S">
                    <template #content>
                        <div class="footer__btn footer__btn--save">
                            <MISAButton
                                :text="$_MISAResources.assetsManagementIncrease.form.saveBtn"
                                @click-btn="saveForm"
                                :tabindex="0"
                            ></MISAButton>
                        </div>
                    </template>
                </MISAToolTip>
            </div>
        </div>
        <Teleport to="#app"
            ><MISADialog :message="dialogMessage" v-if="showDialog" @cancel="showDialog = false">
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
@import url(./EditAssetForm.css);
</style>

<script>
import MISATextfield from "@/components/base/MISATextfield/MISATextfield.vue";
import MISADropdown from "@/components/base/MISADropdown/MISADropdown.vue";
import MISADialog from "@/components/base/MISADialog/MISADialog.vue";

export default {
    name: "EditAssetForm",
    components: {
        MISATextfield,
        MISADropdown,
        MISADialog,
    },
    methods: {
        /**
         * Đóng form
         * Author: vtahoang (17/08/2023)
         */
        closeForm() {
            this.$emit("close-form");
        },
        /**
         * Lưu form
         * Author: vtahoang (25/08/2023)
         */
        saveForm() {
            let status = true;

            //xoá các trường trống
            let dataList = this.dataList.filter((item) => {
                if (item.budget_category_name || item.cost) {
                    return true;
                }
            });

            this.$store.commit("editAssetForm/setBudget", dataList);

            this.$nextTick(() => {
                // validate các trường
                this.$refs.budgetCategory.forEach((item) => {
                    status = item.isValid() && status;
                });
                this.$refs.cost.forEach((item) => {
                    status = item.isValid() && status;
                });

                //  Danh sách nguồn hình thành đã được chọn
                let exist = this.$store.state.editAssetForm.fixedAsset.budget.map(
                    (item) => item.budget_category_name
                );

                // Kiểm tra nguồn hình thành có trùng nhau hay không
                let duplicate = exist.filter((item, index) => {
                    if (item) {
                        return exist.indexOf(item) !== index;
                    }
                });

                duplicate = [...new Set(duplicate)];

                if (duplicate.length >= 1) {
                    this.dialogMessage =
                        this.$_MISAResources.assetsManagementIncrease.editAssetForm.duplicateError(
                            duplicate
                        );
                    this.showDialog = true;
                    return;
                }

                if (!status) {
                    return;
                }

                this.$store.dispatch("editAssetForm/submitForm");

                this.$emit("close-form");
            });
        },
        /**
         * Sự kiện keydown
         * @param {*} event
         * Author: vtahoang (05/09/2023)
         */
        keyDownEvent(event) {
            if (event.key == "Escape") {
                event.preventDefault();
                this.closeForm();
            }
            if (event.ctrlKey && event.key == "s") {
                event.preventDefault();
                this.saveForm();
            }
        },
    },
    props: {},
    data() {
        return {
            // Tổng
            totalText: this.$_MISAResources.assetsManagementIncrease.editAssetForm.total,
            totalCost: this.$store.state.editAssetForm.fixedAsset.cost,
            // Hiển thị dialog thông báo
            showDialog: false,
            // Nội dung dialog
            dialogMessage: "",
        };
    },
    computed: {
        /**
         * Danh sách nguồn hình thành
         * Author: vtahoang (25/08/2023)
         */
        dataList() {
            return this.$store.state.editAssetForm.fixedAsset.budget;
        },
        /**
         * Danh sách nguồn hình thành chưa được chọn
         * Author: vtahoang (27/08/2023)
         */
        budgetCategories() {
            // // Danh sách nguồn hình thành ban đầu
            // let originalList = this.$store.state.increase.budgetCategory.map(
            //     (item) => item.budget_category_name
            // );

            // // Danh sách nguồn hình thành đã được chọn
            // let exist = this.$store.state.editAssetForm.fixedAsset.budget.map(
            //     (item) => item.budget_category_name
            // );

            // // Lấy danh sách nguồn hình thành chưa được chọn
            // let result = originalList.filter((item) => !exist.includes(item));

            return this.$store.state.increase.budgetCategory.map(
                (item) => item.budget_category_name
            );
        },
    },
    watch: {
        /**
         * Cập nhật tổng chi phí
         * Author: vtahoang (29/08/2023)
         */
        dataList: {
            handler: function (value) {
                let total = 0;
                value.forEach((item, index) => {
                    if (item.cost) {
                        total += parseInt(item.cost);
                    }
                });
                this.totalCost = total;
            },
            deep: true,
        },
    },
};
</script>
