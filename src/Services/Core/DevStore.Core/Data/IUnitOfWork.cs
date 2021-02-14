using System.Threading.Tasks;

namespace DevStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}