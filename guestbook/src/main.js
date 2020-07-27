import Vue from 'vue'
import App from './App.vue'

import { router } from './router';
Vue.use(router);

import store from './store';
Vue.use(store);

import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css' // CSS
Vue.use(Vuetify)


Vue.config.productionTip = false

new Vue({
  el: '#app',
  router,
  store,
  vuetify: new Vuetify(),
  render: h => h(App)
});
