<template>
    <div class="dropdown">
        <button class="btn btn-secondary dropdown-toggle" type="button" id="addToListButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            Add To List
        </button>
        <div class="dropdown-menu" aria-labelledby="addToListButton">
            <button
                v-for="list of this.defaultLists"
                :key="list.userList.userListID"
                class="dropdown-item"
                @click="handleListClickAction(list.userList.userListID, list.userList.isDefault)">
                    <span class="list-checked">
                        <i class="far fa-check-circle" v-if="list.movieAdded"></i>
                        <i class="far fa-circle" v-else></i>
                    </span>

                    <span class="list-title">{{ list.userList.title }}</span>
            </button>


            <div v-if="this.customLists && this.customLists.length > 0">
                <div class="dropdown-divider"></div>

                <button
                    v-for="list of this.customLists"
                    :key="list.userList.userListID"
                    class="dropdown-item"
                    @click="handleListClickAction(list.userList.userListID, list.userList.isDefault)">
                    <span class="list-checked">
                        <i class="far fa-check-circle" v-if="list.movieAdded"></i>
                        <i class="far fa-circle" v-else></i>
                    </span>

                    <span class="list-title">{{ list.userList.title }}</span>
                </button>
            </div>
            
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        props: {
            defaultLists: {required: false, type: Array, default: () => {return []} },
            customLists: {required: false, type: Array, default: () => {return []} },
            title: String,
            type: String
        },
        mounted() {
            axios.get("/Content/List/GetUserLists",
                { params: {
                    "title": this.title,
                    "type": this.type
                }})
                .then(res => {
                    this.formatLists(res.data);
                })
                .catch(err => console.error(err));
        },
        methods: {
            formatLists(list) {
                list.forEach(li => {
                    li.userList.isDefault ? this.defaultLists.push(li) : this.customLists.push(li);
                });
            },
            async handleListClickAction(userListID, isDefault) {
                if (userListID && this.title && this.type) {
                    var currentList =
                        isDefault
                        ? this.defaultLists.find(list => list.userList.userListID == userListID)
                        : this.customLists.find(list => list.userList.userListID == userListID);

                    if (!currentList.movieAdded) {
                        this.addToList(userListID, isDefault);
                    }
                    else {
                        this.removeFromList(userListID, isDefault);
                    }
                }
            },
            async addToList(userListID, isDefault) {
                await axios.post("/Content/List/AddToList", {
                        "ListID": userListID,
                        "Title": this.title,
                        "Type": this.type,
                        "IsDefault": isDefault
                    })
                    .then(res => {
                        if (isDefault) {
                            var prevList = this.defaultLists.find(list => list.movieAdded == true);
                            if (prevList) {
                                prevList.movieAdded = false;
                            }

                            var defList = this.defaultLists.find(list => list.userList.userListID == userListID);
                            defList.movieAdded = true;
                        } else {
                            var cList = this.customLists.find(list => list.userList.userListID == userListID);
                            cList.movieAdded = !cList.movieAdded;
                        }
                    })
                    .catch(err => console.error(err));
            },
            async removeFromList(userListID, isDefault) {
                await axios.post("/Content/List/RemoveFromList", {
                        "ListID": userListID,
                        "Title": this.title,
                        "Type": this.type,
                        "IsDefault": isDefault
                    })
                    .then(res => {
                        var list;

                        if (isDefault) {
                            list = this.defaultLists.find(list => list.userList.userListID == userListID);
                        } else {
                            list = this.customLists.find(list => list.userList.userListID == userListID);
                        }

                        list.movieAdded = false;
                    })
                    .catch(err => console.error(err));
            }
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .dropdown-item {
        display: flex;
    }

    .list-title {
        flex: 1;
        padding-left: 5px;
    }
</style>
