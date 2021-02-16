using MediatR;
using Microsoft.Extensions.DependencyInjection;
using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Data;
using DevStore.Catalog.Data.Repository;
using DevStore.Catalog.Domain;
using DevStore.Catalog.Domain.Events;
using DevStore.Communication.Mediator;
using DevStore.Sales.Application.Commands;
using DevStore.Sales.Domain;
using DevStore.Sales.Data.Repository;
using DevStore.Sales.Application.Queries;
using DevStore.Core.Messages.CommonMessages.Notifications;

namespace DevStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalog
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseAppService, CourseAppService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<AlmostFullCourseEvent>, CourseEventHandler>();


            //Sales
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
            
        }
    }
}