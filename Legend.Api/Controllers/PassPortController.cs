using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legend.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Legend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PassPortController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JWTService _jwtService;
        public PassPortController(IConfiguration configuration, JWTService jwtService)
        {
            _configuration = configuration;
            _jwtService = jwtService;
        }
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public string Login()
        {
            return _jwtService.GetToken("admin");
        }
        [Route("info")]
        [HttpPost]
        public string GetInfo()
        {
            var sub = User.FindFirst(d => d.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return "123";
        }
    }
}