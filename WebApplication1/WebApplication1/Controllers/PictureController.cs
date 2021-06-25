using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EFModels;
using WebApplication1.Services;
using WebApplication1.ViewsModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PictureController : Controller
    {
        private picturecommunityContext context;
        private PictureServices services;

        public PictureController(picturecommunityContext context)
        {
            this.context = context;
            services = new PictureServices(context);
        }

        /// <summary>
        /// 点击进行赞或者已有赞取消赞
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="picId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        ////[Authorize]
        [Route("likePicture")]
        [HttpPost]
        public IActionResult likePicture(string userId,string picId,string Type)
        {
            //首先查找是否有现有的点赞
            var like=services.getLikes(userId, picId);
            var pic = services.getPicture(picId);
            if (like != null)//现在已经有 赞
            {
                //点击类型相同，取消赞
                
                context.Likespictures.Remove(like);
                pic.Likes--;//原有的赞减少
                context.Pictures.Attach(pic);
                context.SaveChanges();
            }
            else
            {//创建新的赞
                like = new Likespicture
                {
                    UId = userId,
                    PId = picId,
                    LikeTime = DateTime.Now,
                    LikeType = Type
                };
                context.Likespictures.Add(like);
                pic.Likes++;//原有的赞增加
                context.Attach(pic);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            }) ;
        }


        /// <summary>
        /// 进行收藏或已有收藏取消收藏
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="picId"></param>
        /// <returns></returns>
        ////[Authorize]
        [Route("favoritePicture")]
        [HttpPost]
        public IActionResult favoritePicture(string userId,string picId)
        {
            var favorite = context.Favoritepictures.FirstOrDefault(f => f.PId == picId);
            //var pic = services.getPicture(picId);
            if (favorite != null)
            {//已有收藏
                context.Favoritepictures.Remove(favorite);
                
                context.SaveChanges();
            }
            else
            {//创建新的收藏
                favorite = new Favoritepicture
                {
                    UId = userId,
                    PId = picId,
                    FavTime = DateTime.Now
                };
                context.Favoritepictures.Add(favorite);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success=true,
                msg="Operation Done"
            });
        }


        /// <summary>
        /// 创建评论或修改已有评论
        /// </summary>
        /// <param name="Comment"></param>
        /// <returns></returns>
        ////[Authorize]
        [Route("comment")]
        [HttpPost]
        public IActionResult comment([FromBody]comment Comment)
        {
            var nowComment = context.Piccomments.
                FirstOrDefault(c => c.UId == Comment.userId && c.PId == Comment.picId);
            if (nowComment != null)
            {//当前评论已经存在，可更改评论内容
                context.Entry(nowComment).CurrentValues.SetValues(new Piccomment
                {
                    UId = nowComment.UId,
                    PId = nowComment.PId,
                    Likes = nowComment.Likes,
                    PComment = Comment.content
                });
                context.SaveChanges();
            }
            else
            {
                nowComment = new Piccomment
                {
                    UId = Comment.userId,
                    PId = Comment.picId,
                    PComment = Comment.content,
                    Likes = 0
                };
                context.Piccomments.Add(nowComment);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }


        /// <summary>
        /// 返回图片页面所需信息  图片来源及信息/关注/点赞/收藏
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="picId"></param>
        /// <returns></returns>
        [Route("getPicViewInfo")]
        [HttpGet]
        public IActionResult getPicViewInfo(string userId,string picId)
        {
            //是否下载
            var HasDownload = false;
            //获取关注状态
            var publisherfollow = false;
            var publishUserId = context.Publishpictures.FirstOrDefault(p => p.PId == picId).UId;
            if(context.Follows.FirstOrDefault
                (f => f.FansId == userId && f.FollowId == publishUserId) != null) { publisherfollow = true; }
            //获取点赞状态
            var piclike = false;
            if (context.Likespictures.FirstOrDefault
                (l => l.UId == userId && l.PId == picId) != null){ piclike = true; }

            //获取收藏状态
            var picstar = false;
            if (context.Favoritepictures.FirstOrDefault
                (f => f.UId == userId && f.PId == picId) != null) { picstar = true; }

            //点赞数、收藏数
            var numberlike = context.Likespictures.Count(predicate => predicate.PId == picId);
            var numberfavorite = context.Favoritepictures.Count(predicate => predicate.PId == picId);

            //获取图片信息、图片下载量、图片作者、上传时间、作者头像。
            var pic = services.getPicture(picId);
            var Download = context.Downloads.Count(i => i.PId == picId);
            var LiUp=context.Publishpictures.FirstOrDefault(i => i.PId == picId);


            var uploadTime = LiUp.PublishTime;
            var LiName = context.Users.FirstOrDefault(i => i.UId == LiUp.UId);

            var info = context.Publishpictures.ToLookup(p => p.UId)[LiUp.UId].ToList();
            info = info.OrderByDescending(o => o.PublishTime).ToList();//降序

            var infoTag = context.Owntags.ToLookup(p => p.TagName)["avatar"].ToList();

            String userAvatar = "";
            for (int i = 0; i < infoTag.Count; i++)
            {
                var tempFindTag = info.Find(p => p.PId == infoTag[i].PId);
                if (tempFindTag != null)
                {
                    userAvatar = context.Pictures.FirstOrDefault(p => p.PId == tempFindTag.PId).PUrl;
                    break;
                }
            }


            //获取图片Tag
            var pictag = context.Owntags.ToLookup(t => t.PId)[picId].ToArray();

            //获取评论信息
            var piccomment = "";
            var tmpComment = context.Piccomments.FirstOrDefault(c => c.UId == userId && c.PId == picId);
            if (tmpComment != null) { piccomment = tmpComment.PComment; }

            //是否下载
            if(context.Downloads.FirstOrDefault(c=>c.PId==picId&&c.UId==userId)!=null)
            {
                HasDownload = true;
            }



            //组织返回信息
            return Ok(new
            {
                Success=true,
                Message="numberLike,numberFavorite,Downloads分别是点赞量，收藏量，下载量",
                picUrl = pic.PUrl,

                publisherFollow = publisherfollow,
                publisherId=publishUserId,

                picLike = piclike,
                picStar = picstar,

                numberLike=numberlike,
                numberFavorite=numberfavorite,

                picHeight = pic.PHeight,
                picWidth = pic.PWidth,
                picInfo = pic.PInfo,

                picTags = pictag,
                hasDownload=HasDownload,
                Downloads=Download,


                uploadtime=uploadTime,
                uploadName=LiName.UName,
                uploadURL= userAvatar,
                pirce =pic.Price,
                nowComment=piccomment
            }) ;
        }


        /// <summary>
        /// 获取当前图片所有评论
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        
        [Route("getAllComment")]
        [HttpGet]
        public IActionResult getAllComment(string picId)
        {
            var commentNum = context.Piccomments.Count(c => c.PId == picId);
            var comments = context.Piccomments.ToLookup(c => c.PId)[picId].ToList();

            var returnComment = new List<OtherComment> { };
            foreach(var temp in comments)
            {
                User tempuser=context.Users.FirstOrDefault(predicate => predicate.UId == temp.UId);
                var add = new OtherComment
                {
                    userId = tempuser.UId,
                    userName = tempuser.UName,
                    content = temp.PComment
                };
                returnComment.Add(add);
            }


            //返回列表
            return Ok(new
            {
                CommentNum=commentNum,
                Comments=returnComment
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         
        [Route("getAllTags")]
        [HttpGet]
        public IActionResult getAllTags()
        {
            var taglist = context.Tags.ToList();

            //返回列表
            return Ok(new
            {
                taglists = taglist
            });
        }

        /// <summary>
        /// 根据tag返回相关图片信息列表
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [Route("searchByTag")]
        [HttpGet]
        public IActionResult searchByTag(string tag)
        {
            var returnList = new List<picInfo> { };
            var picList = context.Owntags.ToLookup(t => t.TagName)[tag].ToList();
            foreach (var pic in picList)
            {
                var nowPic = services.getPicture(pic.PId);
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
            return Ok(new
            {
                Success = true,
                picList = returnList,
                msg = "Operation Done"
            });
        }



        ///<summary>
        ///收费并且下载图片。
        /// </summary>
        ////[Authorize]
        [Route("Download")]
        [HttpGet]
        public IActionResult Download(string userId,string picId)
        {
            var nowPic = services.getPicture(picId);
            var publisher = services.GetUserById(context.Publishpictures.FirstOrDefault(p => p.PId == picId).UId);
            if (context.Downloads.FirstOrDefault(d => d.UId == userId && d.PId == picId) == null
                && context.Publishpictures.FirstOrDefault(p => p.UId == userId && p.PId == picId) == null)
            {//表示第一次下载且图片不是本人发布
                //进行购买
                var wallet = context.Wallets.Find(userId);
                if (wallet.Coin >= nowPic.Price)
                {//能够支付
                    wallet.Coin = wallet.Coin-nowPic.Price;
                    wallet.BuyNum=wallet.BuyNum+1;
                    context.Wallets.Attach(wallet);
                    context.SaveChanges();
                    var newDownload = new Download
                    {
                        UId = userId,
                        PId = picId,
                        DownloadTime = DateTime.Now,
                        Price = nowPic.Price
                    };
                    context.Downloads.Add(newDownload);
                    context.SaveChanges();
                    //在payment中增加消费记录
                    var newPayment = new Payment
                    {
                        PayId = (context.Payments.Count() + 1).ToString(),
                        UId = userId,
                        PayTime = DateTime.Now,
                        Coin = nowPic.Price,
                        Type = "CS"
                    };
                    context.Payments.Add(newPayment);
                    context.SaveChanges();
                    //为图片发布者增加硬币
                    var publisherWallet = context.Wallets.Find(publisher.UId);
                    publisherWallet.Coin += nowPic.Price;
                    context.Wallets.Attach(publisherWallet);
                    context.SaveChanges();
                }
                else
                {
                    return Ok(new
                    {
                        Success = false,
                        msg = "Lack of Coin"
                    }); 
                    
                }
            }
            //已经购买进行下载
            return Ok(new
            {
                Success = true,
                downloadUrl = nowPic.PUrl,
                msg = "请右键图片进行手动下载"
            });
        }


        /// <summary>
        /// 根据请求次数获取最新的三张图片
        /// </summary>
        /// <param name="requestTimes"></param>
        /// <returns></returns>
        [Route("get3Pic")]
        [HttpGet]
        public IActionResult get3Pic(int requestTimes)
        {
            var returnList = new List<picInfo> { };
            
            var picList = context.Pictures.ToList();
            picList = picList.OrderByDescending(o => int.Parse(o.PId)).ToList();//降序

            List<Picture> tempInfo = new List<Picture>();

            int Exceptionsimg = 0;
            for (int m = 0; m < picList.Count; m++)
            {
                //排除自己的头像.
                var exceptionImage = context.Owntags.FirstOrDefault(p => p.PId == picList[m].PId && p.TagName == "avatar");
                if (exceptionImage == null)
                {
                    tempInfo.Add(picList[m]);
                    Exceptionsimg++;
                }
            }
            picList = tempInfo;


            int nowPici = services.getPicNum();

            for(int i= 4 * requestTimes;i< 4 * requestTimes+4&&i< Exceptionsimg; i++)
            {
                var pic = picList[i];
                var pub = context.Publishpictures
                    .FirstOrDefault(p => p.PId == picList[i].PId);
                var pubId = pub.UId;
                var publisher = services.GetUserById(context.Publishpictures
                    .FirstOrDefault(p => p.PId == picList[i].PId).UId);
                var picInfo = new picInfo
                {
                    picId = pic.PId,
                    picUrl = pic.PUrl,
                    publisherId = publisher.UId,
                    publisherName = publisher.UName,
                    info = pic.PInfo,
                    starNum = context.Favoritepictures.Count(f => f.PId == pic.PId),
                    likeNum = context.Likespictures.Count(l => l.PId == pic.PId),
                    commNum = context.Piccomments.Count(c => c.PId == pic.PId)
                };
                returnList.Add(picInfo);
            }

            //for (int i = 0; i < 4 && nowPici >= 0; ++i)
            //{//返回三张pic
            //    var pic = picList[nowPici];
            //    var pub = context.Publishpictures
            //        .FirstOrDefault(p => p.PId == picList[nowPici].PId);
            //    var pubId = pub.UId;
            //    var publisher = services.GetUserById(context.Publishpictures
            //        .FirstOrDefault(p => p.PId == picList[nowPici].PId).UId);
            //    var picInfo = new picInfo
            //    {
            //        picId = pic.PId,
            //        picUrl = pic.PUrl,
            //        publisherId = publisher.UId,
            //        publisherName = publisher.UName,
            //        info = pic.PInfo,
            //        starNum = context.Favoritepictures.Count(f => f.PId == pic.PId),
            //        likeNum = context.Likespictures.Count(l => l.PId == pic.PId),
            //        commNum = context.Piccomments.Count(c => c.PId == pic.PId)
            //    };
            //    returnList.Add(picInfo);
            //    nowPici--;
            //}

            return Ok(new
            {
                Success = true,
                ReturnList = returnList,
                msg = "Operation Done"
            });
        }


    }


}
