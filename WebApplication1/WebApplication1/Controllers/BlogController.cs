using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BlogController : Controller
    {
        private picturecommunityContext context;
        private BlogServices services;
        public BlogController(picturecommunityContext context)
        {
            this.context = context;
            this.services = new BlogServices(context);
        }

        [Route("getTen")]
        [HttpGet]
        public IActionResult getTen(int times)
        {
            //得到最新的十条博客
            var list = context.Blogs.ToList();
            list = list.OrderByDescending(o => o.BDate).ToList();//降序
            var returnList = new List<ReturnBlogInfo> { };
            var blogNum = context.Tablecounts.Find(1).Blog;


            for (var i =10*times; i <blogNum; ++i)
            {
                if (i >= 10 * times + 10) break;
                var nowBlogUser = context.Ownblogs.FirstOrDefault(b => b.BId == list[(int)i].BId);
                var nowBlogUserInfo = context.Userinfos.FirstOrDefault(u => u.UId == nowBlogUser.UId);
                var newReturnBlogInfo = new ReturnBlogInfo
                {

                    blogDate = (DateTime)list[(int)i].BDate,
                    blogId = list[(int)i].BId,
                    content = list[(int)i].BText,
                    blogType = list[(int)i].BType,
                    userId = nowBlogUserInfo.UId,
                    userName = nowBlogUserInfo.UName
                };
                returnList.Add(newReturnBlogInfo);
            }

            return Ok(new
            {
                Success = true,
                Times = times,
                List = returnList,
                msg = "Operation Done"
            });
        }


        ////[Authorize]
        [Route("writeBlog")]
        [HttpPost]
        public IActionResult writeBlog([FromBody] BlogInfo Blog)
        {
            var nowblog = new Blog
            {
                BDate = DateTime.Now,
                BId = (context.Tablecounts.Find(1).Blog + 1).ToString(),
                BText = Blog.content,
                BType = "tt"
            };
            context.Blogs.Add(nowblog);
            context.SaveChanges();
            //添加blog数量
            var tableCount = context.Tablecounts.Find(1);
            tableCount.Blog += 1;
            context.Tablecounts.Attach(tableCount);
            context.SaveChanges();
            //添加联系集
            var newOwnBlog = new Ownblog
            {
                UId = Blog.userId,
                BId = (context.Tablecounts.Find(1).Blog).ToString()
            };
            context.Ownblogs.Add(newOwnBlog);
            context.SaveChanges();
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }
    }
}
