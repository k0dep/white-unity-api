using System.Threading.Tasks;

namespace WhiteUnity.DataAccess.Abstraction
{
    public interface IUnitOfWork
    {
        void Rollback();
        Task SaveChanges();
    }
}