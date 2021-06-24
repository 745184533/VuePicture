using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WebApplication1.EFModels;
using WebApplication1.Services;
using WebApplication1.ViewsModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : Controller
    {
        private picturecommunityContext context;
        private WalletServices services;
        //private AccountServices vice_services;
        public WalletController(picturecommunityContext context)
        {
            this.context = context;
            this.services = new WalletServices(context);
        }

        //[Authorize]
        [Route("getWalletInfo")]
        [HttpGet]
        public IActionResult getWalletInfo(string userId)
        {
            var user = context.Users.FirstOrDefault(u => u.UId == userId);
            var wallet = context.Wallets.FirstOrDefault(u => u.UId == userId);
            return Ok(new
            {
                Success = true,
                UserName = user.UName,
                NowCoin = wallet.Coin,
                BuyNum = wallet.BuyNum,
                msg = "Operation Done"
            });
        }


        ////[Authorize]
        [Route("depositWallet")]
        [HttpPost]
        public IActionResult depositWallet(string userId, int amount)
        {
            var nowWallet = context.Wallets.FirstOrDefault(u => u.UId == userId);
            context.Entry(nowWallet).CurrentValues.SetValues(new Wallet
            {
                UId = nowWallet.UId,
                Coin = nowWallet.Coin + amount,
                PublishNum = nowWallet.PublishNum,
                BuyNum = nowWallet.BuyNum + 1
            });
            context.SaveChanges();
            //
            var nowPayment = new Payment
            {
                PayId = (context.Payments.Count() + 1).ToString(),
                UId = userId,
                PayTime = DateTime.Now,
                Coin = amount,
                Type = "DP"
            };
            context.Payments.Add(nowPayment);
            context.SaveChanges();
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }

        ////[Authorize]
        [Route("getDepositRecord")]
        [HttpGet]
        public IActionResult getDepositRecord(string userId)
        {
            //得到用户所有充值记录
            var list = context.Payments.ToList();
            list = list.OrderByDescending(o => o.PayTime).ToList();//降序

            var returnList = new List<Payment> { };
            var totalPayment = context.Payments.Count();
            for (var i = 0; i < totalPayment; i++) 
            {
                if (list[i].UId == userId && list[i].Type == "DP") 
                {
                    returnList.Add(list[i]);
                }
            }

            return Ok(new
            {
                Success = true,
                DepositNum=returnList.Count(),
                List = returnList,
                msg = "Operation Done"
            });
        }


        ////[Authorize]
        [Route("getConsumeRecord")]
        [HttpGet]
        public IActionResult getConsumeRecord(string userId)
        {
            //得到用户所有充值记录
            var list = context.Payments.ToList();
            list = list.OrderByDescending(o => o.PayTime).ToList();//降序
            var returnList = new List<Payment> { };
            var totalPayment = context.Payments.Count();
            for (var i = 0; i < totalPayment; i++)
            {
                if (list[i].UId == userId && list[i].Type == "CS") 
                {
                    returnList.Add(list[i]);
                }
            }

            return Ok(new
            {
                Success = true,
                ConsumeNum = returnList.Count(),
                List = returnList,
                msg = "Operation Done"
            });
        }
    }
}
