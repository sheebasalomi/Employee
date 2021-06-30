using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeMGNT
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //  Middlewares(UseDefaultFiles,UseStaticFiles, UseFileServer)
            // Middle wares always statrt with USE

            //----loading default file first------
            //app.UseDefaultFiles(); // if this line is added, when the site is loaded the default.html will load. the default file will load only if it follows  app.UseStaticFiles()
            //app.UseStaticFiles();// wwwroot is the default static file directory - http://localhost:55937/images/lion.jpg will load the lion image from the directory  wwwroot 

            //-------loading any other file like home.html first----
            //DefaultFilesOptions defaultFileOptions = new DefaultFilesOptions();
            //defaultFileOptions.DefaultFileNames.Clear();
            //defaultFileOptions.DefaultFileNames.Add("foo.html");
            //app.UseDefaultFiles(defaultFileOptions);
            //app.UseStaticFiles();
            //------------------------------------------------------

            // ---- UseFileServer middleware combine both UseDefaultFiles and UseStaticFile
            FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            app.UseFileServer(fileServerOptions);
            //----------------------------------


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
