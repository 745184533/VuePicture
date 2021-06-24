using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EFModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }
        [Route("Index")]
        [HttpGet]
        public Blog Index()
        {
            return null;
        }

        [Route("test")]
        [HttpGet]
        public bool Test()
        {
            return true;
        }
    }
}
