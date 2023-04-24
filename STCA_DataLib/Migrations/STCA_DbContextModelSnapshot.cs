﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using STCA_DataLib.Data;

#nullable disable

namespace STCA_DataLib.Migrations
{
    [DbContext(typeof(STCA_DbContext))]
    partial class STCA_DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("STCA_DataLib.Model.RangoTiempo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaSemana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("HoraFinal")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraInicial")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("RangoTiempo");
                });

            modelBuilder.Entity("STCA_DataLib.Model.ZonaHoraria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ZonaHoraria");
                });

            modelBuilder.Entity("STCA_DataLib.Model.ZonaHoraria_RangoTiempo", b =>
                {
                    b.Property<int>("ZonaHorariaId")
                        .HasColumnType("int");

                    b.Property<int>("RangoTiempoId")
                        .HasColumnType("int");

                    b.HasKey("ZonaHorariaId", "RangoTiempoId");

                    b.HasIndex("RangoTiempoId");

                    b.ToTable("ZonaHoraria_RangoTiempo");
                });

            modelBuilder.Entity("STCA_DataLib.Model.ZonaHoraria_RangoTiempo", b =>
                {
                    b.HasOne("STCA_DataLib.Model.RangoTiempo", "RangoTiempo")
                        .WithMany("ZonaHoraria_RangoTiempo")
                        .HasForeignKey("RangoTiempoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STCA_DataLib.Model.ZonaHoraria", "ZonaHoraria")
                        .WithMany("ZonaHoraria_RangoTiempo")
                        .HasForeignKey("ZonaHorariaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RangoTiempo");

                    b.Navigation("ZonaHoraria");
                });

            modelBuilder.Entity("STCA_DataLib.Model.RangoTiempo", b =>
                {
                    b.Navigation("ZonaHoraria_RangoTiempo");
                });

            modelBuilder.Entity("STCA_DataLib.Model.ZonaHoraria", b =>
                {
                    b.Navigation("ZonaHoraria_RangoTiempo");
                });
#pragma warning restore 612, 618
        }
    }
}