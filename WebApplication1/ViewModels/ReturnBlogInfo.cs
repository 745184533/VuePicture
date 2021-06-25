using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewsModels
{
    public class ReturnBlogInfo
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string blogId { get; set; }
        public DateTime blogDate { get; set; }
        public string blogType { get; set; }
        public string content { get; set; }
    }
}
