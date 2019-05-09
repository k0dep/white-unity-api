using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhiteUnity.DataAccess.Abstraction;

namespace WhiteUnity.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        

        public void Rollback()
        {
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}