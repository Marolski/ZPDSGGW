import Vue from 'vue'
import App from './App.vue'
import router from './router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'vue-material/dist/vue-material.min.css';
import 'vue-material/dist/theme/default.css';
import VueMaterial from 'vue-material';
import VModal from 'vue-js-modal';
import 'vue-js-modal/dist/styles.css';
import '@fortawesome/fontawesome-free/css/all.min.css'; 
import 'bootstrap-css-only/css/bootstrap.min.css'; 
import 'mdbvue/lib/mdbvue.css';
import Antd from 'ant-design-vue';
import 'ant-design-vue/dist/antd.css';
import { message } from 'ant-design-vue'

Vue.use(Antd);

Vue.config.productionTip = false
Vue.prototype.$message = message;
Vue.use(VueMaterial)
Vue.use(VModal)

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
