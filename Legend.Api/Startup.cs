using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Legend.Api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Legend.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Logger.Info("进来一个");//此处调用日志记录函数记录日志
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _hostingEnvironment;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,           // 是否验证 Issuer
                    ValidateAudience = true,         // 是否验证 Audience
                    ValidateLifetime = true,         // 是否验证失效时间
                    ValidateIssuerSigningKey = true, // 是否验证 SecurityKey
                    ValidAudience = "www.baidu.com", // Configuration["audience"], // Audience
                    ValidIssuer = "www.baidu.com",   // Configuration["issuer"],   // Issuer, 这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lajj89757")) // 拿到 SecurityKey
                };
            });
            #region 微软DI注入
            {
                // 由于我使用了 Autofac，所以这里就不用 微软DI 的注入方式了
                //  services.AddSingleton<IJWTService, JWTService>();
                services.AddSingleton<ICommon, Common>();
            }
            #endregion
            #region 配置Controller全部由Autofac创建
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            // services.AddControllersWithViews().AddControllersAsServices(); // 或者将Controller加入到Services中，这样写上面的代码就可以省略了
            #endregion
        }

        /// <summary>
        /// 注册容器
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 添加服务注册
            builder.RegisterType<JWTService>();

            #region 在Controller中使用属性注入
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 强制使用 https, 访问 http 后会自动 307 重定向到 https, 发现这个问题花了好久...测试阶段注释这一行代码就好
            // app.UseHttpsRedirection(); 

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins); // 注入跨域请求

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
