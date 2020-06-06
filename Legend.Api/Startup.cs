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
            Logger.Info("����һ��");//�˴�������־��¼������¼��־
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
                    ValidateIssuer = true,           // �Ƿ���֤ Issuer
                    ValidateAudience = true,         // �Ƿ���֤ Audience
                    ValidateLifetime = true,         // �Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true, // �Ƿ���֤ SecurityKey
                    ValidAudience = "www.baidu.com", // Configuration["audience"], // Audience
                    ValidIssuer = "www.baidu.com",   // Configuration["issuer"],   // Issuer, �������ǰ��ǩ��jwt������һ��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lajj89757")) // �õ� SecurityKey
                };
            });
            #region ΢��DIע��
            {
                // ������ʹ���� Autofac����������Ͳ��� ΢��DI ��ע�뷽ʽ��
                //  services.AddSingleton<IJWTService, JWTService>();
                services.AddSingleton<ICommon, Common>();
            }
            #endregion
            #region ����Controllerȫ����Autofac����
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            // services.AddControllersWithViews().AddControllersAsServices(); // ���߽�Controller���뵽Services�У�����д����Ĵ���Ϳ���ʡ����
            #endregion
        }

        /// <summary>
        /// ע������
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ��ӷ���ע��
            builder.RegisterType<JWTService>();

            #region ��Controller��ʹ������ע��
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

            // ǿ��ʹ�� https, ���� http ����Զ� 307 �ض��� https, ����������⻨�˺þ�...���Խ׶�ע����һ�д���ͺ�
            // app.UseHttpsRedirection(); 

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins); // ע���������

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
