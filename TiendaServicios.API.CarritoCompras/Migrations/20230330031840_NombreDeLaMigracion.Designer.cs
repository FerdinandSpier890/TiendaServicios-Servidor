﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaServicios.API.CarritoCompras.Persistencia;

namespace TiendaServicios.API.CarritoCompras.Migrations
{
    [DbContext(typeof(CarritoContexto))]
    [Migration("20230330031840_NombreDeLaMigracion")]
    partial class NombreDeLaMigracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TiendaServicios.API.CarritoCompras.Modelo.CarritoSesion", b =>
                {
                    b.Property<int>("CarritoSesionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarritoSesionId");

                    b.ToTable("CarritoSesion");
                });

            modelBuilder.Entity("TiendaServicios.API.CarritoCompras.Modelo.CarritoSesionDetalle", b =>
                {
                    b.Property<int>("CarritoSesionDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CarritoSesionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductoSeleccionado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarritoSesionDetalleId");

                    b.HasIndex("CarritoSesionId");

                    b.ToTable("CarritoSesionDetalle");
                });

            modelBuilder.Entity("TiendaServicios.API.CarritoCompras.Modelo.CarritoSesionDetalle", b =>
                {
                    b.HasOne("TiendaServicios.API.CarritoCompras.Modelo.CarritoSesion", "CarritoSesion")
                        .WithMany("ListaDetalle")
                        .HasForeignKey("CarritoSesionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarritoSesion");
                });

            modelBuilder.Entity("TiendaServicios.API.CarritoCompras.Modelo.CarritoSesion", b =>
                {
                    b.Navigation("ListaDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}
