using BlueDiscosOnline.Domain.Entities;
using BlueDiscosOnline.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BlueDiscosOnline.Infrastructure.Data
{
    public class BlueDiscosOnlineContext : DbContext
    {
        public BlueDiscosOnlineContext(DbContextOptions<BlueDiscosOnlineContext> options)
               : base(options)
        {

        }

        #region Tabelas

        private DbSet<Album> Album { get; set; }
        private DbSet<Genero> Genero { get; set; }
        private DbSet<GeneroPercentualDia> GeneroPercentualDia { get; set; }
        private DbSet<Venda> Venda { get; set; }
        private DbSet<VendaItem> VendaItem { get; set; }

        #endregion

        #region Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AlbumConfiguration(modelBuilder.Entity<Album>());
            new GeneroConfiguration(modelBuilder.Entity<Genero>());
            new GeneroPercentualDiaConfiguration(modelBuilder.Entity<GeneroPercentualDia>());
            new VendaConfiguration(modelBuilder.Entity<Venda>());
            new VendaItemConfiguration(modelBuilder.Entity<VendaItem>());

            base.OnModelCreating(modelBuilder);
        }

        #endregion

    }

   

}
