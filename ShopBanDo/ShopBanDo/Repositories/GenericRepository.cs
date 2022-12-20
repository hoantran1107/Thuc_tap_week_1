using ShopBanDo.Interface;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopBanDo.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //neu dung private o day thi phai viet getter cho lop con
        //con khong thi dung proteced con ke thua
        protected readonly dbshopContext _context;
        public GenericRepository(dbshopContext context)
        {
            _context = context;
        }
        //them 1 record = bang ghi
        public void Add(T entity, bool saveChange)
        {
            //Set tao EFC.DBSet<> dung de query hoặc lưu trữ
            _context.Set<T>().Add(entity);
            if(saveChange) _context.SaveChanges();
        }

        public async void AddAsync(T entity, bool saveChange)
        {
            await _context.Set<T>().AddAsync(entity);
            if (saveChange) await _context.SaveChangesAsync();
        }

        //them list cac record
        public void AddRange(IEnumerable<T> entities, bool saveChange)
        {
            _context.Set<T>().AddRange(entities);
            if(saveChange) _context.SaveChanges();
        }
        //Find a set of record that matches pass expression
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        //get all records
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        //get id 
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity, bool saveChange)
        {
            _context.Set<T>().Remove(entity);
            if(saveChange) _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities, bool saveChange)
        {
            _context.Set<T>().RemoveRange(entities);
            if(saveChange) _context.SaveChanges();
        }


        public void Update(T entity, T entitynew, bool saveChange)
        {
            entity = entitynew;
            _context.Update(entity);
            if (saveChange) _context.SaveChanges();
        }
    }
}
