
using Microsoft.EntityFrameworkCore;
using STCA_DataLib.Model;
//using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_DataLib.Data
{
    public class MSSQL_STCA_DbContext : DbContext
    {
        private readonly string _connectionstring = @"Data Source=LAPTOP-0KKLKRNG\SQLEXPRESS;Initial Catalog=STCA_DEV;Integrated Security=true;TrustServerCertificate=true;";

        public MSSQL_STCA_DbContext()
        {
            
        }

        public MSSQL_STCA_DbContext(DbContextOptions<MSSQL_STCA_DbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ZonaHoraria
            modelBuilder.Entity<ZonaHoraria>().ToTable("ZONA_HORARIA");
            modelBuilder.Entity<ZonaHoraria>().HasAlternateKey("Nombre");


            // RangoTiempo
            modelBuilder.Entity<RangoTiempo>().ToTable("RANGO_TIEMPO");
            modelBuilder.Entity<RangoTiempo>().HasAlternateKey(x => new { x.DiaSemana, x.HoraInicial, x.HoraFinal });


            // ZonaHoraria_RangoTiempo
            modelBuilder.Entity<ZonaHoraria_RangoTiempo>().ToTable("ZONA_HORARIA_RANGO_TIEMPO");
            modelBuilder.Entity<ZonaHoraria_RangoTiempo>()
                .HasKey(x => new { x.ZonaHorariaId, x.RangoTiempoId });

        }

        public DbSet<ZonaHoraria> ZonasHorarias { get; set; }

        public DbSet<RangoTiempo> RangosTiempos { get; set; }

    }
}
