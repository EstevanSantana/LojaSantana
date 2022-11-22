using System.Threading.Tasks;

namespace LS.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
