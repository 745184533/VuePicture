<template>
  <div class="homepage">

    <el-row style="padding-top: 30px">
      <el-col :span="3" :offset="6">
        <el-button icon="el-icon-edit" @click="dialogVisible = true"
                   style="width: 100px"
        >写博客</el-button>
      </el-col>
      <el-col :span="3" :offset="6">
        <el-button icon="el-icon-refresh" @click="getTenBlog"
                   style="width: 100px"
        >换一批</el-button>
      </el-col>
    </el-row>

    <div class="BlogList">

      <div class="blogitem" v-for="(item,i) in Blog" :key="i" >
        <el-card style="width: 600px;height: 200px;background: #ece9cc;"
                 :body-style="{padding:'0px' }"
                 shadow="hover">
          <div class="textContent">{{item.content}}</div>
          <el-row style="position: absolute;bottom: 5px;margin-left: 0;width: 600px">
            <el-col :span="3" :offset="2"><div style="font-weight:700">{{item.userName}}</div></el-col>
            <el-col :span="6" :offset="13"><div style="font-style: italic;width: 150px">{{item.blogDate}}</div></el-col>
          </el-row>
        </el-card>
      </div>

    </div>

    <el-dialog
        title="博客随想"
        :visible.sync="dialogVisible"
        width="50%"
        :before-close="handleClose">

      <el-form :model="mycontent" :rules="rules1" ref="myblog">
        <el-form-item prop="contents">
          <el-input v-model="mycontent.contents"
                    :rows="10"
                    type="textarea"
                    maxlength="150"
                    show-word-limit
                    placeholder="写点什么吧"
                  ></el-input>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="writeBlog">确认提交</el-button>
        </el-form-item>

      </el-form>

    </el-dialog>


  </div>


</template>

<script>
export default {
  name: "Blog",
  data(){
    let validatePass1 = (rule, value, callback) => {

      console.log(value);
      let valadite=["操","艹","滚","傻逼"];

      if (value.length === 0) {
        callback(new Error('博客内容不能为空'));

      } else if(value.length >= 150)
      {
        callback(new Error('字数不超过150字'));
      }
      else {
        for(let j=0;j<value.length;j++)
        {
          if(value.indexOf(valadite[j]) !==-1)
          {
            callback(new Error('博客不能包含敏感信息'));
            return;
          }
        }
        callback();
      }
    }

    return{
      Blog:[
        {blogDate:"2020/11/12",
        blogId:"",
        content:"hello world",
        blogType:"",
        userId:"",
        userName:"Li Hua"},
        {blogDate:"2020/11/12",
          blogId:"",
          content:"hello world",
          blogType:"",
          userId:"",
          userName:"Li Hua"},
        {blogDate:"2020/11/12",
          blogId:"",
          content:"hello world",
          blogType:"",
          userId:"",
          userName:"Li Hua"},
        {blogDate:"2020/11/12",
          blogId:"",
          content:"hello world",
          blogType:"",
          userId:"",
          userName:"Li Hua"},
        {blogDate:"2020/11/12",
          blogId:"",
          content:"hello world",
          blogType:"",
          userId:"",
          userName:"Li Hua"},
        {blogDate:"2020/11/12",
          blogId:"",
          content:"hello world",
          blogType:"",
          userId:"",
          userName:"Li Hua"},

      ],
      mycontent:{contents:""},
      dialogVisible:false,
      rules1:{contents: [{validator:validatePass1,trigger: 'blur'}],},
      tempTimes:"0",
    }

  },
  methods:{
    getTenBlog(){
      let _this=this;

      _this.$axios({
            method:'get',
            url:'http://localhost:6306/Blog/getTen?times='+_this.tempTimes,
            headers: {
              'Access-Control-Allow-Origin': '*',
            },
          }).then(function(response) {
            let Temp=response.data.list;
            console.log("计数",_this.tempTimes);
            if(Temp.length===0)
            {
              _this.tempTimes=0;
              //重新开始计数
              return;
            }
            _this.tempTimes++;
            _this.Blog=Temp;

          }).catch(function(response) {
            _this.$message.error('获取后端接口失败');
            console.log('error 获取后端接口失败!!',response);
            return false;
          })




    },

    writeBlog(){

      let _this=this;

      _this.$refs.myblog.validate(valid => {
        if (valid) {
          console.log("写博客",this.mycontent.contents);
          this.dialogVisible = false;
          let loginData = JSON.stringify({
            "userId": sessionStorage.getItem("userId"),
            "content":_this.mycontent.contents,
          });


          _this.$axios({
            method:'post',
            url:'http://localhost:6306/Blog/writeBlog',
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
                message: '已添加博客',
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

    handleClose(done) {
      this.$confirm('确认关闭？')
          .then(_ => {
            done();
          })
          .catch(_ => {});
    },

  },
  mounted() {
    let _this=this;
    _this.$axios({
      method:'get',
      url:'http://localhost:6306/Blog/getTen?times='+_this.tempTimes,
      headers: {
        'Access-Control-Allow-Origin': '*',
      },
    }).then(function(response) {
      console.log("成功",response.data)
      let Temp=response.data;
      _this.Blog=Temp.list;
      console.log(_this.Blog);
      _this.tempTimes++;

    }).catch(function(response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!',response);
      return false;
    })
  }
}
</script>

<style scoped>
.homepage{
  overflow: scroll;
  height: 680px;
  background: url("../assets/img/blog4.jpg");
  background-size:cover;

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

.BlogList{
  display: flex;
  flex-direction:column;
  min-height: 400px;
  width: 100%;
  /*overflow-y: auto;*/
  align-items: safe center;
  justify-content: safe center;
  /*padding-top: 50px;*/
  margin: auto;
}

.blogitem{
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