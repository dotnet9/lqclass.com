import Vue from "vue";
import Vuex from "vuex";
import reqLogin from "@/service/login";
import createStore from "@/store/base/index";
Vue.use(Vuex);
const newStore = createStore(reqLogin);

export default new Vuex.Store({
    strict: true,
    getters: {},
    ...newStore
});
