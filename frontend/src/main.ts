import Vue from 'vue'
import App from './App.vue'
import router from './router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'vue-material/dist/vue-material.min.css';
import 'vue-material/dist/theme/default.css';
import VueMaterial from 'vue-material';
import VModal from 'vue-js-modal';
import 'vue-js-modal/dist/styles.css'

Vue.config.productionTip = false
Vue.use(VueMaterial)
Vue.use(VModal)

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
