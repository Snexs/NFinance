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
    [Migration("20210529142621_AddMotivoExclusao")]
    partial class AddMotivoExclusao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NFinance.Domain.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("LogoutToken")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("NFinance.Domain.Ganho", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataDoGanho")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NomeGanho")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Recorrente")
                        .HasColumnType("bit");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Ganho");
                });

            modelBuilder.Entity("NFinance.Domain.Gasto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataDoGasto")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MotivoExclusao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeGasto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("QuantidadeParcelas")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Gasto");
                });

            modelBuilder.Entity("NFinance.Domain.Investimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataAplicacao")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NomeInvestimento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(38)
                        .HasColumnType("DECIMAL(38)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("Investimento");
                });

            modelBuilder.Entity("NFinance.Domain.Resgate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataResgate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdInvestimento")
                        .HasColumnType("uniqueidentifier");

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
#pragma warning restore 612, 618
        }
    }
}
