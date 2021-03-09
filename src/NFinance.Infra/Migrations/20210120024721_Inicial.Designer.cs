﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NFinance.Infra;

namespace NFinance.Infra.Migrations
{
    [DbContext(typeof(BaseDadosContext))]
    [Migration("20210120024721_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("NFinance.Domain.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("RendaMensal")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("NFinance.Domain.Gastos", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataDoGasto")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("QuantidadeParcelas")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Gastos");
                });

            modelBuilder.Entity("NFinance.Domain.Investimentos", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataAplicacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Investimentos");
                });

            modelBuilder.Entity("NFinance.Domain.PainelDeControle", b =>
                {
                    b.Property<Guid>("IdPainelDeControle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("GastosAnual")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("GastosMensal")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("SaldoAnual")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("SaldoMensal")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("ValorInvestidoAnual")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("ValorInvestidoMensal")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("ValorRecebidoAnual")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.HasKey("IdPainelDeControle");

                    b.HasIndex("Id");

                    b.ToTable("PainelDeControle");
                });

            modelBuilder.Entity("NFinance.Domain.Resgate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataResgate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotivoResgate")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Resgate");
                });

            modelBuilder.Entity("NFinance.Domain.Gastos", b =>
                {
                    b.HasOne("NFinance.Domain.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("NFinance.Domain.Investimentos", b =>
                {
                    b.HasOne("NFinance.Domain.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("NFinance.Domain.PainelDeControle", b =>
                {
                    b.HasOne("NFinance.Domain.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("NFinance.Domain.Resgate", b =>
                {
                    b.HasOne("NFinance.Domain.Investimentos", "IdInvestimento")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdInvestimento");
                });
#pragma warning restore 612, 618
        }
    }
}
