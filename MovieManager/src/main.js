import Vue from 'vue'
import AddToList from "./components/AddToList";
import ListsTable from "./components/ListsTable.vue";

Vue.config.productionTip = false

Vue.component("add-to-list", AddToList);
Vue.component("lists-table", ListsTable);

window.Vue = Vue;