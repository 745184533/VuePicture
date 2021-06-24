using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using WebApplication1.EFModels;
using WebApplication1.Tools;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace WebApplication1
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
            services.AddRazorPages();

            services.AddEntityFrameworkSqlServer();

            services.AddCors(options =>
            {
                options.AddPolicy("any",
                builder => builder.WithOrigins("http://localhost:6306",
                "http://localhost:8055").AllowAnyHeader().
                AllowAnyOrigin().AllowAnyMethod());

            });
            var DBConnection = "Server=localhost;port=3306;database=picturecommunity;uid=root;pwd=526874Lyf;CharSet=utf8";

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 24));

            services.AddControllersWithViews().AddNewtonsoftJson(option =>
                //����ѭ������
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );

            services.AddDbContextPool<picturecommunityContext>(options =>
                options.UseMySql(DBConnection,serverVersion)
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = "Api",
                    Description = "˵���ĵ�",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "AAAApi", 
                        Email = "892542", Url = new Uri("http://www.baidu.com") }
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "WebApplication1.xml");
                c.IncludeXmlComments(xmlPath, true);

            });

            services.AddMvc(options =>
            {
            }).AddXmlSerializerFormatters();


            //����Jwt��֤
            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
            var jwtSetting = new JwtSetting();
            Configuration.Bind("JwtSetting", jwtSetting);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //�Ƿ���֤������
                    ValidateIssuer = true,
                    ValidIssuer = jwtSetting.Issuer,//������
                                                    //�Ƿ���֤������
                    ValidateAudience = true,
                    ValidAudience = jwtSetting.Audience,//������
                                                        //�Ƿ���֤��Կ
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),

                    ValidateLifetime = true, //��֤��������
                    RequireExpirationTime = false //����ʱ��
                };
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

             
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseCors("any");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            

        }
    }
}
