<template>
  <div>
    <router-view />
    <van-tabbar v-model="tabbarIndex"
                @change="onTabbarChange">
      <van-tabbar-item icon="home-o"
                       to="/home">首页</van-tabbar-item>
      <van-tabbar-item icon="search">会员</van-tabbar-item>
      <van-tabbar-item icon="friends-o"
                       to="/info">资讯</van-tabbar-item>
      <van-tabbar-item icon="setting-o"
                       to="/my">我的</van-tabbar-item>
    </van-tabbar>
  </div>
</template>

<script>
import fetch from '@/api/fetch'
export default {
  name: 'Index',
  data () {
    return {
      tabbarIndex: 1,
      msg: 'Welcome to Your Vue.js App'
    }
  },
  methods: {
    onTabbarChange (index) {
      fetch({
        url: `/api/PassPort/info`,
        method: 'post',
        data: {
          parentId: 1
        }
      }).then(data => {
        console.log(data, 123456)
      })

      // fetch.post(`http://172.20.70.152:53882/api/PassPort/login`, {}).then(data => {
      //   console.log(data, 123456)
      // })
    }
  }
}
</script>
