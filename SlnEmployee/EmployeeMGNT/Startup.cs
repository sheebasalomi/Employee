using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMGNT.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeMGNT
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }




        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) //dependancy injection service container
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddXmlSerializerFormatters();
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();

            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

            //AddSingleton - create only one instance for the service per applocation, used in the aplication life time and all the subsequent requests will use the same instance
            //AddTransient - this method will create a transient service, instances of transient services created each time it is being requested.
            //AddScoped - this method will create a scoped service, one instance of the service is created when get a request with in that scope, use the same instance within  a particular web request

            //Adv. of dependance injection - loosely coupled and easy for unit testing

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //-- setting DeveloperExceptionPageOptions 
                //DeveloperExceptionPageOptions options = new DeveloperExceptionPageOptions();
                //options.SourceCodeLineCount = 10; // setting the line count for developer exception page
                //app.UseDeveloperExceptionPage(options);
            }

            //  Middlewares(UseDefaultFiles,UseStaticFiles, UseFileServer)
            // Middle wares always statrt with USE

            //----loading default file first------
            //app.UseDefaultFiles(); // if this line is added, when the site is loaded the default.html will load. the default file will load only if it follows  app.UseStaticFiles()
            //app.UseStaticFiles();// wwwroot is the default static file directory - http://localhost:55937/images/lion.jpg will load the lion image from the directory  wwwroot 
            //OR
            // app.UseFileServer();


            //-------loading any other file like home.html first----
            //DefaultFilesOptions defaultFileOptions = new DefaultFilesOptions();
            //defaultFileOptions.DefaultFileNames.Clear();
            //defaultFileOptions.DefaultFileNames.Add("foo.html");
            //app.UseDefaultFiles(defaultFileOptions);
            //app.UseStaticFiles();
            //------------------------------------------------------

            // ---- UseFileServer middleware combine both UseDefaultFiles and UseStaticFile
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer(fileServerOptions);
            //----------------------------------

            app.UseStaticFiles();


            // app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); // conventional routing. Tag helper inject the controller and action name in place
            });
            // app.UseMvc();//attribute routing

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                    //await context.Response.WriteAsync("The environment is : " + env.EnvironmentName); // display the value of ASPNETCORE_ENVIRONMENT from launchSettings.json
                });
            });
        }
    }
}
