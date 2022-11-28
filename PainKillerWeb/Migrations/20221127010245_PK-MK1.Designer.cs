﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PainKillerWeb.Context;

namespace PainKillerWeb.Migrations
{
    [DbContext(typeof(PainKillerDbContext))]
    [Migration("20221127010245_PK-MK1")]
    partial class PKMK1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PainKillerWeb.Models.Main.Atributo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("atributos");
                });

            modelBuilder.Entity("PainKillerWeb.Models.Main.Personaje", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("energiaMax")
                        .HasColumnType("int");

                    b.Property<int>("expActual")
                        .HasColumnType("int");

                    b.Property<int>("expGastada")
                        .HasColumnType("int");

                    b.Property<int>("manaMax")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(21)")
                        .HasMaxLength(21);

                    b.Property<int>("razaId")
                        .HasColumnType("int");

                    b.Property<int>("vidaMax")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("razaId");

                    b.ToTable("personajes");
                });

            modelBuilder.Entity("PainKillerWeb.Models.Main.Raza", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("atributoPesimoid")
                        .HasColumnType("int");

                    b.Property<int?>("atributoRelevante2id")
                        .HasColumnType("int");

                    b.Property<int?>("atributoRelevanteid")
                        .HasColumnType("int");

                    b.Property<int>("idAtributoPesimo")
                        .HasColumnType("int");

                    b.Property<int>("idAtributoRelevante")
                        .HasColumnType("int");

                    b.Property<int>("idAtributoRelevante2")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("atributoPesimoid");

                    b.HasIndex("atributoRelevante2id");

                    b.HasIndex("atributoRelevanteid");

                    b.ToTable("raza");
                });

            modelBuilder.Entity("PainKillerWeb.Models.Main.Personaje", b =>
                {
                    b.HasOne("PainKillerWeb.Models.Main.Raza", "raza")
                        .WithMany()
                        .HasForeignKey("razaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PainKillerWeb.Models.Main.Raza", b =>
                {
                    b.HasOne("PainKillerWeb.Models.Main.Atributo", "atributoPesimo")
                        .WithMany()
                        .HasForeignKey("atributoPesimoid");

                    b.HasOne("PainKillerWeb.Models.Main.Atributo", "atributoRelevante2")
                        .WithMany()
                        .HasForeignKey("atributoRelevante2id");

                    b.HasOne("PainKillerWeb.Models.Main.Atributo", "atributoRelevante")
                        .WithMany()
                        .HasForeignKey("atributoRelevanteid");
                });
#pragma warning restore 612, 618
        }
    }
}
