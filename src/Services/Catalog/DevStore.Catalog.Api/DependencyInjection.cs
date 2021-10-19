using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Data;
using DevStore.Catalog.Data.Repository;
using DevStore.Catalog.Domain;
using DevStore.Core.Communication.Bus;
using DevStore.Core.Data.EventSourcing;

using EventSourcing;

using Microsoft.Extensions.DependencyInjection;

namespace DevStore.Catalog
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

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseAppService, CourseAppService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<CatalogContext>();

            ////Domain Events
            //services.AddScoped<IHandleMessages<AlmostFullCourseEvent>, CourseEventHandler>();
            //services.AddScoped<IHandleMessages<OrderStartedEvent>, CourseEventHandler>();
            //services.AddScoped<IHandleMessages<OrderCanceledEvent>, CourseEventHandler>();
        }
    }
}