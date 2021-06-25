<template>

  <el-container>

    <el-header style="padding: 0;">
      <el-menu
          :default-active="activeIndex"
          class="el-menu-demo"
          mode="horizontal"
          background-color="#545c64"
          text-color="#fff"
          active-text-color="#ffd04b"
          :router="true"
          style="width: 1536px;justify-content:flex-end;display: flex"
      >
        <el-menu-item index="/Home">首页</el-menu-item>

        <el-submenu index="2">
          <template slot="title">个人中心</template>
          <el-menu-item index="/login">登陆</el-menu-item>
          <el-menu-item index="/login">注册</el-menu-item>
          <el-menu-item index="/personal">个人中心</el-menu-item>
        </el-submenu>

        <el-menu-item @click="dialogVisible = true">上传图片</el-menu-item>

        <el-menu-item index="/Blog">博客</el-menu-item>
        <el-menu-item index="/about">关于我们</el-menu-item>

      </el-menu>
    </el-header>


    <el-dialog
        title="上传图片"
        :visible.sync="dialogVisible"
        width="40%"
        :before-close="handleClose">
      <el-form :model="picture" :rules="rulesPic" ref="uploadImg">


        <el-form-item label="上传图片" prop="Image">
          <el-upload
              class="upload-demo"
              ref="upload"
              action="saveImg"
              :on-change="saveImg"
              accept=".jpg,.jpeg,.png,.gif,.bmp,.pdf,.JPG,.JPEG,.PBG,.GIF,.BMP,.PDF"

              :on-preview="handlePreview"
              :on-remove="handleRemove"
              :file-list="picture.fileImage"
              :auto-upload="false"
              :limit="1"
              list-type="picture"
              :multiple="false"
              :drag="false"
          >
            <el-button slot="trigger" size="small" type="primary">选取文件</el-button>
            <el-button style="margin-left: 10px;" size="small" type="success" @click="submitUpload">上传到服务器</el-button>
            <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
          </el-upload>
        </el-form-item>


        <el-form-item label="标签选择" class="tag-group__title">
          <el-tag
              v-for="(some,index) in picture.tag"
              :key="index"
              :type="some"
              effect="dark"
              style="margin-left: 3px"
              closable
              @close="revokeTag(some)"
          >
            {{ some }}
          </el-tag>
          <br>
          自定义标签：
          <el-input v-model="MyTag"
                    placeholder="请输入内容"
                    style="width: 150px;margin-right: 10px"
                    maxlength="15"
                    show-word-limit
          ></el-input>
          <el-button type="primary" @click="AddTag">确定</el-button>
          <br>
          <div style="height: 100px;overflow-y: scroll;">
            <el-button
                v-for="item in Tags"
                :key="item.label"
                :type="item.type"
                effect="dark"
                style="padding-left: 3px"
                @click="selectTag(item.label)"
            >
              {{ item.label }}
            </el-button>
          </div>
        </el-form-item>


        <el-form-item label="图片简介" prop="Pinfo">
          <el-input v-model="picture.Pinfo" style="width: 80%" placeholder="说点什么吧"></el-input>
        </el-form-item>

        <el-form-item label="图片价格" prop="Price">
          <el-input v-model="picture.Price" style="width: 40%" placeholder="0"></el-input>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="submitUpload">立即上传</el-button>
        </el-form-item>

      </el-form>
    </el-dialog>

    <el-main style="padding: 0;width: 100%">
      <router-view></router-view>
    </el-main>

  </el-container>


</template>

