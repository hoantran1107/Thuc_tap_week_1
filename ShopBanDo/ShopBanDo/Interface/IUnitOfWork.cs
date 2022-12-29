using Microsoft.EntityFrameworkCore;
using ShopBanDo.Models;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ShopBanDo.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task<bool> Commit();
        Task Rollback();
    }
}
