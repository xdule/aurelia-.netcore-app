using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hahn.ApplicationProcess.Web.Filters;
using Hahn.ApplicationProcess.Web.Validator;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Domain.Interface;
using Hahn.ApplicatonProcess.May2020.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Hahn.ApplicationProcess.Web.Helpers;
using Microsoft.OpenApi.Models;

namespace Hahn.ApplicationProcess.Web
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            services.AddControllers();

            /* services.AddCors(c =>
             {
                 c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
             });*/

            

            services.AddMvc(options=> { options.Filters.Add<ValidationFilter>(); }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddScoped<IApplicantService, ApplicantServices>();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo{ Title = "Abdullah api doc", Version = "v1" });
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .AddFilter("Microsoft", LogLevel.Information)
                    .AddFilter("System", LogLevel.Error);
            });
            //  services.AddTransient<IValidator<Applicant>, ApplicantValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new Helpers.SwaggerOptions();
            Configuration.GetSection(nameof(Helpers.SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });

            

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint,swaggerOptions.Description);
            });            

            app.UseHttpsRedirection();            

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
