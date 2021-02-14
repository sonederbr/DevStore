using MediatR;
using Microsoft.Extensions.DependencyInjection;
using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Data;
using DevStore.Catalog.Data.Repository;
using DevStore.Catalog.Domain;
using DevStore.Catalog.Domain.Events;
using DevStore.Core.Bus;

namespace DevStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // Catalog
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseAppService, CourseAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<LowStockCourseEvent>, CourseEventHandler>();
        }
    }
}