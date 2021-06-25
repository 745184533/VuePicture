<template>
  <div class="homepage">

    <div class="home-wrap">

      <h1 style="padding-top: 200px;padding-left:600px;color: white;align-content: center;">免费正版高清图片素材库</h1>
      <h4 style="margin-top:30px; padding-left: 500px;color: #171616;">图片社区拥有超过许多优质图片素材，让你轻松应对各种设计场景</h4>

      <div id="search">


        <el-autocomplete style="padding-left: 5px;z-index: 5;width: 400px"
                         class="inline-input"
                         v-model="state1"
                         :fetch-suggestions="querySearch"
                         autosize
                         placeholder="请输入内容"
                         @select="handleSelect"
                         clearable
        ></el-autocomplete>
        <el-button slot="append" icon="el-icon-search" @click="serachByTag"></el-button>

      </div>

    </div>


    <div class="imageList">
      <div class="SingleImage" v-for="(item,i) in srcList" :key="i">
        <el-card style="width: 300px;height: 391px;" :body-style="{padding:'0px' }" shadow="hover">
          <img :src=item.picUrl alt="暂无图片" class="image" @click="showPicture(item.picId)">
          <div style="padding: 14px;">
            <el-badge :value=item.likeNum class="item">
              <el-button type="primary" size="small"
                         @click="changeIcon1($event,i)">
                <i class="el-icon-check"></i>
                点赞
              </el-button>
            </el-badge>
            <el-badge :value=item.starNum class="item" type="primary">
              <el-button type="primary" size="small"
                         @click="changeIcon2($event,i)">
                <i class="el-icon-star-on"></i>

                收藏
              </el-button>
            </el-badge>
            <el-badge :value=item.commNum class="item" type="warning">
              <el-button type="primary" icon="el-icon-s-comment" size="small">评论</el-button>
            </el-badge>
          </div>
        </el-card>
      </div>
    </div>


  </div>

</template>

