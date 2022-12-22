using Microsoft.EntityFrameworkCore;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopBanDo.Interface
{
    public interface IGenericRepository<T> where T:class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity,bool saveChange);
        void AddAsync(T entity, bool saveChange);
        void AddRange(IEnumerable<T> entities, bool saveChange);
        void Remove(T entity,bool saveChange);
        void Update(T entity,T entitynew, bool saveChange);
        void RemoveRange(IEnumerable<T> entities,bool saveChange);

    }
}
