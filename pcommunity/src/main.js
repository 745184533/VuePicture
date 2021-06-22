/* eslint-disable */
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import './plugins/element.js'
import axios from 'axios'
import VueParticles from 'vue-particles'

Vue.use(VueParticles)

Vue.prototype.$axios = axios    //全局注册，使用方法为:this.$axios

Vue.config.productionTip = false

axios.defaults.baseURL = 'http://localhost:6306/'
//使用钩子函数对路由进行权限跳转
router.beforeEach((to, from, next) => {
  document.title = `${to.meta.title} | PictureCommunity`;
  const role = sessionStorage.getItem('token');
  if (!role && to.path !== '/login') {
    console.log("回到登陆页面",to.path);
    next('/login');
  } else if (to.meta.permission) {
    // 如果是管理员权限则可进入，这里只是简单的模拟管理员权限而已
    role === 'admin' ? next() : next('/403');
  } else {
    // 简单的判断IE10及以下不进入富文本编辑器，该组件不兼容
    if (navigator.userAgent.indexOf('MSIE') > -1 && to.path === '/editor') {
      Vue.prototype.$alert('vue-quill-editor组件不兼容IE10及以下浏览器，请使用更高版本的浏览器查看', '浏览器不兼容通知', {
        confirmButtonText: '确定'
      });
    } else {
      console.log("正常准备进入下一个页面",to.path);
      next();
    }
  }
});
new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
