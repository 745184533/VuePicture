using WebApplication1.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class BlogServices
    {
        private picturecommunityContext context;

        public BlogServices(picturecommunityContext context)
        {
            this.context = context;
        }
    }
}
