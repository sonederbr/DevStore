using System.Linq;
using System.Threading.Tasks;

using DevStore.Core.Communication.Bus;
using DevStore.Core.Messages.CommonMessages.DomainEvents;

namespace DevStore.Finance.Data
{
    public static class BusExtension
    {
        public static async Task PublishEvents(this IBusHandler bus, FinanceContext ctx)
        {
            var domainEntitiesWithNotifications = ctx.ChangeTracker
               .Entries<Entity>()
               .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntitiesWithNotifications
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntitiesWithNotifications.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await bus.PublishEvent(domainEvent);
                });

            var domainEntitiesWithIntegratedNotifications = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.IntegratedNotifications != null && x.Entity.IntegratedNotifications.Any());

            var integrationEvents = domainEntitiesWithIntegratedNotifications
                .SelectMany(x => x.Entity.IntegratedNotifications)
                .ToList();

            domainEntitiesWithIntegratedNotifications.ToList()
                .ForEach(entity => entity.Entity.ClearIntegrationEvents());

            var tasksIntegrationEvents = integrationEvents
                .Select(async (integrationEvent) => {
                    await bus.PublishIntegrationEvent(integrationEvent);
                });

            tasksIntegrationEvents.Select(p => tasks.Append(p));

            await Task.WhenAll(tasks);
        }
    }
}