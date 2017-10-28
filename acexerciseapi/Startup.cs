#if DEBUG
#else
#define USE_ALIYUN
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace acexerciseapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

#if DEBUG
            Startup.DBConnectionString = Configuration.GetConnectionString("DebugConnection");
#else
            Startup.DBConnectionString = Configuration.GetConnectionString("AliyunConnection");
#endif
        }

        public IConfiguration Configuration { get; }
        internal static String DBConnectionString { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
#if DEBUG
                builder.WithOrigins(
                    "http://localhost:4500",
                    "https://localhost:4500"
                    )
#else
#if USE_MICROSOFTAZURE
                builder.WithOrigins(
                    "http://achihui.azurewebsites.net",
                    "https://achihui.azurewebsites.net"
                    )
#elif USE_ALIYUN
                builder.WithOrigins(
                    "http://118.178.58.187:5200",
                    "https://118.178.58.187:5200"
                    )
#endif
#endif
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                );

            app.UseMvc();
        }
    }
}
