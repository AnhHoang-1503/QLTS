<template>
    <div class="table__container unselectable" ref="container" tabindex="-1">
        <div class="table__header">
            <div class="column--checkbox" v-if="checkBoxKey" @click.stop="clickSelectAllCheckBox">
                <div class="header__item__content">
                    <MISACheckBox
                        :checked="checkBoxAllStatus"
                        @click.stop="clickSelectAllCheckBox"
                    />
                </div>
            </div>
            <div class="header__item index--column">
                <MISAToolTip :text="$_MISAResources.assetsManagementIncrease.table.indexTooltip">
                    <template #content>
                        <div class="header__item__content">
                            {{ $_MISAResources.assetsManagementIncrease.table.index }}
                        </div>
                    </template>
                </MISAToolTip>
            </div>
            <div
                class="header__item"
                v-for="(column, index) in columnsList"
                :key="index"
                :ref="`column_${index}`"
                draggable="true"
                @dragstart="dragStartEvent(index, $event)"
                @dragover.prevent=""
                @dragenter="dragEnterEvent(index, $event)"
                @dragleave="dragLeaveEvent(index, $event)"
                @drop="dropEvent(index, $event)"
            >
                <MISAToolTip :text="column.title" :overflow="true">
                    <template #content>
                        <div class="header__item__content">
                            {{ column.title }}
                        </div>
                    </template>
                </MISAToolTip>
                <div class="header__resize" @mousedown="clickInLineEvent(index, $event)"></div>
            </div>
            <div
                class="header__item column--tool"
                v-if="Object.values(toolColumn).some((item) => item)"
            >
                <div class="header__item__content"></div>
            </div>
        </div>
        <div class="table__body" ref="body">
            <div class="body__loading" v-if="isLoading">
                <MISAIcon icon="loading"></MISAIcon>
            </div>
            <div class="body__empty" v-if="isEmpty && !isLoading && showEmpty">
                <MISAIcon icon="emptyBox"></MISAIcon>
                <p class="h-empty__text">
                    {{ $_MISAResources.assetsManagement.table.empty }}
                </p>
            </div>
            <div
                class="body__row"
                v-for="(row, index) in dataList"
                :key="index"
                @dblclick="doubleClickEvent(index, $event)"
                @click.stop="clickEvent(index, $event)"
                @contextmenu="showContexMenu(index, $event)"
                :class="{
                    'body__row--selected': index == selectedRow,
                }"
            >
                <div
                    class="column--checkbox"
                    v-if="checkBoxKey"
                    @click.stop="
                        () => {
                            checkBoxList[row[this.checkBoxKey]] =
                                !checkBoxList[row[this.checkBoxKey]];
                            preselectedItem = row[this.checkBoxKey];
                        }
                    "
                    @dblclick.stop=""
                >
                    <div class="body__item__content">
                        <MISACheckBox
                            v-model:checked="checkBoxList[row[this.checkBoxKey]]"
                            ref="checkbox"
                        />
                    </div>
                </div>
                <div class="body__item index--column">
                    <div class="body__item__content">
                        {{ index + 1 + offset }}
                    </div>
                </div>
                <div
                    class="body__item"
                    v-for="(column, index) in columnsList"
                    :key="index"
                    :ref="`column_${index}`"
                >
                    <MISAToolTip :text="row[column.field]" :overflow="true">
                        <template #content>
                            <div class="body__item__content">
                                {{ row[column.field] }}
                            </div>
                        </template>
                    </MISAToolTip>
                </div>
                <div class="column--tool" v-if="Object.values(toolColumn).some((item) => item)">
                    <div class="body__item__content--tool">
                        <MISAToolTip :text="$_MISAResources.table.edit">
                            <template #content>
                                <div
                                    class="tool__btn tool-edit"
                                    v-if="toolColumn.edit"
                                    @click.stop="editRow(index)"
                                    @dblclick.stop=""
                                >
                                    <MISAIcon icon="btn--edit" />
                                </div>
                            </template>
                        </MISAToolTip>
                        <MISAToolTip :text="$_MISAResources.table.delete">
                            <template #content>
                                <div
                                    class="tool__btn tool-delete"
                                    v-if="toolColumn.delete"
                                    @click.stop="deleteRow(index)"
                                    @dblclick.stop=""
                                >
                                    <MISAIcon icon="delete" />
                                </div>
                            </template>
                        </MISAToolTip>
                    </div>
                </div>
            </div>
        </div>
        <div class="table__footer" v-if="showFooter">
            <div class="column--checkbox" v-if="checkBoxKey"></div>
            <div class="footer__item index--column">
                <div class="footer__item__content"></div>
            </div>
            <div
                class="footer__item"
                v-for="(column, index) in columnsList"
                :key="index"
                :ref="`column_${index}`"
            >
                <MISAToolTip :text="footer[column.field]" :overflow="true">
                    <template #content>
                        <div class="footer__item__content">
                            {{ footer[column.field] }}
                        </div>
                    </template>
                </MISAToolTip>
            </div>
            <div class="column--tool"></div>
        </div>
        <Teleport to="#app">
            <MISAContextMenu
                v-if="Object.values(toolColumn).some((item) => item)"
                v-show="isShowContextMenu"
                :x="contextMenuPosition.x"
                :y="contextMenuPosition.y"
                v-model:show="isShowContextMenu"
                @editAsset="editRow(contextMenuTarget)"
                @deleteAsset="deleteRow(contextMenuTarget)"
                @click-out-side="hideContextMenu"
                :tool="toolColumn"
                :item="item"
            ></MISAContextMenu>
        </Teleport>
    </div>
