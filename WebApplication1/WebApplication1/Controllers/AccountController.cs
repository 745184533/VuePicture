using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using WebApplication1.EFModels;
using WebApplication1.Services;
using WebApplication1.ViewsModel;
using WebApplication1.ViewsModels;
using WebApplication1.Tools;
using Microsoft.EntityFrameworkCore;
using clockMe;
using System.Runtime.InteropServices;
using ATLProjectLib;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private picturecommunityContext context;
        private AccountServices services;
        private PictureServices PictureServices;

        private IWebHostEnvironment hostingEnv;

        string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif",
            "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };

        public AccountController(picturecommunityContext context,IOptions<JwtSetting> options, 
            IWebHostEnvironment env)
        {
            this.context = context;
            this.services = new AccountServices(context,options);
            this.PictureServices = new PictureServices(context);
            this.hostingEnv = env;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public IActionResult login([FromBody]LoginUser user)
        {
            if (services.checkExist(user.userName, user.userPassword))
            {//登录成功
                var User = context.Users
                    .FirstOrDefault(u => u.UName == user.userName && u.UPassword == user.userPassword);
                //颁发Token
                var token = services.GetToken(user);
                return Ok(new
                {
                    Success = true,
                    userId = User.UId,
                    userName=User.UName,
                    Token = token,
                    Type = "Bearer"
                }) ;
            }
            return Ok(new
            {
                Success = false
            }) ;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> register([FromBody]RegisterUser RUser)
        {
            if (!services.checkExist(RUser.userName, RUser.userPassword))
            {//没有该账号密码存在
                //创建user
                var user = new User { };
                user.UId = (services.getUserNum() + 1).ToString();
                user.UName = RUser.userName;
                user.UPassword = RUser.userPassword;
                user.UStatus = "AC";
                user.UType = "US";
                user.CreateTime = DateTime.Now;
                context.Users.Add(user);
                await context.SaveChangesAsync();

                //添加用户数
                var tableCount = context.Tablecounts.Find(1);
                tableCount.Users += 1;
                context.Tablecounts.Attach(tableCount);
                context.SaveChanges();
                //创建相应userInfo
                var newUserInfo = new Userinfo
                { 
                    UId = user.UId,
                    UName = user.UName,
                    Birthday = Convert.ToDateTime("1800-01-01 "),
                    Mail = RUser.Email,
                    PhoneNumber = "0",
                    Message=""
                };
                context.Userinfos.Add(newUserInfo);
                await context.SaveChangesAsync();

                //创建相应钱包
                var newUserWallet = new Wallet
                {
                    UId = user.UId,
                    BuyNum = 0,
                    Coin = 0,
                    PublishNum = 0
                };
                context.Wallets.Add(newUserWallet);
                await context.SaveChangesAsync();

                //完成了注册赋予Token
                var token = services.GetToken(
                    new LoginUser { userName = user.UName, userPassword = user.UPassword });
                return Ok(new
                {
                    Success = true,
                    userId=user.UId,
                    userName=user.UName,
                    Token=token,
                    msg="New User Registered"
                });
            }
            else
            {//已有该账号密码存在
                return Ok(new
                {
                    Success = false,
                    msg = "Same UserName&Password"
                });
            }
        }

        /// <summary>
        /// 获取当前所有用户信息
        /// </summary>
        /// <returns></returns>
        [Route("getAllUsers")]
        [HttpGet]
        public IEnumerable<User> getAllUsers()
        {
            Calclock.startClock();
            return context.Users.ToArray();
        }

        [Route("TestClock")]
        [HttpGet]
        public int TestClock()
        {
            
            return Calclock.startClock();
        }

        [DllImport("Util.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr getFileName(string path);

        [DllImport("Util.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int getFileID(string path);


        [Route("TestDLL")]
        [HttpGet]
        public string TestDLL(string filename)
        {
            //Console.WriteLine("开始？");
            IntPtr pRet = getFileName(filename);
            Console.WriteLine(pRet);
            string strRet = Marshal.PtrToStringAnsi(pRet);
            Console.WriteLine(strRet);

            return strRet;
        }

        [Route("TestCom")]
        [HttpGet]
        public long TestCom(int filename)
        {
            Simple a = new Simple();
            int b = 0;
            a.testRule(filename, out b);
            return b;
        }


        /// <summary>
        /// 获得对应id的用户详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("getUserInfo")]
        [HttpGet]
        public IActionResult getUserInfo(string userId)
        {
            //首先判断是否存在该Id
            var user = services.GetUserById(userId);
            if (user != null)
            {//用户存在
                var userInfo = services.GetUserInfoById(userId);
                if (userInfo == null)
                {//如果此时没有userInfo
                    return Ok(new
                    {
                        Success = false,
                        msg = "No such User"
                    });
                }
                return Ok(new
                {
                    Success = true,
                    //Picture=
                    UserName = user.UName,
                    Name = userInfo.UName,
                    Birthday = userInfo.Birthday,
                    phoneNumber = userInfo.PhoneNumber,
                    Email = userInfo.Mail,
                    Message = userInfo.Message,
                });
            }
            else
            {//用户不存在
                return Ok(new
                {
                    Success = false,
                    msg = "No such User"
                });
            }
        }


        /// <summary>
        /// 保存填写后用户详细信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("saveUserInfo")]
        [HttpPost]
        public IActionResult saveUserInfo([FromBody] UserInfo info)
        {
            var user = services.GetUserById(info.UserId);
            var userInfo = services.GetUserInfoById(info.UserId);
            user.UName = info.UserName;
            context.Users.Attach(user);
            userInfo.UName = info.Name;
            userInfo.Mail = info.Email;
            userInfo.Message = info.Message;
            userInfo.PhoneNumber = info.PhoneNumber;
            userInfo.Birthday = info.Birthday;

            context.Userinfos.Attach(userInfo);
            context.SaveChanges();
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }
        

        /// <summary>
        /// 获得用户个人图片主页信息，点赞数等
        /// </summary>
        //[Authorize]
        [Route("getProfileInfo")]
        [HttpGet]
        public IActionResult getProfileInfo(string userId)
        {
            var info = services.GetProfileInfoById(userId);
            return Ok(new
            {
                Success=true,
                StarNum=info.starNum,
                LikeNum=info.likeNum,
                CommentNum=info.commentNum,
                FollowNum=info.followNum
            });
        }

        ///<summary>
        ///获取用户个人图片主页的图片信息
        /// 
        /// </summary>
        //[Authorize]
        [Route("getProfilePicture")]
        [HttpGet]
        public IActionResult getProfilePicture(string userId)
        {
            //我的上传
            var info = context.Publishpictures.ToLookup(p => p.UId)[userId].ToList();
            info = info.OrderByDescending(o => o.PublishTime).ToList();//降序
            //我的收藏
            var infos = context.Favoritepictures.ToLookup(p => p.UId)[userId].ToList();
            infos = infos.OrderByDescending(o => o.PId).ToList();//降序

            int count1 = info.Count();
            int count2 = infos.Count();
            //?初始化局部list?
            //int[] a = new []int;
            var tempPicture = new List<ProfilePicture> { };

            var tempPictures = new List<ProfilePicture> { };

            ///ProfilePicture[] tempPictures = new ProfilePicture[count2];
            ///ProfilePicture[] tempPictures = new ProfilePicture[count2];

            //int[] a = new int[10] ;
            int i = 0;

            foreach(var Picture in info)
            {
                //上传图片信息
                var temp1 = new ProfilePicture
                {
                    like = context.Likespictures.Count(p => p.PId == Picture.PId),
                    favorite = context.Favoritepictures.Count(p => p.PId == Picture.PId),
                    comment = context.Piccomments.Count(p => p.PId == Picture.PId),
                    thatpicture = context.Pictures.FirstOrDefault(p => p.PId == Picture.PId)
                };
                tempPicture.Add(temp1);
                ++i;
            }
            i = 0;
            foreach(var Picture in infos)
            {
                //收藏图片信息
                var temp1 = new ProfilePicture
                {
                    like = context.Likespictures.Count(p => p.PId == Picture.PId),
                    favorite = context.Favoritepictures.Count(p => p.PId == Picture.PId),
                    comment = context.Piccomments.Count(p => p.PId == Picture.PId),
                    thatpicture = context.Pictures.FirstOrDefault(p => p.PId == Picture.PId)
                };
                tempPictures.Add(temp1);
                ++i;
            }
            return Ok(new
            {
                Success=true,
                Upload=tempPicture,
                favorite=tempPictures
            }
            );
        }


        /// <summary>
        /// 关注用户或已关注取消关注用户
        /// </summary>
        /// <param name="FansId"></param>
        /// <param name="followId"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("followUser")]
        [HttpPost]
        public IActionResult followUser(string FansId, string followId)
        {
            var follow = context.Follows.FirstOrDefault(f => f.FansId == FansId && f.FollowId == followId);
            if (follow != null)
            {//已经关注，则取消关注
                context.Follows.Remove(follow);
                context.SaveChanges();
            }
            else
            {
                follow = new Follow
                {
                    FansId = FansId,
                    FollowId = followId
                };
                context.Follows.Add(follow);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }

        ///<summary>
        ///下载图片
        /// </summary>
        ////[Authorize]
        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormCollection forms)
        {
            

            //需要绑定图片名和图片id
            StringValues[] temp = { "", "", "" };
            string[] thagTag = { "tag", "tag1", "tag2" };
            //int[5] a;
            for (int i = 0; i < 3; ++i)
            {
                forms.TryGetValue(thagTag[i], out temp[i]);
            }

            StringValues information = "";
            forms.TryGetValue("PInfo", out information);

            StringValues userID = "";
            forms.TryGetValue("userId", out userID);

            StringValues Price = "";
            forms.TryGetValue("Price", out Price);
            string tempsd = Price;
            int Prices = int.Parse(tempsd);
            


            var file = Request.Form.Files[0];


            long size = file.Length;

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "pictures total size > 100MB , server refused !"
                });
            }


            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            //@
            string filePath = "F:/MyCode/MyC#/picCommunity/WebApplication1/WebApplication1/wwwroot/img/";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string suffix = fileName.Split('.')[1];


            //检查文件后缀名确保是图片而不是其他文件。
            if (!pictureFormatArray.Contains(suffix))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "the picture format not support ! you must upload files " +
                    "that suffix like 'png','jpg','jpeg','bmp','gif','ico'.",
                    Name = fileName
                });
            }
            //文件名命名？
            //存取图片的时候以id为准
            //context.Pictures.Count();


            fileName = Guid.NewGuid() + "." + suffix;

            string fileFullName = filePath + fileName;

            int height = 0;
            int width = 0;


            await using (FileStream fs = System.IO.File.Create(fileFullName))
            {
                file.CopyTo(fs);
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                height = image.Height;
                width = image.Width;
                fs.Flush();
            }


            //刷新为服务器的图片。
            fileFullName = "http://localhost:6306/img/" + fileName;
            var tableCount = context.Tablecounts.Find(1);

            Picture tempPicture = new Picture
            {
                PId = (tableCount.Picture + 1).ToString(),
                PUrl = fileFullName,
                PInfo = information,//还是要能用http访问，不是https
                PHeight=height,
                PWidth=width,
                PStatus="OK",//图片状态不确定。
                Price=Prices,
                Likes=0,
                Dislikes=0,
                CommNum = 0
            };
            //承接前一步异步保存。
            context.Pictures.Add(tempPicture);
            await context.SaveChangesAsync();

            

            


            //List<tag> contextTag = context.Tags.ToList();
            for (int i=0;i<3;++i)
            {
                String tempTagxx = temp[i].ToString();
                if (tempTagxx != "")
                {
                    Tag isLegal=context.Tags.AsNoTracking().FirstOrDefault(p => p.TagName == tempTagxx);

                    if(isLegal==null)
                    {
                        //表示这是用户新增的tag
                        isLegal = new Tag
                        {
                            TagName = tempTagxx
                        };
                        //因为外码依赖要先增加tag
                        context.Tags.Add(isLegal);
                        await context.SaveChangesAsync();
                    }

                    Owntag tempTag = new Owntag
                    {
                        PId = tempPicture.PId,
                        TagName = tempTagxx
                    };
                    context.Owntags.Add(tempTag);
                }
            }
            await context.SaveChangesAsync();



            Publishpicture tempPublish = new Publishpicture
            {
                UId=userID,
                PId=tempPicture.PId,
                PublishTime=DateTime.Now
            };
            context.Publishpictures.Add(tempPublish);
            await context.SaveChangesAsync();

            //添加图片数量

            tableCount.Picture += 1;
            context.Tablecounts.Attach(tableCount);
            await context.SaveChangesAsync();


            string message = $"{file.FileName} file(s) /{size} bytes uploaded successfully!";

            //Json("tag1", temp[0].ToString());
            string OwnTags = "tag1:" + temp[0].ToString() + ",tag2:" + temp[1].ToString() + ",tag3:" + temp[2].ToString();
            return Ok(new
            {
                Success = true,
                Message = message,
                PictureHeight=tempPicture.PHeight,
                PictureWidth=tempPicture.PWidth,
                Price=Prices,
                PictureURL=tempPicture.PUrl,
                OwnTag=OwnTags
            });
        }

        ///<summary>
        ///搜索相似图片
        /// 
        /// </summary>
        [Route("SimilarPicture")]
        [HttpPost]
        public async Task<IActionResult> SimilarPicture([FromForm] IFormFile forms)
        {
            var files = Request.Form.Files;

            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "pictures total size > 100MB , server refused !"
                });
            }
            var file = files[0];
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');



            string filePath = "C:" + @"\Pics\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string suffix = fileName.Split('.')[1];


            //检查文件后缀名确保是图片而不是其他文件。
            if (!pictureFormatArray.Contains(suffix))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "the picture format not support ! you must upload files " +
                    "that suffix like 'png','jpg','jpeg','bmp','gif','ico'.",
                    Name = fileName
                });
            }


            fileName = Guid.NewGuid() + "." + suffix;

            string fileFullName = filePath + fileName;

            int height = 0;
            int width = 0;


            await using (FileStream fs = System.IO.File.Create(fileFullName))
            {
                file.CopyTo(fs);
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                height = image.Height;
                width = image.Width;
                fs.Flush();
            }

            string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

            //刷新为服务器的图片。
            fileFullName = "C:/Pics/" + fileName;
            

            //string[] ReturnTag = PictureServices.getTag("D:/Entertainment/Entertainment/MechineLearning/Data/test/5.jpg");
            string[] ReturnTag = PictureServices.getTag(fileFullName);

            var returnList = new List<picInfo> { };

            //循环3个tag
            for(int i=0;i<3;++i)
            {
                var picList = context.Owntags.ToLookup(t => t.TagName)[ReturnTag[i]].ToList();
                //循环每个tag里面的图片
                foreach (var pic in picList)
                {
                    var nowPic = PictureServices.getPicture(pic.PId);
                    var pubulisher = services.GetUserById(context.Publishpictures
                        .FirstOrDefault(p => p.PId == nowPic.PId).UId);
                    var newPicInfo = new picInfo
                    {

                        picId = nowPic.PId,
                        picUrl = nowPic.PUrl,
                        publisherId = pubulisher.UId,
                        publisherName = pubulisher.UName,
                        info = nowPic.PInfo,
                        starNum = context.Favoritepictures.Count(f => f.PId == nowPic.PId),
                        likeNum = context.Likespictures.Count(l => l.PId == nowPic.PId),
                        commNum = context.Piccomments.Count(c => c.PId == nowPic.PId)
                    };
                    returnList.Add(newPicInfo);
                }
            }

            return Ok(new
            {
                Success = true,
                picList = returnList,
                msg = "Operation Done"
            });


        }

        ///<summary>
        ///第一回合上传
        /// 
        /// </summary>
        //[Authorize]
        [Route("Upload1")]
        [HttpPost]
        public async Task<IActionResult> Upload1([FromForm] IFormCollection forms)
        {
            StringValues userID = "";
            forms.TryGetValue("userId", out userID);

            var PictureId = context.Tablecounts.Find(1).Picture + 1;
 
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "pictures total size > 100MB , server refused !"
                });
            }

            //只能上传一张图片顺便贴标签
            var file = files[0];

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            //@
            string filePath = "C:" + @"\Pics\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string suffix = fileName.Split('.')[1];


            //检查文件后缀名确保是图片而不是其他文件。
            if (!pictureFormatArray.Contains(suffix))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "the picture format not support ! you must upload files " +
                    "that suffix like 'png','jpg','jpeg','bmp','gif','ico'.",
                    Name = fileName
                });
            }

            fileName = Guid.NewGuid() + "." + suffix;

            string fileFullName = filePath + fileName;
            int height = 0;
            int width = 0;


            await using (FileStream fs = System.IO.File.Create(fileFullName))
            {
                file.CopyTo(fs);
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                height = image.Height;
                width = image.Width;
                fs.Flush();
            }
            //刷新为服务器的图片。
            fileFullName = "http://172.81.239.44:8002/" + fileName;
            var Tanfile = "C:/Pics/" + fileName;

            Picture tempPicture = new Picture
            {
                PId = (context.Tablecounts.Find(1).Picture + 1).ToString(),
                PUrl = fileFullName,
                PHeight = height,
                PWidth = width,
                PStatus = "OK",//图片状态不确定。
                Price=0,
                PInfo="NULL",
                Likes = 0,
                Dislikes = 0,
                CommNum = 0
            };

            //承接前一步异步保存。
            context.Pictures.Add(tempPicture);
            await context.SaveChangesAsync();

            Publishpicture tempPublish = new Publishpicture
            {
                UId = userID,
                PId = tempPicture.PId,
                PublishTime = DateTime.Now
            };
            context.Publishpictures.Add(tempPublish);
            await context.SaveChangesAsync();

            //添加图片数量
            var tableCount = context.Tablecounts.Find(1);
            tableCount.Picture += 1;
            context.Tablecounts.Attach(tableCount);
            context.SaveChanges();

            string[] AITag = PictureServices.getTag(Tanfile);
            string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

            return Ok(new
            {
                Success = true,
                Tags=AITag,
                pictureHeight=height,
                pictureWidth=width,
                Message=message,
                pictureURL=fileFullName,
                pictureId= PictureId
            });
        }


        ///<summary>
        ///第二回合上传
        /// 
        /// </summary>
        //[Authorize]
        [Route("Upload2")]
        [HttpPost]
        public async Task<IActionResult> Upload2([FromForm] IFormCollection forms)
        {
            StringValues pid = "";
            StringValues userID = "";
            StringValues information = "";
            StringValues sPrice = "";

            //需要绑定图片名和图片id
            StringValues[] temp = { "", "", "" };
            string[] thagTag = { "tag", "tag1", "tag2" };
            //int[5] a;
            for (int i = 0; i < 3; ++i)
            {
                forms.TryGetValue(thagTag[i], out temp[i]);
            }

            forms.TryGetValue("userId",out userID);
            forms.TryGetValue("pictureId", out pid);
            forms.TryGetValue("PInfo", out information);
            forms.TryGetValue("Price", out sPrice);
            int prices = int.Parse(sPrice);

            var LiPicture = context.Pictures.FirstOrDefault(s=>s.PId==pid);
            LiPicture.Price = prices;
            LiPicture.PInfo = information;
            context.Pictures.Attach(LiPicture);
            for(int i=0;i<3;++i)
            {
                var tagPicture = new Owntag
                {
                    PId = pid,
                    TagName = temp[i]
                };
                context.Owntags.Add(tagPicture);
            }
            
                



            return Ok(new
            {
                Success = true,
                ownTag=temp,
                Price=prices,
                PInfo=information
            }) ;
        }

    }
}
