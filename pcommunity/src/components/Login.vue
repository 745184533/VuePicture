<template>
    <div class="login-wrap">

        <div class="ms-login">

            <div class="ms-title">图片社区系统</div>

            <el-tabs type="border-card" style="background-color:transparent;">

            <el-tab-pane >
              <div slot="label" class="LoginTitle"><i class="el-icon-s-promotion"></i> 登陆</div>
              <el-form :model="param" :rules="rules" ref="login" label-width="0px" class="ms-content">

                <el-form-item prop="username">
                  <el-input v-model="param.username" placeholder="username">
                    <el-button slot="prepend" icon="el-icon-user"></el-button>
                  </el-input>

                </el-form-item>

                <el-form-item prop="password">
                  <el-input
                      type="password"
                      placeholder="password"
                      v-model="param.password"
                      @keyup.enter.native="Login()"
                      show-password
                  >
                    <el-button slot="prepend" icon="el-icon-lock"></el-button>
                  </el-input>
                </el-form-item>

                <div class="login-btn">
                  <el-button type="primary" @click="Login()">登陆</el-button>
                </div>

                <p class="login-tips">Tips : 用户名和密码随便填。</p>

              </el-form>
            </el-tab-pane>

            <el-tab-pane>
              <div slot="label" class="LoginTitle"><i class="el-icon-folder-add"></i> 注册</div>
              <el-form :model="param" :rules="rules2" ref="register" label-width="0px" class="ms-content">

                <el-form-item prop="username">
                  <el-input v-model="param.username" placeholder="username">
                    <el-button slot="prepend" icon="el-icon-user"></el-button>
                  </el-input>

                </el-form-item>

                <el-form-item prop="password">
                  <el-input
                      type="password"
                      placeholder="password"
                      v-model="param.password"
                      @keyup.enter.native="register()"
                      show-password
                  >
                    <el-button slot="prepend" icon="el-icon-lock"></el-button>
                  </el-input>
                </el-form-item>

                <el-form-item prop="rePassword">
                  <el-input
                      type="password"
                      placeholder="confirm password"
                      v-model="param.rePassword"
                      @keyup.enter.native="register()"
                      show-password
                  >
                    <el-button slot="prepend" icon="el-icon-lock"></el-button>
                  </el-input>
                </el-form-item>

                <el-form-item prop="email">
                  <el-input v-model="param.email" placeholder="email">
                    <el-button slot="prepend" icon="el-icon-s-home"></el-button>
                  </el-input>
                </el-form-item>

                <div class="login-btn">
                  <el-button type="primary" @click="register()">注册</el-button>
                </div>

                <p class="login-tips">Tips : 密码在5~15位之间。</p>

              </el-form>
            </el-tab-pane>



          </el-tabs>

        </div>


    </div>

</template>

<script>
export default {
    data: function() {
      var validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'));
        } else if (value !== this.param.password) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };

        return {
            param: {
                username: 'root',
                password: 'admin',
                rePassword:"admin",
                email:'1@qq.com'
            },


            rules: {
                username: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
                password: [
                    { required: true, message: '请输入密码', trigger: 'blur' },
                    { min:3,  max:15, message: '密码长度不合适', trigger: 'blur'}]
            },


          rules2: {
            username: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
            password: [
              { required: true, message: '请输入密码', trigger: 'blur' },
              { min:3,  max:15, message: '密码长度不合适', trigger: 'blur'}],
            rePassword: [{ validator: validatePass, trigger: 'blur' }],
            email:[
              { required: true, message: '请输入邮箱地址', trigger: 'blur' },
              { type: 'email', message: '请输入正确的邮箱地址', trigger: ['blur', 'change']}
            ]
          },
        };
    },
    methods: {

        Login() {

            let _this=this;

            _this.$refs.login.validate(valid => {
                if (valid) {
                  let loginData = JSON.stringify({
                    "username": this.param.username,
                    "userPassword":this.param.password
                  });


                  _this.$axios({
                    method:'post',
                    url:'http://localhost:6306/Account/login',
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
                            message: '登陆成功',
                            duration:1000,
                            type: 'success'
                          });
                          sessionStorage.setItem('userName', Temp.userName);
                          sessionStorage.setItem('userId', Temp.userId);
                          sessionStorage.setItem('token', Temp.token);

                          _this.$router.push('/Home');
                        }
                        else{
                          _this.$message.error('账户密码不匹配');
                          console.log('error 账户密码不匹配!!');
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

        register(){
          let _this=this;

          _this.$refs.register.validate(valid => {
            if (valid) {
              let loginData = JSON.stringify({
                "username": this.param.username,
                "email":this.param.email,
                "userPassword":this.param.password
              });


              _this.$axios({
                method:'post',
                url:'http://localhost:6306/Account/register',
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
                    message: '登陆成功',
                    duration:1000,
                    type: 'success'
                  });
                  sessionStorage.setItem('userName', Temp.userName);
                  sessionStorage.setItem('userId', Temp.userId);
                  sessionStorage.setItem('token', Temp.token);

                  _this.$router.push('/Home');
                }
                else{
                  _this.$message.error('账户已存在');
                  console.log('error 账户已存在!!');
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

        }

    },
};
</script>

<style scoped>
.login-wrap {
    position: relative;
    width: 100%;
    height: 100%;
    background-image: url('../assets/img/login-bg.jpg');
    background-size: 100%;
}
.ms-title {
    width: 100%;
    line-height: 50px;
    text-align: center;
    font-size: 20px;
    color: #9191b3;
    border-bottom: 1px solid #ddd;
}
.ms-login {
    position: absolute;
    left: 50%;
    top: 50%;
    width: 350px;
    margin: -190px 0 0 -175px;
    border-radius: 5px;
    background: rgba(255, 255, 255, 0.3);
    overflow: hidden;
}
.ms-content {
    padding: 30px 30px;
}
.login-btn {
    text-align: center;
}
.login-btn button {
    width: 100%;
    height: 36px;
    margin-bottom: 10px;
}
.login-tips {
    font-size: 12px;
    line-height: 30px;
    color: #fff;
}
.LoginTitle{
  display: block;
  width: 133px;
  background-color:transparent;
}
.el-tabs__header {
  background-color: transparent;
}


</style>