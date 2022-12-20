using Microsoft.EntityFrameworkCore;
using ShopBanDo.Models;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ShopBanDo.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        Task<bool> Commit();
        Task Rollback();
    }
}
