using BlueDiscosOnline.Domain.Contracts.Repositories.Base;
using BlueDiscosOnline.Domain.Contracts.Services;
using BlueDiscosOnline.Domain.Contracts.Services.Base;
using BlueDiscosOnline.Domain.Contracts.UnitiesOfWork;
using BlueDiscosOnline.Infrastructure.Repositories.Base;
using BlueDiscosOnline.Infrastructure.UnitiesOfWork;
using BlueDiscosOnline.Services.Services;
using BlueDiscosOnline.Services.Services.Base;
using Microsoft.Extensions.DependencyInjection;

namespace BlueDiscosOnline.API.DependencyInjection
{
    public static class DependencyResolver
    {
        public static void Resolve(IServiceCollection services)
        {
            //Bases
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IUnitOfWorkFactory, EFUnitOfWorkFactory>();
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));

            //Services
            services.AddTransient<ISpotifyService, SpotifyService>();
            services.AddTransient<IGeneroService, GeneroService>();
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<IVendaService, VendaService>();
        }
    }
}
