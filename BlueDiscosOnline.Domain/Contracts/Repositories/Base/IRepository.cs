using BlueDiscosOnline.Domain.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Domain.Contracts.Repositories.Base
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : BaseEntity;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, bool noTracking = false) where T : BaseEntity;
        Task<T> Get<T>(object id) where T : BaseEntity;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression, params string[] includes) where T : BaseEntity;
        Task<T> First<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        Task Edit<T>(object id, T entity) where T : BaseEntity;
        Task<int> Count<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;        
    }
}
