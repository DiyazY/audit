using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using audit.Entities;
using audit.Models;
using audit.Repositories;
using audit.Services.Implementations;
using audit.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;

namespace audit
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
            #region cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                    builder => builder.AllowAnyOrigin()
                                                      .AllowAnyMethod()
                                                      .AllowAnyHeader()
                                                      .AllowCredentials()
                );
            });
            #endregion

            #region services
            services.AddTransient<IGenericRepository<AuditObject>, GenericRepository<AuditObject>>();
            services.AddTransient<IAuditService, AuditService>();
            #endregion

            #region db
            var dbName = "AuditDb";
            var mongoClient = new MongoClient(Configuration.GetConnectionString(dbName));
            var database = mongoClient.GetDatabase(dbName);
            services.AddScoped<IMongoDatabase>(_ => database);
            #endregion

            services.AddMvc();

            #region api-doc
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "AUDIT API",
                    Description = "Open api",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Diyaz Yakubov", Email = "diyaz.yakubov@gmail.com", Url = "https://www.facebook.com/diyaz.yakubov.9" },
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = System.IO.Path.Combine(basePath, "audit.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            #region api-doc
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUDIT API V1");
            });
            #endregion

            app.UseMvc();
        }
    }
}
