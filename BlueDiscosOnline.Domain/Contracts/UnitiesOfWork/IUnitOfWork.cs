using BlueDiscosOnline.Domain.Contracts.Repositories.Base;
using System;
using System.Threading.Tasks;

namespace BlueDiscosOnline.Domain.Contracts.UnitiesOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repository { get; set; }
        object GetTransaction();
        void SetTransaction(object conext);
        Task<int> Commit();
        void Rollback();
    }
}
