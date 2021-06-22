<template>
  <div class="homepage">
    <vue-particles
        color="#409EFF"
        :particleOpacity="0.7"
        :particlesNumber="80"
        shapeType="circle"
        :particleSize="4"
        linesColor="#409EFF"
        :linesWidth="1"
        :lineLinked="true"
        :lineOpacity="0.4"
        :linesDistance="150"
        :moveSpeed="3"
        :hoverEffect="true"
        hoverMode="grab"
        :clickEffect="true"
        clickMode="push"
    >   </vue-particles>

    <el-row style="padding-top: 20px;height: 600px;">

      <el-col :span="12" :offset="1" style="display: table-cell; /*主要是这个属性*/
                vertical-align: middle;
                text-align: center;
                height: 600px">
        <el-image :src="pictureInfo.picUrl" style="border:3px solid #2c538e;"></el-image>
      </el-col>

      <el-col :span="8" style="height: 600px" :offset="2">
        <el-form :model="pictureInfo" label-width="100px" class="ms-content">

          <div style="display: flex;align-items:center;height: 100px">

            <div style="height: 100px;padding-top:20px;
            /*background: #2d8cf0;*/
" >
              <el-form-item prop="avatar" style="">
                <el-avatar :src="pictureInfo.avatar" :size="ImgSize" shape="square" alt="作者暂无头像">
                </el-avatar>
              </el-form-item>
            </div>
<!--          <div>-->
<!--            {{pictureInfo.uploadName}}-->
<!--          </el-col>-->
            <div style="padding-left: 100px; padding-top:60px; height: 100px;
            /*background: #64bf2b*/
">
              <el-button type="primary"
                         style="position: relative;
                         width: 80px;
                         height: 48px;
                         font-size: 15px;
                          " >关注</el-button>
            </div>
          </div>

          <el-divider></el-divider>

          <div style="display: flex;padding-top: 30px">
            <div>
              <el-form-item prop="numberLike" >
                <el-badge :value=pictureInfo.numberLike class="item">
                  <el-button type="primary"  style="width: 100px;height: 60px;font-weight:bolder;font-size: 18px"
                             @click="changeIcon1($event,i)">
                    <i :class="likeicon" style="padding-right: 5px"></i>
                    点赞</el-button>
                </el-badge>
              </el-form-item>
            </div>

            <div>
              <el-form-item prop="numberFavorite" >

                <el-badge :value=pictureInfo.numberFavorite class="item">
                  <el-button type="primary"  style="width: 100px;height: 60px;font-weight:bolder;font-size: 18px"
                             @click="changeIcon2($event,i)">
                    <i :class="favoriteicon" style="padding-right: 5px"></i>
                    收藏</el-button>
                </el-badge>
              </el-form-item>
            </div>
          </div>

          <el-form-item prop="pirce" style="padding-top: 60px;padding-left: 20px">
            <el-button type="warning"
                       style="width: 250px;height: 60px;font-weight:bolder;font-size: 22px"
                       :disabled="pictureInfo.hasDownload"
            @click="Download">
              购买&下载：{{pictureInfo.pirce}}<i class="el-icon-download" style="padding-left: 15px"></i>
            </el-button>
          </el-form-item>


        </el-form>
      </el-col>

    </el-row>

    <el-row style="padding-top: 20px;height: 500px;">

      <el-col :span="12" :offset="1">
        <el-form :model="pictureInfo" ref="comment" label-width="0" class="ms-content" style="height: 380px">

          <div style="padding-left: 20px;padding-top: 20px;font-size: 20px;font-weight:700">评论区域</div>

          <el-form-item prop="myComment" style="margin-left: 20px;padding-top: 20px;">
            <el-input v-model="pictureInfo.myComment"
                      :rows="10"
                      type="textarea"
                      maxlength="150"
                      show-word-limit
                      placeholder="写点什么吧">
            </el-input>
          </el-form-item>

          <div style="margin-left: 20px;">
            <el-button type="primary" icon="el-icon_check" @click="writeComment">提交</el-button>
          </div>

        </el-form>
      </el-col>

      <el-col :span="8" :offset="2" style="height: 380px">

          <el-table
              :data="reversedMessage"
              stripe
              border
              style="width: 100%;font-size: 18px;font-weight: 600;border:1px solid #000;">
            <el-table-column
                prop="name"
                label="属性"
                width="180">
            </el-table-column>
            <el-table-column
                prop="value"
                label="值"
                width="300">
            </el-table-column>
          </el-table>

      </el-col>

    </el-row>

    <el-row style="padding-top: 20px;">

      <el-col :span="12" :offset="1">
        <el-form :model="pictureInfo" label-width="100px"
                 style="border:3px solid purple;border-radius: 25px">

          <div style="padding-left: 20px;padding-top: 20px;font-size: 20px;font-weight:700">图片评论</div>

          <div class="commList">

            <div class="commitem" v-for="(item,i) in pictureInfo.comments" :key="i" >
              <el-card style="width: 600px;height: 200px;background: #ece9cc;"
                       :body-style="{padding:'0px' }"
                       shadow="hover">
                <div class="textContent">{{item.content}}</div>
                <el-row style="position: absolute;bottom: 5px;margin-left: 0;width: 600px">
                  <el-col :span="3" :offset="2">
