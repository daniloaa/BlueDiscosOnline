using BlueDiscosOnline.Domain.Contracts.Repositories.Base;
using BlueDiscosOnline.Domain.Contracts.UnitiesOfWork;
using BlueDiscosOnline.Infrastructure.Data;
using BlueDiscosOnline.Infrastructure.Repositories.Base;
using System;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Infrastructure.UnitiesOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BlueDiscosOnlineContext _dbContext;

        public IRepository Repository { get; set; }

        public EFUnitOfWork(BlueDiscosOnlineContext dbContext)
        {
            _dbContext = dbContext;
            Repository = new Repository(_dbContext);
        }
       
        public object GetTransaction()
        {
            return _dbContext;
        }

        public void SetTransaction(object context)
        {
            _dbContext = (BlueDiscosOnlineContext)context;
            Repository = new Repository(_dbContext);
        }

        public async Task<int> Commit()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rollback()
        {
            try
            {
                _dbContext.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
        }
    }
}
