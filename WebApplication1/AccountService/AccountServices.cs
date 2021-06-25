using Microsoft.Extensions.Options;
using WebApplication1.EFModels;
using WebApplication1.ViewsModel;
using WebApplication1.ViewsModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Tools;

namespace WebApplication1.Services
{
    public class AccountServices
    {
        private picturecommunityContext context;
        private JwtSetting _jwtSetting;

        public AccountServices(picturecommunityContext context,IOptions<JwtSetting> options)
        {
            this.context = context;
            _jwtSetting = options.Value;
        }
        
        //查找当前是否存在用户
        public bool checkExist(string userName,string password)
        {
            var user = context.Users.FirstOrDefault(u => u.UName == userName && u.UPassword == password);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        //根据用户信息获取Token
        public string GetToken(LoginUser user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("name", user.userName) // 用户名
            };
            //创建令牌
            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                signingCredentials: _jwtSetting.Credentials,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
        //获取对应id的User，不存在则返回Null
        public User GetUserById(string userId)
        {
            return context.Users.FirstOrDefault(u => u.UId == userId);
        }
        
        //根据id取得对应User详细信息
        public Userinfo GetUserInfoById(string userId)
        {
            
            return context.Userinfos.FirstOrDefault(u => u.UId == userId);
        }

        //根据id对应的钱包
        public Wallet GetWalletById(string userId)
        {
            return context.Wallets.FirstOrDefault(w => w.UId == userId);
        }

        //获取个人图片主页的数据信息
        public ProfileInfo GetProfileInfoById(string userId)
        {
            
            var StarNum = 0;var LikeNum = 0;var CommentNum = 0;var FollowNum = 0;

            #region 获取图片相关各类数据
            var picGroup = context.Publishpictures.ToLookup(p => p.UId)[userId].ToList();
            foreach (var pic in picGroup)
            {
                StarNum += context.Favoritepictures.Count(f => f.PId == pic.PId);

                LikeNum += (int)context.Pictures.Find(pic.PId).Likes;
            }
            #endregion

            #region 获取个人相关各类数据
            CommentNum += context.Piccomments.Count(p => p.UId == userId);
            FollowNum += context.Follows.Count(f => f.FollowId == userId);
            #endregion

            return new ProfileInfo {
                starNum=StarNum,
                likeNum=LikeNum,
                followNum=FollowNum,
                commentNum=CommentNum
            };
        }
        public int getPicNum()
        {
            return (int)context.Tablecounts.Find(1).Picture;
        }
        public int getUserNum()
        {
            return (int)context.Tablecounts.Find(1).Users;
        }
        public int getBlogNum()
        {
            return (int)context.Tablecounts.Find(1).Blog;
        }
    }
}