</template>

<style scoped>
@import url(./Table.css);
</style>

<script>
import MISACheckBox from "@/components/base/MISANewCheckBox/MISACheckBox.vue";
import MISAContextMenu from "@/components/base/MISAContextMenu/MISAContextMenu.vue";

export default {
    name: "Table",
    components: {
        MISACheckBox,
        MISAContextMenu,
    },
    data() {
        return {
            // Danh sách cột
            columnsList: this.columns,
            // Danh sách các ô check box
            checkBoxList: {},
            // Danh sách dữ liệu
            dataList: JSON.parse(JSON.stringify(this.tableData)),
            // Cờ kiểm tra watch
            watchFlag: false,
            // Cờ kiểm tra double click
            doubleClickFlag: false,
            // dòng được chọn trước đó
            preselectedItem: null,
            // nhấn shift
            shiftKey: false,
            // hiển thị context menu
            isShowContextMenu: false,
            // vị trí context menu
            contextMenuPosition: {
                x: 0,
                y: 0,
            },
            // target context menu
            contextMenuTarget: null,
            // cờ resize
            resize: false,
            // Cột được chọn
            columsTager: null,
            // Vị trí chuột khi click vào line
            preMouseX: 0,
            // Chiều rộng trước đó
            preWidth: 0,
            // Chiều rộng của table
            tableWidth: 0,
        };
    },
    props: {
        //Độ lệch trang dữ liệu
        offset: {
            type: Number,
            default: 0,
        },
        // danh sách dữ liệu
        tableData: {
            type: Array,
            default: () => [],
        },
        // danh sách các cột
        columns: {
            type: Array,
            default: () => [],
        },
        footer: {
            type: Object,
            default: () => {
                return {
                    vouchers_code2: 30000,
                };
            },
        },
        // có hiển thị index hay không
        index: {
            type: Boolean,
            default: true,
        },
        // giá trị dùng làm key cho radio checkbox
        checkBoxKey: {
            type: String,
            default: "",
        },
        // Các item đc chọn
        selectedList: {
            type: Object,
            default: {},
        },
        // Hiện footer
        showFooter: {
            type: Boolean,
            default: true,
        },
        // Hiện cột công cụ (sửa, xóa, nhân bản)
        toolColumn: {
            type: Object,
            default: () => {
                return {
                    edit: false,
                    delete: false,
                    duplicate: false,
                };
            },
        },
        // Trạng thái loading
        isLoading: {
            type: Boolean,
            default: false,
        },
        // Hiện thị khi dữ liệu rỗng
        showEmpty: {
            type: Boolean,
            default: true,
        },
        // click hàng để chọn checkbox
        clickRowToSelect: {
            type: Boolean,
            default: false,
        },
        // Hàng được chọn
        selectedRow: {
            type: Number,
            default: -1,
        },
        // tên đối tượng Assets | Vouchers
        item: {
            type: String,
            default: "Assets",
        },
    },
    methods: {
        /**
         * Thay đổi độ cao tối đa body
         * Author: vtahoang (16/08/2023)
         */
        updateBodyMaxheight() {
            this.$nextTick(() => {
                // set max height cho body
                let bodyHeight = this.$refs.container.offsetHeight - 42;
                if (this.showFooter) {
                    bodyHeight -= 42;
                }

                this.$refs.body.style["max-height"] = `${bodyHeight}px`;
            });
        },
        /**
         * Sự kiện double click vào 1 hàng
         * @param {*} index target
         * Author: vtahoang (21/08/2023)
         */
        doubleClickEvent(index, event) {
            this.$emit("double-click", index);
        },
        /**
         * Sự kiện click vào 1 hàng
         * @param {*} index target
         * Author: vtahoang (21/08/2023)
         */
        clickEvent(index, event) {
            this.hideContextMenu();
            if (!this.doubleClickFlag) {
                this.$emit("click-row", index);

                // Chọn nhiều bằng shift
                if (event.shiftKey) {
                    if (this.preselectedItem !== null) {
                        let keys = Object.keys(this.checkBoxList);
                        let start = index;
                        let end = keys.indexOf(this.preselectedItem);
                        if (start > end) {
                            let temp = start;
                            start = end;
                            end = temp;
                        }
                        for (let i = start; i <= end; i++) {
                            this.checkBoxList[keys[i]] = true;
                        }
                        this.preselectedItem = this.dataList[index][this.checkBoxKey];
                        return;
                    }
                }

                if (this.clickRowToSelect || event.ctrlKey || event.shiftKey) {
                    let id = this.dataList[index][this.checkBoxKey];
                    this.checkBoxList[id] = !this.checkBoxList[id];
                    this.preselectedItem = id;
                }

                // // Chọn nhiều bằng shift
                // if (this.shiftKey) {
                //     if (!this.preselectedItem) {
                //         this.preselectedItem = this.dataList[index][this.checkBoxKey];
                //         return;
                //     }
                //     let keys = Object.keys(this.checkBoxList);
                //     let start = index;
                //     let end = keys.indexOf(this.preselectedItem);
                //     if (start > end) {
                //         let temp = start;
                //         start = end;
                //         end = temp;
                //     }
                //     for (let i = start; i < end; i++) {
                //         this.checkBoxList[keys[i]] = true;
                //     }
                // }
            }
        },
        /**
         * click button sửa
         * @param {*} index index data
         * Author: vtahoang (22/08/2023)
         */
        editRow(index) {
            this.hideContextMenu();
            this.$emit("edit-row", index);
        },
        /**
         * click button xoá
         * @param {*} index index data
         * Author: vtahoang (22/08/2023)
         */
        deleteRow(index) {
            this.hideContextMenu();
            this.$emit("delete-row", index);
        },
        /**
         * Sự kiện click vào check box all
         * Author: vtahoang (18/08/2023)
         */
        clickSelectAllCheckBox() {
            this.hideContextMenu();
            if (this.checkBoxKey) {
                let status = this.checkBoxAllStatus;
                this.dataList
                    .map((item) => item[this.checkBoxKey])
                    .forEach((key) => {
                        this.checkBoxList[key] = !status;
                    });
            }
        },
        /**
         * Cập nhật css cho các cột
         * Author: vtahoang (18/08/2023)
         */
        updateCSS() {
            // css cho các cột
            for (let i = 0; i < this.columnsList.length; i++) {
                this.$refs[`column_${i}`].forEach((item, index) => {
                    // xoá các giá trị cũ
                    item.style.width = null;
                    item.style.flex = null;
                    // thêm width cho các cột
                    if (this.columnsList[i].width) {
                        item.style.width = this.columnsList[i].width;
                    } else {
                        // nếu không có width thì set flex = 1
                        item.style.flex = 1;
                    }

                    // căn chỉnh cho các cột
                    switch (this.columnsList[i].align) {
                        case "left":
                            item.style["justify-content"] = "start";
                            break;
                        case "right":
                            item.style["justify-content"] = "end";
                            break;
                        case "center":
                            item.style["justify-content"] = "center";
                            break;
                        default:
                            item.style["justify-content"] = "start";
                            break;
                    }

                    // thêm màu cho các cột (trừ header)
                    if (this.columnsList[i].color && index !== 0) {
                        item.style.color = this.columnsList[i].color;
                    } else {
                        item.style.color = "#000";
                    }
                });

                // Xử lý các cột có kiểu dữ liệu số
                if (this.columnsList[i].type === "number") {
                    this.dataList.forEach((row) => {
                        row[this.columnsList[i].field] = this.handler.numberHandler(
                            row[this.columnsList[i].field]
                        );
                    });
                }
                // Xử lý các cột có kiểu dữ liệu số
                if (this.columnsList[i].type === "date") {
                    this.dataList.forEach((row) => {
                        // kiểm tra xem string đã đúng định dạng chưa
                        const regex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
                        if (row[this.columnsList[i].field].match(regex)) return;

                        row[this.columnsList[i].field] = this.handler.toDateTime(
                            row[this.columnsList[i].field]
                        );
                    });
                }
            }
            // set max height cho body
            this.updateBodyMaxheight();
        },

        /**
         * Xóa các item đã chọn
         * Author: vtahoang (21/08/2023)
         */
        clearSelectedItems(exept = []) {
            for (let key in this.checkBoxList) {
                if (!exept.includes(key)) {
                    this.checkBoxList[key] = false;
                }
            }
        },
        /**
         * Sự kiện key down
         * @param {*} event
         * Author: vtahoang (29/08/2023)
         */
        keyDownEvent(event) {
            if (event.key === "Shift") {
                this.shiftKey = true;
            }
        },
        /**
         * Sự kiện key up event
         * @param {*} event
         * Author: vtahoang (06/09/2023)
         */
        keyUpEvent(event) {
            if (event.key === "Shift") {
                this.shiftKey = false;
                this.preselectedItem = null;
            }
        },
        /**
         * Hiển thị context menu
         * @param {*} index target
         * @param {*} event
         * Author: vtahoang (29/08/2023)
         */
        showContexMenu(index, event) {
            event.preventDefault();
            // chặn event click outside
            event.stopPropagation();
            // gán target
            this.contextMenuTarget = index;
            // gán vị trí của context menu
            this.contextMenuPosition.x = event.x;

            this.contextMenuPosition.y = event.y;

            // hiển thị context menu
            this.isShowContextMenu = true;
        },
        /**
         * Ẩn context menu
         * Author: vtahoang (29/08/2023)
         */
        hideContextMenu() {
            this.isShowContextMenu = false;
        },
        /**
         * Sự kiện drag start
         * @param {Number} index index target
         * @param {Event} event
         * Author: vtahoang (06/09/2023)
         */
        dragStartEvent(index, event) {
            if (this.resize) event.preventDefault();

            event.dataTransfer.setData("text/plain", index);
        },
        /**
         * Sự kiện drag enter
         * @param {Number} index index target
         * @param {Event} event
         * Author: vtahoang (06/09/2023)
         */
        dragEnterEvent(index, event) {
            this.$refs[`column_${index}`][0].classList.add("dragover");
        },
        /**
         * Sự kiện drag leave
         * @param {Number} index index target
         * @param {Event} event
         * Author: vtahoang (06/09/2023)
         */
        dragLeaveEvent(index, event) {
            if (!this.$refs[`column_${index}`][0].contains(event.relatedTarget))
                this.$refs[`column_${index}`][0].classList.remove("dragover");
        },
        /**
         * Sự kiện drop
         * @param {Number} index index target
         * @param {Event} event
         * Author: vtahoang (06/09/2023)
         */
        dropEvent(index, event) {
            this.$refs[`column_${index}`][0].classList.remove("dragover");
            let preIndex = event.dataTransfer.getData("text/plain");
            // Thêm cột vào vị trí mới
            let temp = this.columnsList[preIndex];
            this.columnsList.splice(preIndex, 1);
            this.columnsList.splice(index, 0, temp);

            // Cập nhật lại css
            this.$nextTick(() => {
                this.updateCSS();
            });
            // gửi sự kiện update columns ra ngoài
            this.$emit("update-columns", this.columnsList);
        },
        /**
         * Sự kiện click vào line resize
         * @param {Number} index index cột
         * @param {Event} event Sự kiện
         * Author: vtahoang (06/09/2023)
         */
        clickInLineEvent(index, event) {
            this.resize = true;
            this.columsTager = index;
            this.preMouseX = event.clientX;
            this.preWidth = this.$refs[`column_${index}`][0].offsetWidth;
            this.tableWidth = this.$refs.container.offsetWidth;
            window.addEventListener("mousemove", this.resizeEvent);
        },
        /**
         * Sự kiện resize cột
         * Author: vtahoang (06/09/2023)
         */
        resizeEvent(event) {
            if (this.resize) {
                let changeWidth = event.clientX - this.preMouseX;

                if (this.preWidth + changeWidth < 100) return;

                let temp = this.columnsList[this.columsTager].width;
                this.columnsList[this.columsTager].width = `${this.preWidth + changeWidth}px`;

                this.$nextTick(() => {
                    this.updateCSS();
                    // Nếu thay đổi chiều rộng của bảng thì không cho resize
                    if (this.$refs.container.offsetWidth > this.tableWidth) {
                        this.columnsList[this.columsTager].width = temp;
                        this.$nextTick(() => {
                            this.updateCSS();
                        });
                    }
                });
            }

            this.handler.debounce(() => {
                this.$emit("update-columns", this.columnsList);
            }, 1000)();
        },
    },
    mounted() {
        this.$nextTick(() => {
            this.updateCSS();
        });

        // Huỷ cờ resize khi nhấn chuột ra ngoài
        window.addEventListener("mouseup", () => {
            this.resize = false;
            window.removeEventListener("mousemove", this.resizeEvent);
        });

        window.addEventListener("keydown", this.keyDownEvent);
        window.addEventListener("keyup", this.keyUpEvent);
    },
    beforeUnmount() {
        window.removeEventListener("keydown", this.keyDownEvent);
        window.removeEventListener("keyup", this.keyUpEvent);
    },
    watch: {
        /**
         * Cập nhật dữ liệu bảng
         * Author: vtahoang (21/08/2023)
         */
        tableData() {
            this.dataList = JSON.parse(JSON.stringify(this.tableData));
            this.dataList.forEach((item, index) => {
                item.index = index + 1 + this.offset;
            });

            // tạo danh sách checkbox theo key, data và selectedList
            if (this.checkBoxKey) {
                this.dataList.forEach((item) => {
                    this.checkBoxList[item[this.checkBoxKey]] = false;
                });
                // thêm các item đã chọn vào danh sách checkbox
                this.checkBoxList = { ...this.checkBoxList, ...this.selectedList };
            }

            this.$nextTick(() => {
                this.updateCSS();
            });
        },
        /**
         * Cập nhật danh sách chọn
         * Author: vtahoang (21/08/2023)
         */
        checkBoxList: {
            handler() {
                this.$emit("update-selected-list", this.checkBoxList);
            },
            deep: true,
        },
    },
    computed: {
        /**
         * Trạng thái check box all
         * Author: vtahoang (18/08/2023)
         */
        checkBoxAllStatus() {
            if (this.checkBoxKey) {
                // let status = Object.values(this.checkBoxList).every((item) => item);
                let status = this.dataList
                    .map((item) => item[this.checkBoxKey])
                    .every((key) => {
                        return this.checkBoxList[key];
                    });
                return status && this.dataList.length > 0;
            }
        },
        /**
         * Kiểm tra dữ liệu có rỗng hay không
         * Author: vtahoang (21/08/2023)
         */
        isEmpty() {
            if (this.dataList.length <= 0) {
                return true;
            } else {
                return false;
            }
        },
    },
};
</script>
