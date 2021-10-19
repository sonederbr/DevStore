using DevStore.Catalog.Application.AutoMapper;
using DevStore.Catalog.Data;
using DevStore.Catalog.Domain.Events;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Rebus.Config;
using Rebus.ServiceProvider;

namespace DevStore.Catalog.Api
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
            services.AddDbContext<CatalogContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configure and register Rebus
            var queue = "queue_order";
            services.AddRebus(c => c
                .Transport(t => t.UseRabbitMq(Configuration.GetConnectionString("RabbitConnection"), queue))
            );

            services.AutoRegisterHandlersFromAssemblyOf<CourseEventHandler>();

            services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));

            services.RegisterServices();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.UseRebus(c =>
            {
                c.Subscribe<AlmostFullCourseEvent>().Wait();
                c.Subscribe<OrderStartedEvent>().Wait();
                c.Subscribe<OrderCanceledEvent>().Wait();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
