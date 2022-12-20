using ShopBanDo.Interface;
using ShopBanDo.Models;
using System.Threading.Tasks;

namespace ShopBanDo.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly dbshopContext _context;

        public UnitOfWork(dbshopContext context) =>
            _context = context;

        public IOrderRepository OrderRepository => throw new System.NotImplementedException();

        public IProductRepository ProductRepository => throw new System.NotImplementedException();

        public IGenericRepository<OrderDetail> OrderDetailRepository => throw new System.NotImplementedException();

        public async Task<bool> Commit()
        {
            var success = (await _context.SaveChangesAsync()) > 0;

            // Possibility to dispatch domain events, etc

            return success;
        }

        public void Dispose() =>
            _context.Dispose();

        public Task Rollback()
        {
            // Rollback anything, if necessary
            return Task.CompletedTask;
        }
    }
}
