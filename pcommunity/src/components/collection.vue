<template>
  <div class="imageList">
    <div class="SingleImage" v-for="(item,i) in srcList" :key="i" v-if="i<10">
      <el-card style="width: 300px;height: 391px;" :body-style="{padding:'0px' }" shadow="hover">
        <img :src=item.thatpicture.pUrl alt="暂无图片"   class="image" @click="showPicture(item.thatpicture.pId)">
        <div style="padding: 14px;">

          <el-badge :value=item.like class="item">
            <el-button type="primary"  size="small">
              <i class="el-icon-check"></i>
              点赞</el-button>
          </el-badge>

          <el-badge :value=item.favorite class="item" type="primary">
            <el-button type="primary" size="small">
              <i class="el-icon-star-on"></i>
              收藏</el-button>
          </el-badge>

          <el-badge :value=item.comment class="item" type="warning">
            <el-button type="primary" icon="el-icon-s-comment" size="small">评论</el-button>
          </el-badge>

        </div>
      </el-card>
    </div>
  </div>

</template>

<script>
export default {
  name: "collection",
  data(){
    return{
      srcList: [
        {
          like: 12,
          favorite: 13,
          comment: 15,
          thatpicture: {
            checkinfos: [],
            commNum: 0,
            dislikes: 0,
            favoritepictures: [],
            likes: 1,
            likespictures: [],
            orderpics: [],
            owntags: [],
            pHeight: 640,
            pId: "1",
            pInfo: "liyifan",
            pStatus: "OK",
            pUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
            pWidth: 960,
            piccomments: [],
            price: 18,
            publishpictures: 0
          },
        },
        {
          like: 12,
          favorite: 13,
          comment: 15,
          thatpicture: {
            checkinfos: [],
            commNum: 0,
            dislikes: 0,
            favoritepictures: [],
            likes: 1,
            likespictures: [],
            orderpics: [],
            owntags: [],
            pHeight: 640,
            pId: "1",
            pInfo: "liyifan",
            pStatus: "OK",
            pUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
            pWidth: 960,
            piccomments: [],
            price: 18,
            publishpictures: 0
          },
        },
        {
          like: 12,
          favorite: 13,
          comment: 15,
          thatpicture: {
            checkinfos: [],
            commNum: 0,
            dislikes: 0,
            favoritepictures: [],
            likes: 1,
            likespictures: [],
            orderpics: [],
            owntags: [],
            pHeight: 640,
            pId: "1",
            pInfo: "liyifan",
            pStatus: "OK",
            pUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
            pWidth: 960,
            piccomments: [],
            price: 18,
            publishpictures: 0
          },
        },
        {
          like: 12,
          favorite: 13,
          comment: 15,
          thatpicture: {
            checkinfos: [],
            commNum: 0,
            dislikes: 0,
            favoritepictures: [],
            likes: 1,
            likespictures: [],
            orderpics: [],
            owntags: [],
            pHeight: 640,
            pId: "1",
            pInfo: "liyifan",
            pStatus: "OK",
            pUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
            pWidth: 960,
            piccomments: [],
            price: 18,
            publishpictures: 0
          },
        },
        {
          like: 12,
          favorite: 13,
          comment: 15,
          thatpicture: {
            checkinfos: [],
            commNum: 0,
            dislikes: 0,
            favoritepictures: [],
            likes: 1,
            likespictures: [],
            orderpics: [],
            owntags: [],
            pHeight: 640,
            pId: "1",
            pInfo: "liyifan",
            pStatus: "OK",
            pUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
            pWidth: 960,
            piccomments: [],
            price: 18,
            publishpictures: 0
          },
        },
        {
          like: 12,
          favorite: 13,
          comment: 15,
          thatpicture: {
            checkinfos: [],
            commNum: 0,
            dislikes: 0,
            favoritepictures: [],
            likes: 1,
            likespictures: [],
            orderpics: [],
            owntags: [],
            pHeight: 640,
            pId: "1",
            pInfo: "liyifan",
            pStatus: "OK",
            pUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
            pWidth: 960,
            piccomments: [],
            price: 18,
            publishpictures: 0
          },
        },
      ],
    }
  },
  methods:{
    showPicture(key){
      let temp="?userId="+sessionStorage.getItem("userId")+"&picId="+key
      this.$router.push("/pictureShow"+temp);
    },
  },
  mounted() {
    let _this = this;

    let tempId = sessionStorage.getItem("userId");

    _this.$axios(
        {
          method: 'get',
          url: 'http://localhost:6306/Account/getProfilePicture?userId=' + tempId,
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function (response) {
      let Temp = response.data.favorite;
      console.log(Temp);
      _this.srcList=Temp;
      console.log(_this.srcList);

    }).catch(function (response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!', response);
      return false;
    });


  }
}
</script>

<style scoped>
.imageList{
  display: flex;
  flex-wrap:wrap;
  /*height: 500px;*/
  width: 100%;
  overflow-y: auto;
  align-items: safe center;
  justify-content: safe center;
  padding-top: 50px;
  margin: auto;
  /*background: #00a854;*/
}

.SingleImage{
  display: block;
  width: 330px;
  height: 400px;
  padding-left: 45px;
  padding-top: 20px;
  margin: auto;
  /*background: #999999;*/
  /*margin-left: 40px;*/
  /*margin-top: 40px;*/
  /*height: 200px;*/
}
.image {
  width: 100%;
  display: block;
}
.item{
  padding-right: 15px;
}
</style>