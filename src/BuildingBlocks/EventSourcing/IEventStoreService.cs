using System.Threading.Tasks;

using EventStore.ClientAPI;

namespace EventSourcing
{
    public interface IEventStoreService
    {
        public IEventStoreConnection GetConnection();
    }
}
