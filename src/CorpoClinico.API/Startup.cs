using ConsultaMedica.Core.Communication.Mediator;
using ConsultaMedica.CorpoClinico.Application.Commands;
using ConsultaMedica.CorpoClinico.Data;
using ConsultaMedica.CorpoClinico.Domain;
using MediatR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Microsoft.Extensions.Options;
using ConsultaMedica.Core;
using ConsultaMedica.CorpoClinico.Application.Queries;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConsultaMedica.CorpoClinico.API
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

            services.Configure<ConfigurationSettings>(Configuration.GetSection("Configuration"));

            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ConfigurationSettings>>().Value);

            services.AddDbContext<CorpoClinicoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(typeof(Startup));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Serviço de Consulta Médica API", Version = "v1" });
            });

            services.AddMvc()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IAgendaMedicaRepository, AgendaMedicaRepository>();
            services.AddScoped<CorpoClinicoContext>();
            services.AddScoped<IRequestHandler<AgendarConsultaCommand, bool>, AgendaMedicaCommandHandler>();
            services.AddScoped<IAgendaMedicaQueries, AgendaMedicaQueries>();
            
            services.AddMvc(option => option.EnableEndpointRouting = false);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, CorpoClinicoContext corpoClinicoContext)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            loggerFactory.AddSerilog();

            corpoClinicoContext.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serviço de Consulta Médica");
            });

            app.UseMvc();
        }
    }
}
