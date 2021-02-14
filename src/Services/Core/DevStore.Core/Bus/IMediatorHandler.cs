using System.Threading.Tasks;
using DevStore.Core.Messages;

namespace DevStore.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;

        Task<bool> SendCommand<T>(T command) where T : Command;
    }
}