using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace RestCustomerServiceCoreY
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
            services.AddCors(); //then app.AddCors in Configure method

            //Alternatively define policies and use them in Configure for whole application.
            //OR spcifically for each controller and methods provided.
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigin",
            //        builder => builder.WithOrigins("http://example.com"));

            //    options.AddPolicy("AllowAnyOrigin",
            //        builder => builder.AllowAnyOrigin());
            //options.AddPolicy("AllowAnyOriginGetPost",
            //    builder => builder.AllowAnyOrigin().WithMethods("GET", "POST"));

           // });



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // SWAGGER
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Items API", Version = "v1.0" });

                c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Homebrew API",
                        Version = "v1.01337",
                        Description = "Example of OpenAPI for api/localItems //",
                        TermsOfService = new Uri("https://moodle.zealand.dk/pluginfile.php/172691/mod_workshop/intro/RESTService3-swagger.pdf"),
                        Contact = new OpenApiContact() // Contact()
                        {
                            Name = "{your-name}",
                            Email = "{your-email}",
                            Url = new Uri("https://moodle.zealand.dk/pluginfile.php/172691/mod_workshop/intro/RESTService3-swagger.pdf")
                        },
                        License = new OpenApiLicense() // License()
                        {
                            Name = "No licence required",
                            Url = new Uri("https://moodle.zealand.dk/pluginfile.php/172691/mod_workshop/intro/RESTService3-swagger.pdf")
                        }
                    }

                );


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

           // app.UseCors("AllowAnyOrigin");  //Policy solution on app.
                                            // app.UseCors("AllowAnyOriginGetPost");



            //app.UseCors(
            //   options =>
            //   {
            //       options.AllowAnyOrigin().WithMethods("GET","POST"); // allow everything from anywhere

            //   });

            app.UseHttpsRedirection();
            app.UseCors(
              options =>
              {
                  options.AllowAnyOrigin().AllowAnyMethod(); // allow everything from anywhere
                  //options.WithOrigins("my.com").AllowAnyMethod();
                  //options.AllowAnyOrigin().WithMethods("GET", "POST");

               }
           

              );
           
            app.UseMvc();

            // SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api/help"; // Swagger Ui URL
            });

        }
    }
}
