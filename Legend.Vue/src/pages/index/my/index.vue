<template>
  <div>
    <van-cell is-link class="middleArrow">
      <template #title>
        <van-row>
          <van-col span="6">
            <van-image width="4rem"
                       height="4rem"
                       :src="defaulrAvatar" />
          </van-col>
          <van-col span="18">
            <div class="label" style="text-align:left;">昵称</div>
            <div class="label" style="text-align:left;margin-top:0.5rem;">13333333333</div>
          </van-col>
        </van-row>
      </template>
    </van-cell>
  </div>
</template>

<script>
import defaulrAvatar from '@/assets/happyeveryday.jpg'
export default {
  name: 'My',
  data () {
    return {
      msg: 'Welcome to Your Vue.js App',
      defaulrAvatar
    }
  }
}
</script>

<style scoped>
.middleArrow {
  background-color:blue;
}
.middleArrow .label{
  color:white;
}
</style>