<script>
export default {
  name: "Dash",


  data() {
    // var validatePass = (rule, value, callback) => {
    //
    //   console.log(value);
    //   if (value.length === 0) {
    //     callback(new Error('请至少选择一个标签'));
    //   } else {
    //     callback();
    //   }
    // }

    return {
      MyTag: "",
      color: ["success", "info", "danger", "warning", "primary"],
      dialogVisible: false,
      activeIndex: '/Home',
      AllWidth: document.body.clientWidth,
      param: {
        username: 'admin',
        password: '123123',
      },


      rulesPic: {

        Image: [{required: true, message: '请选择一张图片', trigger: 'blur'}],
        Pinfo: [
          {required: true, message: '必须添加简介', trigger: 'blur'},
          {max: 125, message: '字数少于125字', trigger: 'blur'}],
        Price: [{required: true, message: '请填写', trigger: 'blur'}],
      },


      picture: {
        tag: [],
        // userId:"",
        fileImage: [
          // {
          //   name: 'food.jpeg',
          //   url: 'https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/' +
          //       'thumbnail/360x360/format/webp/quality/100'
          // }
        ],
        Image: "",
        Pinfo: "",
        Price: ""
      },

      Tags: [
        {type: 'success', label: '标签一'},
        {type: 'success', label: '标签二'},
        {type: 'info', label: '标签三'},
        {type: 'danger', label: '标签四'},
        {type: 'warning', label: '标签五'}
      ],

      showTags: [],


    };
  },
  methods: {


    saveImg(file) {
      console.log(file);
      let selectedFile = file;
      let name = selectedFile.name; //选中文件的文件名
      let size = selectedFile.size; //选中文件的大小
      console.log("文件名:" + name + "大小:" + size);
      this.picture.Image = file.raw;
    },

    submitUpload() {
      let _this = this;
      let listTag = ['tag', 'tag1', 'tag2']
      _this.$refs.uploadImg.validate(valid => {
        if (_this.picture.tag.length === 0) {
          _this.$message.error('请至少选择一个标签');
          console.log('error submit!!');
          return false;
        }
        if (valid) {
          console.log(this.picture);
          console.log("!!!", this.picture.fileImage);

          let pictureData = new FormData();
          pictureData.append('', _this.picture.Image)
          pictureData.append('userId', sessionStorage.getItem("userId"))
          for (let i = 0; i < _this.picture.tag.length; i++) {
            pictureData.append(listTag[i], _this.picture.tag[i]);
          }
          pictureData.append('Pinfo', _this.picture.Pinfo)
          pictureData.append('Price', _this.picture.Price)


          _this.$axios({
            method: 'post',
            url: 'http://localhost:6306/Account/Upload',
            headers: {
              'Content-Type': 'application/x-www-form-urlencoded',
              'Access-Control-Allow-Origin': '*',
            },
            data: pictureData
          }).then(function (response) {
            console.log("成功", response.data)
            _this.$message.success('图片上传成功');
            _this.dialogVisible = false;

            // let Temp=response.data;

          }).catch(function (response) {
            _this.$message.error('获取后端接口失败');
            console.log('error 获取后端接口失败!!', response);
            return false;
          })


        } else {
          _this.$message.error('请输入格式是否正确');
          console.log('error submit!!');
          return false;
        }
      });


    },

    handleRemove(file, fileList) {
      // console.log(file, fileList);
    },

    handlePreview(file) {
      // console.log(file);
    },


    handleClose(done) {
      this.$confirm('确认关闭？')
          .then(_ => {
            done();
          })
          .catch(_ => {
          });
    },

    selectTag(key) {
      console.log(this.picture.tag);
      if (this.picture.tag.length >= 3) {

        this.$message({
          message: '最多选择三个标签',
          type: 'warning'
        });
        return;
      }
      if (this.picture.tag.indexOf(key) === -1
          || this.picture.tag.length === 0) {
        this.picture.tag.push(key)
      }

    },

    AddTag() {
      if (this.picture.tag.length >= 3) {

        this.$message({
          message: '最多选择三个标签',
          type: 'warning'
        });
        return;
      }
      if (this.picture.tag.indexOf(this.MyTag) === -1
          || this.picture.tag.length === 0) {
        this.picture.tag.push(this.MyTag)
      }
    },

    revokeTag(key) {
      console.log(key);
      let locations = this.picture.tag.indexOf(key);
      this.picture.tag.splice(locations, 1);
    },

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
      // console.log(Temp);

      let TempNaoTan = [];
      for (let i = 0; i < Temp.length; i++) {
        if(Temp[i].tagName=="avatar")continue;


        TempNaoTan.push({
          type: _this.color[i % 5],
          label: Temp[i].tagName
        });
      }
      _this.Tags = TempNaoTan;
      // console.log(_this.Tags);

    }).catch(function (response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!', response);
      return false;
    });

  }
}

</script>


<style>


#app /deep/ .el-main {
  overflow: scroll;
  height: 680px;
  background: url("../assets/img/blog4.jpg");
  background-size: cover;

}

/*滚动条里面轨道*/
#app /deep/ .el-main::-webkit-scrollbar-track {
  -webkit-box-shadow: inset 0 0 6px rgba(255, 255, 255, 0);
  background-color: rgb(27, 27, 92);
}

/*定义滚动条整体的样式*/
#app /deep/ .el-main::-webkit-scrollbar {
  width: 6px;
  background-color: rgb(20, 19, 19);
}

/*滚动条的样式*/
#app /deep/ .el-main::-webkit-scrollbar-thumb {
  height: 20px;
  background-image: -webkit-gradient(linear,
  left bottom,
  left top,
  color-stop(0.2, rgb(125, 126, 128)),
  color-stop(0.4, rgb(97, 98, 99)),
  color-stop(0.8, rgb(125, 126, 128)));
}


</style>

<style scoped>

.tag-group__title {
  padding-right: 10px;

}


</style>