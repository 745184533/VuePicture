/* eslint-disable */
import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

const originalPush = Router.prototype.push
Router.prototype.push = function push(location, onResolve, onReject) {
  if (onResolve || onReject) return originalPush.call(this, location, onResolve, onReject)
  return originalPush.call(this, location).catch(err => err)
}

export default new Router({
  routes: [


    {
      path: '/',
      redirect: '/Home',
      component: () => import(/* webpackChunkName: "home" */ '../components/Dash.vue'),
      meta: { title: '导航头部' },
      children: [
        {
          // 国际化组件
          path: '/Home',
          component: () => import(/* webpackChunkName: "i18n" */ '../components/Home.vue'),
          meta: { title: '首页' }
        },
        {
          // 国际化组件
          path: '/personal',
          redirect: '/Information',
          component: () => import(/* webpackChunkName: "i18n" */ '../components/Personal.vue'),
          meta: { title: '个人中心' },
          children:[
            {
              // 国际化组件
              path: '/Information',
              component: () => import(/* webpackChunkName: "i18n" */ '../components/Information.vue'),
              meta: { title: '个人信息' }
            },
            {
              // 国际化组件
              path: '/wallet',
              component: () => import(/* webpackChunkName: "i18n" */ '../components/wallet.vue'),
              meta: { title: '钱包' }
            },
            {
              // 国际化组件
              path: '/collection',
              component: () => import(/* webpackChunkName: "i18n" */ '../components/collection.vue'),
              meta: { title: '个人收藏' }
            },
            {
              // 国际化组件
              path: '/pushPic',
              component: () => import(/* webpackChunkName: "i18n" */ '../components/pushPic.vue'),
              meta: { title: '个人上传' }
            },
          ]
        },


        {
          // 国际化组件
          path: '/Blog',
          component: () => import(/* webpackChunkName: "i18n" */ '../components/Blog.vue'),
          meta: { title: '博客' }
        },
        {
          // 国际化组件
          path: '/pictureShow',
          component: () => import(/* webpackChunkName: "i18n" */ '../components/pictureShow.vue'),
          meta: { title: '图片' }
        },
        {
          // 国际化组件
          path: '/about',
          component: () => import(/* webpackChunkName: "i18n" */ '../components/about.vue'),
          meta: { title: '介绍' }
        },

        {
          path: '/404',
          component: () => import(/* webpackChunkName: "404" */ '../components/404.vue'),
          meta: { title: '404' }
        },

        {
          path: '/403',
          component: () => import(/* webpackChunkName: "403" */ '../components/403.vue'),
          meta: { title: '403' }
        }
      ]
    },
    {
      path: '/HelloWorld',
      component: () => import(/* webpackChunkName: "login" */ '../components/HelloWorld.vue'),
      meta: { title: '欢迎' }
    },
    {
      path: '/login',
      component: () => import(/* webpackChunkName: "login" */ '../components/Login.vue'),
      meta: { title: '登录' }
    },
    {
      path: '*',
      redirect: '/404'
    },

  ]
});
