using System.Text.Json.Serialization;

using DevStore.Core.DomainObjects.DTO;
using DevStore.Core.Messages;
using DevStore.Core.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Application;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Application.Events;
using DevStore.Sales.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;

namespace DevStore.Sales.Api
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
            services.AddDbContext<SalesContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configure and register Rebus
            var queue = "queue_order";
            services.AddRebus(configure => configure
                .Transport(t => t.UseRabbitMq(Configuration.GetConnectionString("RabbitConnection"), queue))
                //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), nomeFila))
                //.Subscriptions(s => s.StoreInMemory())
                .Routing(r => // Mensagens de acordo com a fila, ele usa namespace p serializar.
                {
                    r.TypeBased()
                        //.MapAssemblyOf<Message>(queue)
                        //.MapAssemblyOf<StartSagaCommand>(queue)
                        .MapAssemblyOf<StartOrderCommand>(queue);
                })
                .Sagas(s => s.StoreInSqlServer(Configuration.GetConnectionString("DefaultConnection"), "Sagas", "SagaIndexTable"))
                //.Sagas(s => s.StoreInMemory())
            );

            services.AutoRegisterHandlersFromAssemblyOf<OrderSaga>();

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
                //c.Subscribe<OrderStartedEvent>().Wait();
                c.Subscribe<OrderCanceledEvent>().Wait();
                c.Subscribe<OrderEnrolledAcceptedEvent>().Wait();
                c.Subscribe<OrderEnrolledRejectedEvent>().Wait();
                c.Subscribe<PaymentRealizedEvent>().Wait();
                c.Subscribe<PaymentRefusedEvent>().Wait();

                c.Subscribe<OrderDraftStartedEvent>().Wait();
                c.Subscribe<OrderItemAddedEvent>().Wait();
                c.Subscribe<OrderUpdatedEvent>().Wait();
                c.Subscribe<OrderItemRemovedEvent>().Wait();
                c.Subscribe<OrderEmptyRemovedEvent>().Wait();
                c.Subscribe<OrderVoucherAppliedEvent>().Wait();
                c.Subscribe<OrderFinishedEvent>().Wait();
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
