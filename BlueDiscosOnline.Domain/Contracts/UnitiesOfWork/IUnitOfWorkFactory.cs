using System;

namespace BlueDiscosOnline.Domain.Contracts.UnitiesOfWork
{
    public interface IUnitOfWorkFactory : IDisposable
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
