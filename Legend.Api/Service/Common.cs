using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Legend.Api.Service
{
    public interface ICommon
    {
        string JwtPivateKey { get; }
    }

    public class Common : ICommon
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public Common(IWebHostEnvironment hostingEnvironmen)
        {
            _hostingEnvironment = hostingEnvironmen;
        }
        /// <summary>
        /// Jwt的密钥
        /// </summary>
        public string JwtPivateKey
        {
            get
            {
                return File.ReadAllText($"{_hostingEnvironment.ContentRootPath}/static/pri.key.txt");
            }
        }
    }
}
