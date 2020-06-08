using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legend.Api.Models.Result
{
    public class ApiResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public ApiResult(int status, string message, object data)
        {
            this.Status = status;
            this.Message = message;
            this.Data = data;
        }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; } 
    }
}
