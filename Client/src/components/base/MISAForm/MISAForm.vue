<template>
    <div
        class="h-form"
        tabindex="-1"
        @keydown="formKeyDownEvent($event)"
        v-focusloop="0"
        v-autofocus
    >
        <div class="h-form__container">
            <div class="h-form__header">
                <div class="h-form__title">{{ title }}</div>
                <MISAToolTip text="Esc">
                    <template #content>
                        <div class="h-form__close" @click="closeForm"></div>
                    </template>
                </MISAToolTip>
            </div>
            <div class="h-form__main">
                <div class="h-form__row">
                    <div class="h-form__assetid">
                        <MISATextfield
                            :tabindex="1"
                            ref="code"
                            :label="$_MISAResources.form.formInputLabel.assetId"
                            :required="true"
                            v-model="assetData.fixed_asset_code"
                            v-model:modelErrormsg="assetIdErrorMessage"
                            :maxLength="50"
                            :placeholder="$_MISAResources.form.formInputPlaceholder.assetId"
                            :isLoading="isLoadingDefaultCode"
                        ></MISATextfield>
                    </div>
                    <div class="h-form__name">
                        <MISATextfield
                            :tabindex="2"
                            ref="name"
                            :label="$_MISAResources.form.formInputLabel.assetName"
                            :required="true"
                            :placeholder="$_MISAResources.form.formInputPlaceholder.assetName"
                            v-model="assetData.fixed_asset_name"
                        ></MISATextfield>
                    </div>
                </div>
                <div class="h-form__row">
                    <div class="h-form__departmentid">
                        <MISADropdown
                            :tabindex="3"
                            ref="department"
                            :label="$_MISAResources.form.formInputLabel.departmentCode"
                            :required="true"
                            v-model="assetData.department_code"
                            :placeholder="$_MISAResources.form.formInputPlaceholder.departmentCode"
                            :iconRight="'expand'"
                            :dataList="
                                this.$_MISAResources.department.map((item) => item.department_code)
                            "
                        ></MISADropdown>
                    </div>
                    <div class="h-form__department">
                        <MISATextfield
                            :label="$_MISAResources.form.formInputLabel.departmentName"
                            :disable="true"
                            v-model="assetData.department_name"
                        ></MISATextfield>
                    </div>
                </div>
                <div class="h-form__row">
                    <div class="h-form__assetid">
                        <MISADropdown
                            :tabindex="4"
                            ref="category"
                            :label="$_MISAResources.form.formInputLabel.assetCategoryCode"
                            :required="true"
                            v-model="assetData.fixed_asset_category_code"
                            :placeholder="
                                $_MISAResources.form.formInputPlaceholder.assetCategoryCode
                            "
                            :iconRight="'expand'"
                            :dataList="
                                this.$_MISAResources.fixed_asset_category.map(
                                    (item) => item.fixed_asset_category_code
                                )
                            "
                        ></MISADropdown>
                    </div>
                    <div class="h-form__asset">
                        <MISATextfield
                            :label="$_MISAResources.form.formInputLabel.assetCategoryName"
                            :disable="true"
                            v-model="assetData.fixed_asset_category_name"
                        ></MISATextfield>
                    </div>
                </div>
                <div class="h-form__row">
                    <div class="h-form__amount">
                        <MISATextfield
                            :tabindex="5"
                            ref="quantity"
                            :label="$_MISAResources.form.formInputLabel.quantity"
                            :required="true"
                            v-model="assetData.quantity"
                            :textRight="true"
                            icon="up_down_arrows"
                            type="number"
                        ></MISATextfield>
                    </div>
                    <div class="h-form__originalprice">
                        <MISATextfield
                            :tabindex="6"
                            ref="cost"
                            :label="$_MISAResources.form.formInputLabel.cost"
                            :required="true"
                            v-model="assetData.cost"
                            :textRight="true"
                            type="number"
                        ></MISATextfield>
                    </div>
                    <div class="h-form__yearsuse">
                        <MISATextfield
                            :tabindex="7"
                            ref="yearuse"
                            :label="$_MISAResources.form.formInputLabel.lifeTime"
                            :required="true"
                            v-model="assetData.life_time"
                            :textRight="true"
                            type="number"
                        ></MISATextfield>
                    </div>
                </div>
                <div class="h-form__row">
                    <div class="h-form__atrophy--percents">
                        <MISATextfield
                            :tabindex="8"
                            ref="depreciation_rate"
                            :label="$_MISAResources.form.formInputLabel.depreciationRate"
                            :required="true"
                            v-model="assetData.depreciation_rate"
                            :textRight="true"
                            icon="up_down_arrows"
                            type="percent"
                        ></MISATextfield>
                    </div>
                    <div class="h-form__atrophy">
                        <MISATextfield
                            :tabindex="9"
                            ref="depreciation_value"
                            :label="$_MISAResources.form.formInputLabel.depreciationValue"
                            :required="true"
                            :textRight="true"
                            type="number"
                            v-model="assetData.depreciation_value_year"
                            v-model:modelErrormsg="depreciationValueErrorMessage"
                        ></MISATextfield>
                    </div>
                    <div class="h-form__yeartracking">
                        <MISATextfield
                            :label="$_MISAResources.form.formInputLabel.yearTracking"
                            :disable="true"
                            v-model="assetData.tracked_year"
                            :textRight="true"
                        ></MISATextfield>
                    </div>
                </div>
                <div class="h-form__row">
                    <div class="h-form__daybuy">
                        <MISADatePicker
                            :tabindex="10"
                            :label="$_MISAResources.form.formInputLabel.purchasedDate"
                            :required="true"
                            v-model="assetData.purchase_date"
                            icon="calendar"
                            ref="daybuy"
                            :placeholder="$_MISAResources.form.formInputPlaceholder.purchasedDate"
                        ></MISADatePicker>
                    </div>
                    <div class="h-form__dayuse">
                        <MISADatePicker
                            :tabindex="11"
                            :label="$_MISAResources.form.formInputLabel.startUsingDate"
                            :required="true"
                            icon="calendar"
                            ref="dayuse"
                            v-model="assetData.start_using_date"
                            :placeholder="$_MISAResources.form.formInputPlaceholder.startUsingDate"
                        ></MISADatePicker>
                    </div>
                    <div></div>
                </div>
            </div>
            <div class="h-form__footer">
                <div class="h-footer__btn--cancel">
                    <MISAButton
                        type="sub"
                        :tabindex="13"
                        @click-btn="closeForm"
                        :text="$_MISAResources.form.formButtonText.cancel"
                    ></MISAButton>
                </div>
                <MISAToolTip text="Ctrl + S">
                    <template #content>
                        <div class="h-footer__btn--save">
                            <MISAButton
                                :tabindex="12"
                                @click-btn="saveForm"
                                :text="$_MISAResources.form.formButtonText.save"
                            >
                            </MISAButton>
                        </div>
                    </template>
                </MISAToolTip>
            </div>
        </div>
        <Teleport to="#app">
            <MISADialog :message="diaglogMessage" v-if="showDialog" @cancel="hideDialog">
                <template #left-btn>
                    <MISAButton
                        v-if="formMode == $_MISAEnums.form.formMode.edit"
                        type="outline"
                        :text="$_MISAResources.dialog.edit.btnCancel"
                        @click-btn="hideDialog"
                        :style="{ 'border-color': '#afafaf' }"
                        :tabindex="1"
                    ></MISAButton>
                </template>
                <template #middle-btn>
                    <MISAButton
                        v-if="
                            formMode == $_MISAEnums.form.formMode.add ||
                            formMode == $_MISAEnums.form.formMode.duplicate
                        "
                        type="outline"
                        :text="$_MISAResources.dialog.add.btnCancel"
                        @click-btn="hideDialog"
                        :style="{ 'border-color': '#afafaf' }"
                        :tabindex="1"
                    ></MISAButton>
                    <MISAButton
                        v-if="formMode == $_MISAEnums.form.formMode.edit"
                        type="outline"
                        :text="$_MISAResources.dialog.edit.btnUnsave"
                        @click-btn="cancelForm"
                        :style="{
                            'border-color': '#0582a2',
                            color: '#0582a2',
                        }"
                        :tabindex="2"
                    ></MISAButton>
                </template>
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.add.btnConfirm"
                        @click-btn="cancelForm"
                        v-if="
                            formMode == $_MISAEnums.form.formMode.add ||
                            formMode == $_MISAEnums.form.formMode.duplicate
                        "
                        :tabindex="2"
                    ></MISAButton>
                    <MISAButton
                        :text="$_MISAResources.dialog.edit.btnSave"
                        @click-btn="saveForm"
                        v-if="formMode == $_MISAEnums.form.formMode.edit"
                        :tabindex="3"
                    ></MISAButton>
                </template>
            </MISADialog>
            <MISADialog
                :message="errorDialogMessage"
                v-if="showErrorDialog"
                @cancel="showErrorDialog = false"
            >
                <template #right-btn>
                    <MISAButton
                        :text="$_MISAResources.dialog.error.close"
                        @click-btn="showErrorDialog = false"
                        :tabindex="1"
                    ></MISAButton>
                </template>
            </MISADialog>
            <div class="h-form__loading--overlay" v-if="isLoading">
                <MISAIcon icon="loading" :style="{ width: '70px', height: '70px' }"></MISAIcon>
            </div>
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./MISAForm.css);
</style>

