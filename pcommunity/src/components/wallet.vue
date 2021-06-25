<template>
  <div class="walletHome">
    <el-row :gutter="100">
      <el-col :span=1 style="background: #00a854;background: transparent">
        <el-avatar :src="myWallet.avatar" :size="ImgSize" shape="square">
        </el-avatar>
      </el-col>
      <el-col :span=2 style="background: #233471;background: transparent">
        {{myWallet.userName}}
      </el-col>
    </el-row>

    <el-divider content-position="center"/>


    <el-form :model="myWallet" :rules="rules" ref="login" label-width="140px" class="ms-content" style="padding-top: 10px">

      <el-form-item prop="coin" label="当前账户余额：">
        {{myWallet.coin}}币
      </el-form-item>

      <el-form-item prop="buyNum" label="购买图片总数：">
        {{myWallet.buyNum}}张
      </el-form-item>

      <el-form-item prop="Add" label="请输入充值金额：">
        <el-input v-model="myWallet.Add" style="width: 30%">

        </el-input>
      </el-form-item>

      <div class="login-btn">
        <el-button type="primary" @click="depositeCoin()">确定充值</el-button>
      </div>

    </el-form>

    <el-divider content-position="center"/>
    <el-tabs type="border-card" style="background-color:transparent;">

    <el-tab-pane >
      <div slot="label" class="LoginTitle"><i class="el-icon-s-promotion"></i>充值记录</div>
      <div>充值总次数：{{deposite.length}}</div>
      <div class="coinItem" v-for="(item,i) in deposite" :key="i" >
        <i>{{item.payTime}}</i>  充值{{item.coin}}币
      </div>
    </el-tab-pane>

    <el-tab-pane>
      <div slot="label" class="LoginTitle"><i class="el-icon-folder-add"></i>消费记录</div>
      <div>消费总次数：{{payment.length}}</div>
      <div class="coinItem" v-for="(item,i) in payment" :key="i" >
        <i>{{item.payTime}}</i>  消费{{item.coin}}币
      </div>
    </el-tab-pane>
    </el-tabs>

  </div>

</template>

<script>
export default {
  name: "wallet",

  data(){

    return{
      ImgSize:50,

      myWallet:{
        userName:sessionStorage.getItem("userName"),
        userId:sessionStorage.getItem("userId"),
        avatar:"https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
        coin:0,
        publishNum:0,
        buyNum:0,
        Add:0,
      },

      rules: {
        username: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
        avatar: [
          { required: true, message: '请输入密码', trigger: 'blur' },
          { min:3,  max:15, message: '密码长度不合适', trigger: 'blur'}]
      },

      payment:[
          {payId:0, payTime:"2020-11-12", coin:0,},
      ],

      deposite:[
        {payId:0, payTime:"2020-11-12", coin:0,},
      ],

    }
  },

  methods:{
    depositeCoin(){
      let _this=this;

      _this.$refs.login.validate(valid => {
        if (valid) {

          let tempUrl="?userId="+sessionStorage.getItem("userId")+"&amount="+_this.myWallet.Add;
          _this.$axios({
            method:'post',
            url:'http://localhost:6306/Wallet/depositWallet'+tempUrl,
            headers: {
              'Access-Control-Allow-Origin': '*',
            },
          }).then(function(response) {
            console.log("成功",response.data)
            let Temp=response.data;

            if(Temp.success)
            {
              _this.$message({
                message: '充值成功',
                duration:1000,
                type: 'success'
              });

              _this.$router.push('/wallet');
            }
            else{
              _this.$message.error('充值失败');
              console.log('error 充值失败!!');
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

  mounted:function (){
      let _this=this;

    let tempId=sessionStorage.getItem("userId");

    _this.$axios(
        {
          method:'get',
          url:'http://localhost:6306/Wallet/getWalletInfo?userId='+tempId,
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function(response) {
      let Temp=response.data;
      // console.log(Temp);
      _this.myWallet.coin=Temp.nowCoin;
      _this.myWallet.buyNum=Temp.buyNum;
      _this.myWallet.avatar=Temp.userAvatar;
      // console.log(_this.myWallet);

    }).catch(function(response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!',response);
      return false;
    });

    _this.$axios(
        {
          method:'get',
          url:'http://localhost:6306/Wallet/getDepositRecord?userId='+tempId,
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function(response) {
      let Temp=response.data;
      // console.log(Temp);
      _this.deposite=Temp.list;
      // console.log(_this.deposite);

    }).catch(function(response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!',response);
      return false;
    });


    _this.$axios(
        {
          method:'get',
          url:'http://localhost:6306/Wallet/getConsumeRecord?userId='+tempId,
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Authorization': 'Bearer ' + sessionStorage.getItem("token")
          },
        }).then(function(response) {
      let Temp=response.data;
      // console.log(Temp);
      _this.payment=Temp.list;
      // console.log(_this.deposite);

    }).catch(function(response) {
      _this.$message.error('获取后端接口失败');
      console.log('error 获取后端接口失败!!',response);
      return false;
    });




    }

}

</script>

<style scoped>
.login-btn{
  padding-left: 30px;
}
.walletHome{
  overflow-x: hidden;
}
.LoginTitle{
  width: 553px;
}
.coinItem{
  padding-left: 5px;
  padding-top: 5px;
}

</style>