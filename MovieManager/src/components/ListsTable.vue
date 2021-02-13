<template>

<div>
    <table>
        <tr class="list-headers">
            <th>List</th>
            <th>Actions</th>
        </tr>

        <tr v-for="list of this.lists" :key="list.userListID" class="list-row">
            <td>
                <a :href="'/Content/List/Overview/' + list.userListID">
                    {{ list.title }}
                </a>
            </td>

            <td>
                <button 
                        class="btn btn-danger"
                        @click="deleteList(list.userListID)"
                        v-if="!list.isDefault && deletingList != list.userListID">
                    Delete
                </button>
                <button 
                        class="btn btn-danger"
                        ref="deleteConfirmButton"
                        @click="deleteList(list.userListID)"
                        @blur="resetDeleteConfirm()"
                        v-if="!list.isDefault && deletingList == list.userListID">
                    Confirm Deletion
                </button>
            </td>
        </tr>

        <tr v-if="this.creating" class="list-row">
            <td>
                <input class="form-control" type="text" v-model="listTitle" placeholder="List title" ref="listCreateInput" @blur="resetForm()">
            </td>
            <td>
                <button class="btn btn-success" @click="createList()">Create</button>
            </td>
        </tr>

    </table>

    <span class="create-list-btn" @click="showCreateForm()">
        <i class="fas fa-plus-circle"></i> <span class="create-list-text">Add to list</span>
    </span>
</div>

</template>

<script>
import axios from 'axios';

export default {
    props: {
        lists: {required: false, type: Array, default: () => {return []} },
        creating: {required: false, type: Boolean, default: () => {return false} },
        deletingList: {required: false, type: Number, default: () => {return -1} },
        listTitle: {required: false, type: String, default: () => {return ""} }
    },
    mounted() {
        axios.get("/Content/List/GetLists")
            .then(res => {
                this.lists = res.data;
            })
            .catch(err => console.error(err));
    },
    methods: {
        async viewList(listID) {
            axios.get("/Content/List/Overview/" + listID);
        },
        showCreateForm() {
            this.resetForm();

            this.creating = true;

            this.$nextTick(() => {
                this.$refs.listCreateInput.focus()
            });
        },
        resetForm() {
            this.listTitle = "";
            this.creating = false;
        },
        resetDeleteConfirm() {
            this.deletingList = -1;
        },
        async createList() {
            if (this.listTitle != "" && this.listTitle != null) {
                await axios.post("/Content/List/Create", {
                    "Title": this.listTitle
                })
                .then(res => {
                    this.lists.push(res.data);
                    this.resetForm();
                })
                .catch(err => console.error(err));
            } 
        },
        async deleteList(listID) {
            if (this.deletingList == -1 || this.deleteList != listID) {
                this.deletingList = listID;

                this.$nextTick(() => {
                    this.$refs.deleteConfirmButton[0].focus()
                });
            }
            else {
                this.deletingList = -1;
                await axios.delete("/Content/List/Delete/" + listID)
                    .then(res => {
                        var deletedList = this.lists.find(l => l.userListID == res.data);
                        this.lists.splice(this.lists.indexOf(deletedList), 1);
                    })
                    .catch(err => console.log(err));
            }
        }
    }
}
</script>

<style scoped>
    table {
        width: 100%;
    }

    .list-headers {
        font-size: 2.2vh;
    }

    .list-headers, .list-row {
        height: 5vh;
    }

    .list-row:nth-child(2n) {
        background-color: rgb(235, 235, 235);
    }

    .list-row td {
        font-size: 1.8vh;
    }

    .list-row td:nth-child(1n), .list-headers th:nth-child(1n) {
        padding-left: 15px;
        width: 80%;
    }

    .list-row td:nth-child(2n), .list-headers th:nth-child(2n) {
        text-align: center;
    }

    .create-list-btn {
        display: block;
        margin-top: 20px;
        font-size: 2vh;
        color: #0f7864
    }

    .create-list-text {
        display: inline-block;
        color: black;
        font-size: 2.3vh;
    }
</style>