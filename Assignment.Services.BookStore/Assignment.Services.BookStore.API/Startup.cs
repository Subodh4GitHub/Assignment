// **************************************************************************
// <copyright file="Startup.cs" company="MyCompany">
//     Copyright ©MyCompany 2022
// </copyright>
// **************************************************************************

namespace Assignment.Services.BookStore.API
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Assignment.Services.BookStore.Infrastructure.Common;
    using Assignment.Services.BookStore.Infrastructure.DataContext;
    using Assignment.Services.BookStore.Infrastructure.HealthChecks;
    using Assignment.Services.BookStore.Infrastructure.Logger;
    using Assignment.Services.BookStore.Infrastructure.Managers;
    using Assignment.Services.BookStore.Infrastructure.Managers.Contracts;
    using Assignment.Services.BookStore.Infrastructure.Repositories;
    using Assignment.Services.BookStore.Infrastructure.Repositories.Contracts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for configuring the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration objects.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the application configuration objects.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure services called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddDbContext<BookStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BookStoreDatabase")));
            services.AddAutoMapper(typeof(Mapper));
            services.AddHealthChecks().AddDbContextCheck<BookStoreContext>();
            SerilogExtensions.AddSerilog();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Assignment.Services.BookStore.API",
                        Description = "Asp.net Core Backend Coding Exercise",
                        Contact = new OpenApiContact()
                        {
                            Name = "Subodh Kumar",
                            Email = "subodhincdac@gmail.com"
                        },
                        Version = "v1"
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configure called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="applicationBuilder">Application builder class.</param>
        /// <param name="webHostEnvironment">Web hosting environment.</param>
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assignment.Services.BookStore.API v1"));
            }
            else
            {
                applicationBuilder.UseExceptionHandler("/error");
            }

            applicationBuilder.UseHealthChecks(
                "/health",
                new HealthCheckOptions()
                {
                    ResponseWriter = async (context, report) =>
                    {
                        context.Response.ContentType = "application/json";
                        var response = new HealthCheckResponse()
                        {
                            Status = report.Status.ToString(),
                            HealthChecks = report.Entries.Select(x => new HealthCheck()
                            {
                                Component = x.Key,
                                Status = x.Value.Status.ToString(),
                                Description = x.Value.Description
                            }),
                            Duration = report.TotalDuration
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }
                });

            applicationBuilder.UseHttpsRedirection();

            applicationBuilder.UseRouting();

            applicationBuilder.UseAuthorization();

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