<script>
export default {
  /* eslint-disable */
  name: "Home",
  data() {
    return {
      thistime: "0",
      restaurants: [],
      naoTan: [{"owntags": "", "tagName": ""}],
      state1: '',
      srcList: [
        {
          picId: "99",
          picUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
          publisherId: "1",
          publisherName: "root",
          info: "NULL",
          likeNum: 7,
          likeicon: "el-icon-check",
          starNum: 1,
          favoriteicon: "el-icon-star-on",
          commNum: 2
        },
        {
          picId: "99",
          picUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
          publisherId: "1",
          publisherName: "root",
          info: "NULL",
          likeNum: 7,
          likeicon: "el-icon-check",
          starNum: 1,
          favoriteicon: "el-icon-star-on",
          commNum: 2
        },
        {
          picId: "99",
          picUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
          publisherId: "1",
          publisherName: "root",
          info: "NULL",
          likeNum: 7,
          likeicon: "el-icon-check",
          starNum: 1,
          favoriteicon: "el-icon-star-on",
          commNum: 2
        },
        {
          picId: "99",
          picUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
          publisherId: "1",
          publisherName: "root",
          info: "NULL",
          likeNum: 7,
          likeicon: "el-icon-check",
          starNum: 1,
          favoriteicon: "el-icon-star-on",
          commNum: 2
        },
        {
          picId: "99",
          picUrl: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
          publisherId: "1",
          publisherName: "root",
          info: "NULL",
          likeNum: 7,
          likeicon: "el-icon-check",
          starNum: 1,
          favoriteicon: "el-icon-star-on",
          commNum: 2
        },
      ],
    };
  },
  methods: {

    showPicture(key) {
      let temp = "?picId=" + key;

      this.$router.push("/pictureShow" + temp);
    },

    changeIcon1(event, key) {
      let temp = event.currentTarget.querySelector("i");
      console.log(temp); // event.currentTarget获取当前点击元素DOM
      this.srcList[key].likeicon = "el-icon-plus";
      console.log(temp); // event.currentTarget获取当前点击元素DOM

    },

    changeIcon2(event, key) {
      let temp = event.currentTarget.querySelector("i");
      console.log(temp); // event.currentTarget获取当前点击元素DOM
      this.srcList[key].favoriteicon = "el-icon-star-on";
      console.log(temp); // event.currentTarget获取当前点击元素DOM

    },

    querySearch(queryString, cb) {
      let restaurants = this.restaurants;
      let results = queryString ? restaurants.filter(this.createFilter(queryString)) : restaurants;
      // 调用 callback 返回建议列表的数据
      cb(results);
    },

    createFilter(queryString) {
      return (restaurant) => {
        return (restaurant.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
      };
    },

    handleSelect(key) {
      console.log(key);

    },

    serachByTag() {
      let _this = this;
      _this.$axios(
          {
            method: 'get',
            url: 'http://localhost:6306/Picture/searchByTag?tag=' + _this.state1,
            headers: {
              'Access-Control-Allow-Origin': '*',
              'Authorization': 'Bearer ' + sessionStorage.getItem("token")
            },
          }).then(function (response) {
        let Temp = response.data;
        console.log(Temp);
        if (Temp.success) {
          let tempTagpicture = [];
          for (let i = 0; i < Temp.picList.length; i++) {
            console.log(Temp.picList[i]);
            tempTagpicture.push({
              picId: Temp.picList[i].picId,
              picUrl: Temp.picList[i].picUrl,
              publisherId: Temp.picList[i].publisherId,
              publisherName: Temp.picList[i].publisherName,
              info: Temp.picList[i].info,
              likeNum: Temp.picList[i].likeNum,
              likeicon: "el-icon-check",
              starNum: Temp.picList[i].starNum,
              favoriteicon: "el-icon-star-on",
              commNum: Temp.picList[i].commNum
            });
          }
          _this.srcList = tempTagpicture;


        } else {
          _this.$message.error('按标签查询失败');
          console.log('error 按标签查询失败!!', response);
          return false;
        }

      }).catch(function (response) {
        _this.$message.error('获取后端接口失败');
        console.log('error 获取后端接口失败!!', response);
        return false;
      });


    }

  },

  mounted() {
    let _this = this;
    _this.$axios(
        {
          method: 'get',
          url: 'http://localhost:6306/Picture/getAllTags',
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function (response) {
      let Temp = response.data.taglists;
      let TempNaoTan = [];
      for (let i = 0; i < Temp.length; i++) {
        if(Temp[i].tagName=="avatar")continue;
        TempNaoTan.push({
          "value": Temp[i].tagName
        });
      }

      _this.restaurants = TempNaoTan;


      // console.log(_this.userInfo.birthday);
    }).catch(function (response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!', response);
      return false;
    });


    _this.$axios(
        {
          method: 'get',
          url: 'http://localhost:6306/Picture/get3Pic?requestTimes=' + _this.thistime,
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function (response) {
      let Temp = response.data;
      console.log(Temp);
      _this.srcList = Temp.returnList;

      // console.log(_this.userInfo.birthday);
    }).catch(function (response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!', response);
      return false;
    });

  }
}
</script>

<style scoped>

.homepage {
  overflow: scroll;
  height: 680px;
}

/*滚动条里面轨道*/
.homepage::-webkit-scrollbar-track {
  -webkit-box-shadow: inset 0 0 6px rgba(255, 255, 255, 0);
  background-color: rgb(27, 27, 92);
}

/*定义滚动条整体的样式*/
.homepage::-webkit-scrollbar {
  width: 6px;
  background-color: rgb(20, 19, 19);
}

/*滚动条的样式*/
.homepage::-webkit-scrollbar-thumb {
  height: 20px;
  background-image: -webkit-gradient(linear,
  left bottom,
  left top,
  color-stop(0.2, rgb(125, 126, 128)),
  color-stop(0.4, rgb(97, 98, 99)),
  color-stop(0.8, rgb(125, 126, 128)));
}


.home-wrap {
  height: 864px;
  display: block;

  width: 100%;

  background-image: url('../assets/img/Tian.jpg');

  /*padding-top: 0;*/
  /*padding-left: 0;*/
  /*position:fixed;*/
  /*min-width:1000px;*/
  /*z-index:-10;*/
  /*zoom:1;*/
  /*background-color:#fff;*/
  /*background-repeat:no-repeat;*/
  background-size: contain;
  /*-webkit-background-size:cover;*/
  /*-o-background-size:cover;*/
  /*background-position:center;*/
}

#search {
  position: relative;
  padding-top: 40px;
  padding-left: 500px;
}

.imageList {
  display: flex;
  flex-wrap: wrap;
  /*height: 500px;*/
  width: 100%;
  overflow-y: auto;
  align-items: safe center;
  justify-content: safe center;
  padding-top: 50px;
  margin: auto;
  /*background: #00a854;*/
}

.SingleImage {
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


.bottom {
  margin-top: 13px;
  line-height: 12px;
}

.button {
  padding: 0;
  float: right;
}

.image {
  width: 100%;
  min-height: 300px;
  display: block;
}

.item {
  margin-top: 10px;
  margin-right: 15px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}

.clearfix:after {
  clear: both
}


</style>