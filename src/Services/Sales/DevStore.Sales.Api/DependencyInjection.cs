using DevStore.Core.Communication.Bus;
using DevStore.Core.Data.EventSourcing;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Application.Events;
using DevStore.Sales.Application.Queries;
using DevStore.Sales.Data;

using EventSourcing;

using Microsoft.Extensions.DependencyInjection;

using Rebus.Handlers;

using SalesData = DevStore.Sales.Data;
using SalesDomain = DevStore.Sales.Domain;

namespace DevStore.Sales
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Bus
            services.AddScoped<IBusHandler, BusHandler>();

            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

            // Sales
            services.AddScoped<SalesDomain.IOrderRepository, SalesData.Repository.OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            //services.AddScoped<IHandleMessages<AddOrderItemCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<RemoveOrderItemCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<ApplyVoucherOrderCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<StartOrderCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<FinishOrderCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<CancelOrderAndDisrollFromCourseCommand>, OrderCommandHandler>();
            //services.AddScoped<IHandleMessages<CancelOrderCommand>, OrderCommandHandler>();

            //services.AddScoped<IHandleMessages<OrderDraftStartedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderItemAddedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderUpdatedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderItemRemovedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderEmptyRemovedEvent>, OrderEventHandler>();
            //services.AddScoped<IHandleMessages<OrderVoucherAppliedEvent>, OrderEventHandler>();
        }
    }
}