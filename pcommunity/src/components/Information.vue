<template>
  <el-form :model="userInfo" :rules="rules" ref="login" label-width="100px" class="ms-content">

    <el-form-item prop="userName" label="用户名">
      <el-input v-model="userInfo.userName" style="width: 20%">
      </el-input>
    </el-form-item>

    <el-row :gutter="0">

      <el-col :span=3>
        <el-badge :value="userInfo.staticNumber.CommentNum">
          <el-form-item prop="staticNumber.CommentNum" label="评论数">
            <i class="el-icon-s-comment"></i>
          </el-form-item>
        </el-badge>
      </el-col>

      <el-col :span=3>
        <el-badge :value="userInfo.staticNumber.FollowNum">
          <el-form-item prop="staticNumber.FollowNum" label="粉丝数">
            <i class="el-icon-s-custom"></i>
          </el-form-item>
        </el-badge>
      </el-col>

      <el-col :span=3>
        <el-badge :value="userInfo.staticNumber.LikeNum">
        <el-form-item prop="staticNumber.LikeNum" label="点赞数">
          <i class="el-icon-check"></i>

        </el-form-item>
        </el-badge>

      </el-col>

      <el-col :span=3>
        <el-badge :value="userInfo.staticNumber.StarNum">
        <el-form-item prop="staticNumber.StarNum" label="收藏数">
          <i class="el-icon-star-on"></i>

        </el-form-item>
        </el-badge>

      </el-col>

    </el-row>


    <el-form-item prop="avatar" label="个人头像">

      <el-upload
          class="avatar-uploader"
          action=""
          :show-file-list="false"
          :on-success="handleAvatarSuccess"
          :limit="1"
          :on-change="getAvatar"
          :auto-upload="false"
          :before-upload="beforeAvatarUpload">
        <img v-if="userInfo.avatar" :src="userInfo.avatar" class="avatar">
        <i v-else class="el-icon-plus avatar-uploader-icon"></i>
      </el-upload>
      <el-button type="primary" @click="postAvatar">上传</el-button>

      <!--        <el-image :src="userInfo.avatar"></el-image>-->

    </el-form-item>

    <el-form-item prop="birthday" label="生日">
      <el-date-picker
          v-model="userInfo.birthday"
          type="date"
          placeholder="选择日期">
      </el-date-picker>

    </el-form-item>

    <el-form-item prop="phoneNumber" label="联系电话">
      <el-input v-model="userInfo.phoneNumber" style="width: 60%" >
      </el-input>
    </el-form-item>

    <el-form-item prop="message" label="个人简介">
      <el-input v-model="userInfo.message"
                style="width: 60%"
                :rows="8"
                type="textarea"
                maxlength="150"
                show-word-limit
                placeholder="写点什么吧">
      </el-input>
    </el-form-item>

    <el-form-item prop="email" label="联系邮箱" style="width: 60%">
      <el-input v-model="userInfo.email">
      </el-input>
    </el-form-item>


    <div class="login-btn">
      <el-button type="primary" @click="modifyInfo()">保存修改</el-button>
    </div>

  </el-form>


</template>

