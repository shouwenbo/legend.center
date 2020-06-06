import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'

Vue.use(Router)

export default new Router({
  routes: [{
    path: '/',
    name: '主页',
    component: resolve => require(['@/pages/index/index'], resolve),
    children: [
      {
        path: 'Home',
        component: resolve => require(['@/pages/index/home'], resolve)
      }, {
        path: 'Info',
        component: resolve => require(['@/pages/index/info'], resolve)
      }, {
        path: 'My',
        component: resolve => require(['@/pages/index/my'], resolve)
      }
    ]
  }, {
    path: '/PassportLogin',
    name: 'PassportLogin',
    component: resolve => require(['@/pages/login/passport'], resolve)
  }, {
    path: '/HelloWorld',
    name: 'HelloWorld',
    component: HelloWorld
  }]
})
