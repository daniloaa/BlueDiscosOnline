using BlueDiscosOnline.Domain.Contracts.UnitiesOfWork;
using BlueDiscosOnline.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlueDiscosOnline.Infrastructure.UnitiesOfWork
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IConfiguration _configuration { get; }

        public EFUnitOfWorkFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var builder = new DbContextOptionsBuilder<BlueDiscosOnlineContext>();      

            var connection = _configuration["ConnectionStrings:SqliteConnectionString"];
            builder.UseSqlite(connection);

            return new EFUnitOfWork(new BlueDiscosOnlineContext(builder.Options));
        }

        public void Dispose()
        {

        }
    }

    
}
