using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BooksBorrow.AppConfiguration;
using BooksBorrow.Common.Constants;
using BooksBorrow.Middlewares;
using BooksBorrow.RuntimeConfiguration;
using BooksBorrow.Security.Bearer;
using BooksBorrow.WebApi.Models;
using Exceptionless;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;

namespace BooksBorrow.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var repository = LogManager.CreateRepository(ConfigKey.RepositoryName);
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(y => y.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors();

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            JwtSettings setting = new JwtSettings();
            Configuration.Bind("JwtSettings", setting);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            var appSettings = new AppSettings();
            Configuration.Bind("AppSettings", appSettings);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = setting.Issuer,
                      ValidAudience = setting.Audience,
                      IssuerSigningKey = JwtSecurityKey.Create(setting.SecretKey)
                  };

                  options.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          var accessToken = context.Request.Query["access_token"];

                          if (!string.IsNullOrEmpty(accessToken))
                          {
                              context.Token = context.Request.Query["access_token"];
                          }
                          return Task.CompletedTask;
                      },
                      OnAuthenticationFailed = context =>
                      {
                          return Task.CompletedTask;
                      },
                      OnTokenValidated = context =>
                      {
                          return Task.CompletedTask;
                      }
                  };
              });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member",
                    policy => policy.RequireClaim("MembershipId"));
            });

            services.TryAdd(ServiceDescriptor.Transient<IAuthorizationPolicyProvider, BooksBorrowAuthorizationPolicyProvider>());

            RuntimeSettingBase runtimeSetting = new ProductionRuntimeSetting();
            runtimeSetting.Inject();
            services.CreateHttpContextAccessor();

            #region webapi help document

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "BooksBorrow API DOCUMENTATION",
                    Version = "v1"
                });
                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                foreach (var name in Directory.GetFiles(basePath, "*.XML", SearchOption.AllDirectories))
                {
                    c.IncludeXmlComments(name);
                }
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseExceptionless("cpaiPtRqHSaChHypM4raMihumdQUHr5x9sEOOCB5");
            app.UseDeveloperExceptionPage();

            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            );

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticHttpContext();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