<!--                    <div style="font-weight:700">{{item.commName}}</div>-->
                  </el-col>
                  <el-col :span="4" :offset="15"><div style="font-style: italic">{{item.userName}}</div></el-col>
                </el-row>
              </el-card>
            </div>

          </div>

        </el-form>
      </el-col>

    </el-row>

  </div>
</template>

<script>
export default {
  // position: relative;
  name: "pictureShow",
  data(){
    return{
      dialogVisible:false,
      ImgSize:100,
      tempId:"",
      likeicon:"el-icon-minus",
      favoriteicon:"el-icon-star-off",
      pictureInfo:{
        picUrl:require("../assets/img/xiu.jpg"),
        publisherId:"0",

        publisherFollow:false,
        picLike:false,
        picStar:false,

        numberLike:0,
        numberFavorite:0,

        pixel:{
          picHeight:100,
          picWidth:100,
        },
        picInfo:"",

        picTags:[],
        hasDownload:false,
        Downloads:0,

        uploadtime:"2020-11-12",
        uploadName:"",
        pirce:0,
        avatar:require("../assets/logo.png"),
        comments:[
          {
            content:"hello world",
                userName:"Li Hua",
              userId:"0"
          },

        ],

        myComment:"",
      },

      rules:{
        userName:[{ required: true, message: '请输入用户名', trigger: 'blur' }],
        avatar:[{ required: true, message: '头像不能为空', trigger: 'blur' }],
        birthday:[{ required: true, message: '请输入生日', trigger: 'blur' }],
        phoneNumber:[{ required: true, message: '请输入电话号码', trigger: 'blur' }],
        message:[{ required: true, message: '请填写简介', trigger: 'blur' }],
        email:[{ required: true, message: '请填写邮箱', trigger: 'blur' }],
      },

    }
  },

  computed:{
    reversedMessage(){
  let showtable=[];
  showtable.push({ name:"图片简介",value:this.pictureInfo.picInfo});
  showtable.push({ name:"图片类型",value:".JPG"});
  showtable.push({ name:"图片尺寸",value:"高"+this.pictureInfo.pixel.picHeight+"*宽"+this.pictureInfo.pixel.picHeight});
  let temptagsContact='';
      for(let i=0;i<this.pictureInfo.picTags.length;i++)
      {
        temptagsContact=temptagsContact+this.pictureInfo.picTags[i]+";";
      }
  showtable.push({ name:"标签",value:temptagsContact});
  showtable.push({ name:"上传时间",value:this.pictureInfo.uploadtime});
  showtable.push({ name:"下载量",value:this.pictureInfo.Downloads});

  return showtable;
    }
  },

  methods:{
    changeIcon1(event,key){
      if(this.likeicon==="el-icon-minus")
      {
        this.likeicon="el-icon-plus";
      }
      else{
        this.likeicon="el-icon-minus";
      }

    },

    changeIcon2(event,key){
      if(this.favoriteicon==="el-icon-star-off")
      {
        this.favoriteicon="el-icon-star-on";
      }
      else{
        this.favoriteicon="el-icon-star-off";
      }
    },

    Download(){
      let _this=this;
      if(!_this.pictureInfo.hasDownload)
      {

        _this.$axios(
            {
              method:'get',
              url:'http://localhost:6306/Picture/Download?userId='
                  +sessionStorage.getItem("userId")+'&picId='+_this.tempId,
              headers: {
                'Access-Control-Allow-Origin': '*',
                'Authorization': 'Bearer ' + sessionStorage.getItem("token")
              },
            }).then(function(response) {
          let Temp=response.data;

          console.log(Temp);
          if(Temp.success)
          {
            _this.$message({
              message: '图片已经下载',
              duration:1000,
              type: 'success'
            });
            window.location.href = 'http://localhost:6306/favicon.ico';
            _this.pictureInfo.hasDownload=true;
          }
          else{
            if(Temp.msg==="Lack of Coin")
            {
              _this.$message({
                message: '金币不足请充值',
                duration:1000,
                type: 'error'
              });

            }
            else{
              _this.$message.error('图片下载失败');
            }

            return false;
          }

        }).catch(function(response) {
          _this.$message.error('获取后端接口失败');
          console.log('error 获取后端接口失败!!',response);
          return false;
        });


      }

    },



    writeComment(){
      let _this=this;



      _this.$refs.comment.validate(valid => {
        if (valid) {
          console.log("写评论",this.pictureInfo.myComment);

          let loginData = JSON.stringify({
            "userId": sessionStorage.getItem("userId"),
            "picId":this.tempId,
            "content":_this.pictureInfo.myComment,
          });


          _this.$axios({
            method:'post',
            url:'http://localhost:6306/Picture/comment',
            headers: {
              'Content-Type': 'application/json',
              'Access-Control-Allow-Origin': '*',
            },
            data:loginData
          }).then(function(response) {
            console.log("成功",response.data)
            let Temp=response.data;

            if(Temp.success)
            {
              _this.$message({
                message: '已添加评论',
                duration:1000,
                type: 'success'
              });

            }
            else{
              _this.$message.error('添加博客失败');
              console.log('error 添加博客失败!!');
              return false;
            }

          }).catch(function(response) {
            _this.$message.error('获取后端接口失败');
            console.log('error 获取后端接口失败!!',response);
            return false;
          })

        }
        else {
          _this.$message.error('请输入格式是否正确');
          console.log('error submit!!');
          return false;
        }
      });
    },
  },

  mounted() {
    let url = window.location.href; /* 获取完整URL */
    let _this=this;
    _this.tempId=url.split('=')[1];

    _this.$axios(
        {
          method:'get',
          url:'http://localhost:6306/Picture/getPicViewInfo?userId='
              +sessionStorage.getItem("userId")+'&picId='+_this.tempId,
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function(response) {
      let Temp=response.data;
      console.log(Temp);
      _this.pictureInfo.picUrl=Temp.picUrl;

      _this.pictureInfo.uploadName=Temp.uploadName;
      _this.pictureInfo.picStar=Temp.picStar;
      _this.pictureInfo.picLike=Temp.picLike;
      _this.pictureInfo.hasDownload=Temp.hasDownload;

      _this.pictureInfo.picInfo=Temp.picInfo;
      _this.pictureInfo.pixel.picWidth=Temp.picWidth;
      _this.pictureInfo.pixel.picHeight=Temp.picHeight;
      let temptags=[];
      for(let i=0;i<Temp.picTags.length;i++)
      {
        temptags.push(Temp.picTags[i].tagName);
      }
      _this.pictureInfo.picTags=temptags;
      _this.pictureInfo.Downloads=Temp.downloads;
      _this.pictureInfo.uploadtime=Temp.uploadtime;

      console.log(_this.pictureInfo)

    }).catch(function(response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!',response);
      return false;
    });

    _this.$axios(
        {
          method:'get',
          url:'http://localhost:6306/Picture/getAllComment?picId='+_this.tempId,
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function(response) {
      let Temp=response.data;
      console.log(Temp);
      _this.pictureInfo.comments=Temp.comments;
    }).catch(function(response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!',response);
      return false;
    });

  },
}
</script>

<style scoped>

.homepage{
  overflow: scroll;
  height: 675px;
  background: transparent;
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

#particles-js{
  width: 100%;
  height: calc(100% - 100px);
  position: absolute;
}

.ms-content{

  border:3px dashed #221f8e;
  border-radius:25px;
  height: 550px;
}

.item{
  margin-top: 10px;
  margin-right: 15px;
}

.pictureTable{
  border:1px solid #000;
}

.commList{
  display: flex;
  flex-direction:column;
  /*height: 500px;*/
  width: 100%;
  /*overflow-y: auto;*/
  align-items: safe center;
  justify-content: safe center;
  /*padding-top: 50px;*/
  margin: auto;
  min-height: 300px;
}

.commitem{
  display: block;
  position: relative;
  width: 630px;
  height: 200px;
  padding-top: 20px;
  margin: auto;

  /*background: #999999;*/
  /*margin-left: 40px;*/
  /*margin-top: 40px;*/
  /*height: 200px;*/
}

.textContent{
  padding-left: 15px;
  padding-top: 20px;
}


</style>