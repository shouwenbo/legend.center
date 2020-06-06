using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legend.Api.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Legend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly JWTService _jwtService;
        public JWTService JWTService { get; set; }
        public TestController(IWebHostEnvironment hostingEnvironment, JWTService jwtService)
        {
            _hostingEnvironment = hostingEnvironment;
            _jwtService = jwtService;
        }
        [Route("aaa")]
        public string Test()
        {
            return _jwtService.TestGet();
        }

        [Route("GetProjectPath")]
        public string GetProjectPath()
        {
            var path = _hostingEnvironment.WebRootPath;
            var path2 = _hostingEnvironment.ContentRootPath;
            System.IO.File.WriteAllText("asd.txt","dsa");
            return $"{path},{path2}";
        }
    }
}