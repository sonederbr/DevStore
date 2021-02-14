using System.Threading.Tasks;
using DevStore.Core.Messages;

namespace DevStore.Core.Bus
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
    }
}