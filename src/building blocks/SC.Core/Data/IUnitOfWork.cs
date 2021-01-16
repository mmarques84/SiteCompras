using System.Threading.Tasks;

namespace SC.Core.Data
{
    public interface IUnitOfWork
    {
        //deixar o commit independente
        Task<bool> Commit();
    }
}