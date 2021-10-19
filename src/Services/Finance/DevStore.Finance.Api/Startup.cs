
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Finance.Business.Events;
using DevStore.Finance.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Rebus.Config;
using Rebus.ServiceProvider;

namespace DevStore.Finance.Api
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
            services.AddDbContext<FinanceContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configure and register Rebus
            var queue = "queue_order";
            services.AddRebus(c => c
                .Transport(t => t.UseRabbitMq(Configuration.GetConnectionString("RabbitConnection"), queue))
            );

            services.AutoRegisterHandlersFromAssemblyOf<PaymentEventHandler>();

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
                c.Subscribe<OrderEnrolledAcceptedEvent>().Wait();
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
