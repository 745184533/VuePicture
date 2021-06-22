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
        <el-form-item prop="staticNumber.LikeNum" label="上传数">
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
        <el-image :src="userInfo.avatar"></el-image>
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

      userInfo:{
        userName:"",
        avatar:"https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
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

<style scoped>

</style>