<script>
export default {
  name: "Information",

  data(){
    return{

      Image:'',
      userInfo:{
        userName:"",
        // avatar:"https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
        avatar:"",

        birthday:"",
        phoneNumber:"",
        message:"",
        email:"",
        staticNumber:{
          StarNum:0,
          LikeNum:0,
          CommentNum:0,
          FollowNum:0
        }
      },

      rules:{
        userName:[{ required: true, message: '请输入用户名', trigger: 'blur' }],
        avatar:[{ required: true, message: '头像不能为空', trigger: 'blur' }],
        birthday:[{ required: true, message: '请输入生日', trigger: 'blur' }],
        phoneNumber:[{ required: true, message: '请输入电话号码', trigger: 'blur' }],
        message:[{ required: true, message: '请填写简介', trigger: 'blur' }],
        email:[{ required: true, message: '请填写邮箱', trigger: 'blur' }],
      }
    }
  },

  methods:{

    modifyInfo(){
      let _this=this;

      _this.$refs.login.validate(valid => {
        if (valid) {
          let pictureData = JSON.stringify({
            "UserName":_this.userInfo.userName,
            "UserId":sessionStorage.getItem("userId"),
            "Name":_this.userInfo.userName,
            "Birthday":_this.userInfo.birthday,
            "Email":_this.userInfo.email,
            "Message":_this.userInfo.message,
            "PhoneNumber":_this.userInfo.phoneNumber,
          });



          _this.$axios({
            method:'post',
            url:'http://localhost:6306/Account/saveUserInfo',
            headers: {
              'Content-Type': 'application/json',
              'Access-Control-Allow-Origin': '*',
            },
            data:pictureData
          }).then(function(response) {
            console.log("成功",response.data)
            let Temp=response.data;

            if(Temp.success) {
              _this.$message({
                message: '修改成功',
                duration: 1000,
                type: 'success'
              });
            }
            //   sessionStorage.setItem('userName', Temp.userName);
            //   sessionStorage.setItem('userId', Temp.userId);
            //   sessionStorage.setItem('token', Temp.token);
            //
            //   _this.$router.push('/Home');
            // }
            // else{
            //   _this.$message.error('账户密码不匹配');
            //   console.log('error 账户密码不匹配!!');
            //   return false;
            // }

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

    handleAvatarSuccess(res, file) {
      this.userInfo.avatar = URL.createObjectURL(file.raw);
    },


    beforeAvatarUpload(file) {
      const isJPG = file.type === 'image/jpeg';
      const isLt2M = file.size / 2048 / 2048 < 2;

      if (!isJPG) {
        this.$message.error('上传头像图片只能是 JPG 格式!');
      }
      if (!isLt2M) {
        this.$message.error('上传头像图片大小不能超过 8MB!');
      }
      return isJPG && isLt2M;
    },

    getAvatar(file){
      let _this=this;
      console.log(file);
      let selectedFile=file;
      let name = selectedFile.name; //选中文件的文件名
      let size = selectedFile.size; //选中文件的大小
      console.log("文件名:" + name + "大小:" + size);
      _this.Image=file.raw;
    },


    postAvatar(){
      let _this=this;
      let pictureData=new FormData();
      pictureData.append('', _this.Image)
      pictureData.append('userId', sessionStorage.getItem("userId"))
        pictureData.append('tag', 'avatar');
      pictureData.append('Pinfo', 'null')
      pictureData.append('Price', 0)

      _this.$axios({
        method:'post',
        url:'http://localhost:6306/Account/Upload',
        headers: {
          'Content-Type':'application/x-www-form-urlencoded',
          'Access-Control-Allow-Origin': '*',
        },
        data:pictureData
      }).then(function(response) {
        console.log("成功",response.data)
        _this.$message.success('图片上传成功');
        // _this.dialogVisible = false;

        let Temp=response.data;
        _this.userInfo.avatar=Temp.pictureURL;
        console.log(_this.userInfo.avatar);

      }).catch(function(response) {
        _this.$message.error('获取后端接口失败');
        console.log('error 获取后端接口失败!!',response);
        return false;
      })
    }

  },

  mounted:function() {

    let _this=this;

    let tempId=sessionStorage.getItem("userId");

    _this.$axios(
        {
      method:'get',
      url:'http://localhost:6306/Account/getUserInfo?userId='+tempId,
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Authorization': 'Bearer ' + sessionStorage.getItem("token")
      },
    }).then(function(response) {
      let Temp=response.data;

      _this.userInfo.userName=Temp.userName;
      _this.userInfo.email=Temp.email;
      _this.userInfo.message=Temp.message;
      _this.userInfo.phoneNumber=Temp.phoneNumber;
      _this.userInfo.birthday=Temp.birthday.slice(0,10);
      _this.userInfo.userName=Temp.userName;
      _this.userInfo.avatar=Temp.userAvatar;

      // console.log(_this.userInfo.birthday);
    }).catch(function(response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!',response);
      return false;
    });

    _this.$axios(
        {
          method:'get',
          url:'http://localhost:6306/Account/getProfileInfo?userId='+tempId,
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function(response) {
      let Temp=response.data;
      console.log(Temp);
      _this.userInfo.staticNumber.StarNum=parseInt(Temp.starNum);
      _this.userInfo.staticNumber.LikeNum=parseInt(Temp.likeNum);
      _this.userInfo.staticNumber.CommentNum=parseInt(Temp.commentNum);
      _this.userInfo.staticNumber.FollowNum=parseInt(Temp.followNum);
      console.log(_this.userInfo.staticNumber);

    }).catch(function(response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!',response);
      return false;
    });


    }
}
</script>

<style>
#app /deep/ .el-upload{
  width: 180px;
}

#app /deep/ .el-upload--text{
  width: 180px;
}
</style>

<style scoped>
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  width:180px;
}
.avatar-uploader .el-upload:hover {
  border-color: #409EFF;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>