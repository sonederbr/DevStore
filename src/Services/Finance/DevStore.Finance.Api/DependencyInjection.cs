using DevStore.Core.Communication.Bus;
using DevStore.Core.Data.EventSourcing;
using DevStore.Finance.AntiCorruption;
using DevStore.Finance.Data;

using EventSourcing;

using Microsoft.Extensions.DependencyInjection;

using FinanceBusiness = DevStore.Finance.Business;
using FinanceData = DevStore.Finance.Data;

namespace DevStore.Finance
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

            services.AddScoped<FinanceBusiness.IOrderRepository, FinanceData.Repository.OrderRepository>();
            services.AddScoped<FinanceBusiness.IPaymentCreditCardFacade, PaymentCreditCardFacade>();
            services.AddScoped<FinanceBusiness.IPaymentService, FinanceBusiness.PaymentService>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<FinanceContext>();
        }
    }
}