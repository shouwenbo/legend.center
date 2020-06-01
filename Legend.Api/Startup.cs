using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legend.Api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Legend.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sk = @"-----BEGIN PUBLIC KEY-----MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDYLmZKhs+uiW8xANDsBiZUkDsJ10C1D93kQqW9nXQ7qrFO8Q9BtBoVScDVbAyqk+LgIo5ThfvUCUN5POyeNggeDPLKfetxJxOBo3rqtyypze2epqqx55c5t+9mYB89rYcEc9gtGrBNpWp5jgA+43JnrivIkdYC/AkkqUKDajLDWQIDAQAB-----END PUBLIC KEY-----";
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,           // 是否验证 Issuer
                    ValidateAudience = true,         // 是否验证 Audience
                    ValidateLifetime = true,         // 是否验证失效时间
                    ValidateIssuerSigningKey = true, // 是否验证 SecurityKey
                    ValidAudience = "www.baidu.com", // Configuration["audience"], // Audience
                    ValidIssuer = "www.baidu.com",   // Configuration["issuer"],   // Issuer, 这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sk)) // 拿到 SecurityKey
                };
            });
            {
                services.AddSingleton<IJWTService, JWTService>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
