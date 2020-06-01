import Vue from 'vue'
import Vuex from 'vuex'
import serviceOrder from './modules/serviceOrder/index'

Vue.use(Vuex)

const store = new Vuex.Store({
  modules: {
    ...serviceOrder
  }
})

export default store
