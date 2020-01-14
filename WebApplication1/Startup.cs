using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;

// AzureADB2C��UI�n
using B2CMyApp.Library;
using B2CMyApp.Library.AzureAdB2C;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace B2CMyApp {

    /// <summary>
    /// 
    /// </summary>
    public class Startup {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public Startup( IConfiguration config, IHostEnvironment env ) {
            _config = config;
            _env = env;
        }

        private readonly IConfiguration _config;

        private readonly IHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services ) {
            IdentityModelEventSource.ShowPII = true;

            services
                .AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => _config.Bind("AzureADB2C", options)); //appsettings.json�̃}�b�s���O

            /*
            .net core��CORS��L���ɂ�����@
            https://www.it-swarm.net/ja/c%23/aspnet-core-webapi%E3%81%A7cors%E3%82%92%E6%9C%89%E5%8A%B9%E3%81%AB%E3%81%99%E3%82%8B%E6%96%B9%E6%B3%95/832167108/
            CORS��L���ɂ���ɂ�AddMVC�̑O�ɓ���邱��
            https://code-examples.net/ja/q/2a52da8
            */
            services.AddCors(); // CORS�������邽��(addMvc�̑O)

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configure( IApplicationBuilder app ) {

            // ?
            if ( _env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
            }

            // wwwroor�ȉ��̐ÓI�ȃt�@�C��
            app.UseStaticFiles();

            // �F�،n
            app.UseAuthentication();

            // CORS�̋���(addMVC�̑O��) 
            app.UseCors(options => {
                options.WithOrigins("https://YagioB2CTenant.b2clogin.com")
                    .AllowAnyOrigin();
            });

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
