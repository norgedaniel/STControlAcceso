
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
    public class STCA_DbContext : DbContext
    {
        private readonly string? _connectionstring = @"Data Source=LAPTOP-0KKLKRNG\SQLEXPRESS;Initial Catalog=STCA_DEV;Integrated Security=true;TrustServerCertificate=true;";


        // Default constructor for initial create migration
        //public STCA_DbContext()
        //{
        //    _connectionstring = @"Data Source=LAPTOP-0KKLKRNG\SQLEXPRESS;Initial Catalog=STCA_DEV;Integrated Security=true;TrustServerCertificate=true;";
        //}

        public STCA_DbContext(DbContextOptions<STCA_DbContext> options) : base(options)
        {
        }

        //public STCA_DbContext(string connectionstring)
        //{
        //    _connectionstring = connectionstring;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ZonaHoraria_RangoTiempo>()
                .HasKey(x => new { x.ZonaHorariaId, x.RangoTiempoId });

        }

        public DbSet<ZonaHoraria> ZonaHoraria { get; set; }

    }
}
