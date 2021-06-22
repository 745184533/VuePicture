<template>
  <el-container class="homepage">
    <el-aside>
      <el-menu
          :default-active="activeIndex[0]"
          class="el-menu-vertical-demo"
          @open="handleOpen"
          @close="handleClose"
          background-color="#606266"
          text-color="#fff"
          :router="true"
          active-text-color="#ffd04b"
          style="height: 100%;width: 300px;"
      >
        <el-menu-item index="/Information">
          <template slot="title">
            <i class="el-icon-location"></i>
            <span>个人信息</span>
          </template>
        </el-menu-item>
        <el-menu-item index="/wallet">
          <i class="el-icon-menu"></i>
          <span slot="title">钱包</span>
        </el-menu-item>
        <el-menu-item index="/collection">
          <i class="el-icon-document"></i>
          <span slot="title">个人收藏</span>
        </el-menu-item>
        <el-menu-item index="/pushPic">
          <i class="el-icon-setting"></i>
          <span slot="title">个人上传</span>
        </el-menu-item>
      </el-menu>

    </el-aside>
    <el-main>
      <router-view></router-view>
    </el-main>

  </el-container>

</template>

<script>
export default {
  name: "personal",
  data(){
    return{
      activeIndex:["/Information","/wallet","/collection","/pushPic"],
      dialogVisible:false,
    }
  },
  methods:{
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    }
  }
}
</script>

<style scoped>

.homepage{
  overflow-y: scroll;
  overflow-x: hidden;
  height: 680px;
  background: white;
}
/*滚动条里面轨道*/
.homepage::-webkit-scrollbar-track{
  -webkit-box-shadow: inset 0 0 6px rgba(255, 255, 255, 0);
  background-color: rgb(27, 27, 92);
}
/*定义滚动条整体的样式*/
.homepage::-webkit-scrollbar{
  width: 6px;
  background-color: rgb(20, 19, 19);
}
/*滚动条的样式*/
.homepage::-webkit-scrollbar-thumb{
  height:20px;
  background-image: -webkit-gradient(linear,
  left bottom,
  left top,
  color-stop(0.2, rgb(125, 126, 128)),
  color-stop(0.4, rgb(97, 98, 99)),
  color-stop(0.8, rgb(125, 126, 128)));
}

</style>