<script>
import MISATextfield from "../MISATextfield/MISATextfield.vue";
import MISADatePicker from "../MISADatePicker/MISADatePicker.vue";
import MISADialog from "../MISADialog/MISADialog.vue";
import MISADropdown from "../MISADropdown/MISADropdown.vue";

export default {
    name: "MISAForm",
    async mounted() {
        try {
            // Lấy dữ liệu tài sản
            if (this.dataObject) this.assetData = this.dataObject;
            else this.assetData = this.defaultValue;

            // Lấy mã tài sản mặc định
            if (
                this.formMode == this.$_MISAEnums.form.formMode.add ||
                this.formMode == this.$_MISAEnums.form.formMode.duplicate
            ) {
                this.isLoadingDefaultCode = true;
                let defaultCode = await this.$_MISAApi.fixedAssetApi.getDefaultFixedAssetCode();
                this.assetData.fixed_asset_code = defaultCode;
            }

            // Đặt trạng thái sửa
            this.$nextTick(() => {
                this.isChange = false;
            });
        } catch (error) {
            console.log("mounted ~ error:", error);
        } finally {
            this.isLoadingDefaultCode = false;
        }
    },
    components: {
        MISATextfield,
        MISADropdown,
        MISADatePicker,
        MISADialog,
    },
    data() {
        // dữ liệu tài sản mặc định
        let defaultValue = {
            department_code: "", // mã phòng ban
            department_id: "", // id phòng ban
            department_name: "", // tên phòng ban
            fixed_asset_category_code: "", // mã loại tài sản
            fixed_asset_category_id: "", // id loại tài sản
            fixed_asset_category_name: "", // tên loại tài sản
            fixed_asset_code: "", // mã tài sản
            fixed_asset_name: "", // tên tài sản
            life_time: 0, // số năm sử dụng
            purchase_date: new Date(), // ngày mua
            quantity: 0, // số lượng
            tracked_year: 2023, // năm theo dõi
            cost: 0, // nguyên giá
            start_using_date: new Date(), // ngày sử dụng
            depreciation_value_year: 0, // giá trị hao mòn năm
            depreciation_rate: 0, // tỷ lệ hao mòn
        };

        // giá trị mặc định khi thêm mới
        let assetData;
        if (this.formMode == 2 || this.formMode == 3) {
            assetData = this.dataObject;
        } else assetData = defaultValue;

        // tiêu đề form và thông báo khi đóng form
        let title = "";
        let diaglogMessage = "";
        switch (this.formMode) {
            case this.$_MISAEnums.form.formMode.add:
                title = this.$_MISAResources.form.formTitle.add;
                diaglogMessage = this.$_MISAResources.dialog.add.message;
                break;
            case this.$_MISAEnums.form.formMode.edit:
                title = this.$_MISAResources.form.formTitle.edit;
                diaglogMessage = this.$_MISAResources.dialog.edit.message;
                break;
            case this.$_MISAEnums.form.formMode.duplicate:
                title = this.$_MISAResources.form.formTitle.duplicate;
                diaglogMessage = this.$_MISAResources.dialog.duplicate.message;
                break;
            default:
                break;
        }

        return {
            // dữ liệu tài sản
            assetData,
            // trạng thái form đã validate chưa
            status: true,
            // tiêu đề form
            title,
            // thông báo khi đóng form
            diaglogMessage,
            // hiển thị dialog
            showDialog: false,
            // trạng thái có thay đổi dữ liệu hay không
            isChange: false,
            //Lỗi trùng mã tài sản
            assetIdErrorMessage: "",
            //Cờ kiểm tra watch
            flag: false,
            // giá trị mặc định
            defaultValue,
            // trạng thái loading
            isLoading: false,
            // hiển thị thông báo lỗi
            showErrorDialog: false,
            // thông báo lỗi
            errorDialogMessage: "",
            // loading default code
            isLoadingDefaultCode: true,
            // Lỗi giá trị hao mòn năm
            depreciationValueErrorMessage: "",
        };
    },

    props: {
        dataObject: {
            type: Object,
            default: null,
        },
        formMode: {
            type: Number,
            default: 0,
        },
    },
    methods: {
        hideDialog: function () {
            this.showDialog = false;
        },
        cancelForm: function () {
            this.showDialog = false;
            this.$emit("close-form");
        },
        /**
         * Hàm lưu dữ liệu
         * Author: vtahoang (04/07/2021)
         */
        async saveForm() {
            this.status = true;
            // validate các trường dữ liệu
            let anyFocus = false;
            Object.values(this.$refs).forEach((value) => {
                if (value.isValid) {
                    value.isValid();
                    // focus vào ô đầu tiên bị lỗi
                    if (!value.isValid() && !anyFocus) {
                        value.focus();
                        anyFocus = true;
                    }

                    this.status = this.status && value.isValid();
                }
            });

            // nếu dữ liệu hợp lệ thì lưu dữ liệu
            if (this.status) {
                // hiển thị loading
                this.isLoading = true;

                // chuyển ngày tháng về dạng IOS
                this.assetData.purchase_date = this.handler.toISOStringHandler(
                    this.assetData.purchase_date
                );
                this.assetData.start_using_date = this.handler.toISOStringHandler(
                    this.assetData.start_using_date
                );

                this.$emit("update:dataObject", this.assetData);

                try {
                    // lưu dữ liệu vào database
                    switch (this.formMode) {
                        case this.$_MISAEnums.form.formMode.add:
                            await this.$_MISAApi.fixedAssetApi.createAsset(this.assetData);
                            break;
                        case this.$_MISAEnums.form.formMode.duplicate:
                            await this.$_MISAApi.fixedAssetApi.createAsset(this.assetData);
                            break;
                        case this.$_MISAEnums.form.formMode.edit:
                            await this.$_MISAApi.fixedAssetApi.editAsset(this.assetData);
                            break;
                    }

                    // đóng form
                    this.$emit("save-form");
                    // ẩn loading
                    this.isLoading = false;
                } catch (error) {
                    // ẩn loading
                    this.isLoading = false;
                    // hiển thị thông báo lỗi
                    if (error.response.data.ErrorCode == 409) {
                        this.assetIdErrorMessage = error.response.data.UserMessage;
                        this.$refs.code.focus();
                    } else {
                        this.errorDialogMessage = error.response.data.UserMessage;
                        this.showErrorDialog = true;
                    }
                }
            }
        },
        /**
         * Hàm đóng form
         * Author: vtahoang (04/07/2021)
         */
        closeForm() {
            try {
                if (this.isChange) {
                    // nếu có thay đổi dữ liệu thì hiển thị dialog
                    this.showDialog = true;
                } else {
                    this.$emit("close-form");
                }
            } catch (error) {
                console.log("closeForm ~ error:", error);
            }
        },
        /**
         * Hàm xử lý sự kiện key down trên form
         * Author: vtahoang (30/07/2021)
         */
        formKeyDownEvent(event) {
            try {
                // nếu nhấn phím ESC thì đóng form
                if (event.key == "Escape") {
                    this.closeForm();
                }

                // nếu nhấn phím Ctrl + S thì lưu dữ liệu
                if (event.ctrlKey && event.key == "s") {
                    event.preventDefault();
                    this.saveForm();
                }
            } catch (error) {
                console.log("formKeyDownEvent ~ error:", error);
            }
        },
    },
    watch: {
        dataObject: function () {
            this.assetData = this.dataObject;
        },
        assetData: {
            handler: function () {
                this.isChange = true;
            },
            deep: true,
        },
        department_code: function () {
            // Cập nhật tên phòng ban
            this.$_MISAResources.department.filter((item) => {
                if (item.department_code == this.assetData.department_code) {
                    this.assetData.department_name = item.department_name;
                    this.assetData.department_id = item.department_id;
                }
            });
        },
        fixed_asset_category_code: function () {
            // Cập nhật tên loại tài sản và số năm sử dụng
            this.$_MISAResources.fixed_asset_category.filter((item) => {
                if (item.fixed_asset_category_code == this.assetData.fixed_asset_category_code) {
                    this.assetData.fixed_asset_category_name = item.fixed_asset_category_name;
                    this.assetData.fixed_asset_category_id = item.fixed_asset_category_id;
                    this.assetData.life_time = item.life_time;
                }
            });
        },
        depreciation_rate: function () {
            if (!this.flag) {
                this.flag = true;

                // Cập nhật giá trị hao mòn năm
                if (this.assetData.cost != 0)
                    this.assetData.depreciation_value_year = (
                        (this.assetData.cost * this.assetData.depreciation_rate) /
                        100
                    ).toFixed();

                this.$nextTick(() => {
                    this.flag = false;
                });
            }
        },
        depreciation_value_year: function () {
            if (!this.flag) {
                this.flag = true;

                // // nếu giá trị hao mòn năm lớn hơn nguyên giá thì báo lỗi
                // if (this.assetData.depreciation_value_year > this.assetData.cost) {
                //     setTimeout(() => {
                //         this.depreciationValueErrorMessage =
                //             this.$_MISAResources.errorMsg.invalidDepreciationValue;
                //     }, 0);
                // }

                // // Cập nhật tỷ lệ hao mòn
                // if (this.assetData.cost != 0) {
                //     this.assetData.depreciation_rate = (
                //         (this.assetData.depreciation_value_year / this.assetData.cost) *
                //         100
                //     ).toFixed(2);
                // }

                this.$nextTick(() => {
                    this.flag = false;
                });
            }
        },
        cost: function () {
            if (!this.flag) {
                if (!isNaN(this.assetData.cost)) {
                    this.flag = true;

                    // Cập nhật giá trị hao mòn năm
                    this.assetData.depreciation_value_year = (
                        (this.assetData.cost * this.assetData.depreciation_rate) /
                        100
                    ).toFixed();
                    this.assetData.budget = null;

                    this.$nextTick(() => {
                        this.flag = false;
                    });
                }
            }
        },
        life_time: function () {
            if (!this.flag) {
                this.flag = true;

                if (this.assetData.life_time != 0) {
                    // cập nhật tỷ lệ hao mòn
                    this.assetData.depreciation_rate = (
                        (1 / this.assetData.life_time) *
                        100
                    ).toFixed(2);
                    // cập nhật giá trị hao mòn năm
                    this.assetData.depreciation_value_year = (
                        (this.assetData.cost * this.assetData.depreciation_rate) /
                        100
                    ).toFixed();
                }

                this.$nextTick(() => {
                    this.flag = false;
                });
            }
        },
    },
    computed: {
        department_code() {
            return this.assetData.department_code;
        },
        fixed_asset_category_code() {
            return this.assetData.fixed_asset_category_code;
        },
        depreciation_rate() {
            return this.assetData.depreciation_rate;
        },
        depreciation_value_year() {
            return this.assetData.depreciation_value_year;
        },
        cost() {
            return this.assetData.cost;
        },
        life_time() {
            return this.assetData.life_time;
        },
    },
};
</script>
