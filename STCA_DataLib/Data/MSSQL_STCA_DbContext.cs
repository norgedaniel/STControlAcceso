
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

            modelBuilder.Entity<ZonaHoraria>().HasAlternateKey("Nombre");

            modelBuilder.Entity<ZonaHoraria_RangoTiempo>()
                .HasKey(x => new { x.ZonaHorariaId, x.RangoTiempoId });

        }

        public DbSet<ZonaHoraria> ZonaHoraria { get; set; }

    }
}
