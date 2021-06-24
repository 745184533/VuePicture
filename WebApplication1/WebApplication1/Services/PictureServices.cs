using WebApplication1.EFModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class PictureServices
        
    {
        private picturecommunityContext context;

        public PictureServices(picturecommunityContext context)
        {
            this.context = context;
        }

        //获取对应id的User，不存在则返回Null
        public User GetUserById(string userId)
        {
            return context.Users.FirstOrDefault(u => u.UId == userId);
        }

        //获取某个用户对应的点赞信息
        public Likespicture getLikes(string userId,string picId)
        {
            return context.Likespictures.FirstOrDefault(l => l.UId == userId && l.PId == picId);
        }

        //获取图片
        public Picture getPicture(string picId)
        {
            return context.Pictures.FirstOrDefault(p=>p.PId == picId);
        }

        //获取图片的总收藏数
        public int getPicStarNum(string picId)
        {
            return context.Favoritepictures.Count(f => f.PId == picId);
        }

        //获取图片总的评论数
        public int getPicCommentNum(string picId)
        {
            return context.Piccomments.Count(p => p.PId == picId);
        }

        public string[] getTag(string url)
        {
            string strInput = "python C:/ProgramData/Anaconda3/envs/ML/MobileNet/typeSever.py";
            //string strInput = "python D:/Anaconda3/envs/ML/MobileNet/typeSever.py";
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(strInput + "&exit");
            p.StandardInput.AutoFlush = true;
            //  string strOuput = p.StandardOutput.ReadToEnd();
            p.StandardInput.WriteLine(url + "\n");
            //获取输出信息
            string strOuput = "tset";
            string[] Out = new string[10];
            while (true)
            {
                strOuput = p.StandardOutput.ReadLine();
                if (strOuput == "Tags") break;

            }
            for (int i = 0; i < 10; i++)
                Out[i] = p.StandardOutput.ReadLine();

            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            return Out;

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
