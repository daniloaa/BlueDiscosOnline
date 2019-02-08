using BlueDiscosOnline.Domain.Contracts.Repositories.Base;
using BlueDiscosOnline.Domain.Entities.Base;
using BlueDiscosOnline.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Infrastructure.Repositories.Base
{
    public class Repository : IRepository
    {
        private BlueDiscosOnlineContext _context { get; set; }

        public Repository(BlueDiscosOnlineContext context)
        {
            _context = context;

        }

        public async void Add<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, bool noTracking = false) where T : BaseEntity
        {
            if (noTracking)
                return _context.Set<T>().Where(expression).AsNoTracking();
            else
                return _context.Set<T>().Where(expression);
        }

        public async Task<T> Get<T>(object id) where T : BaseEntity
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, params string[] includes) where T : BaseEntity
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include).AsQueryable();
                }
            }

            return query.Where(expression);
        }

        public async Task Edit<T>(object id, T entity) where T : BaseEntity
        {
            var oldEntity = await Get<T>(id);
            _context.Entry(oldEntity).State = EntityState.Modified;
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public async Task<T> First<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            return await _context.Set<T>().Where(expression).CountAsync();
        }

    }
}
