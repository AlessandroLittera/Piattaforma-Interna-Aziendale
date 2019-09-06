﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Provider.Sql;

namespace Provider.Sql.Migrations
{
    [DbContext(typeof(SqlModelsContext))]
    partial class SqlModelsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Provider.Sql.SqlAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<bool>("IsDefault");

                    b.Property<DateTime>("LastEdit");

                    b.Property<string>("Nickname")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("SqlAccounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SqlAccount");
                });

            modelBuilder.Entity("Provider.Sql.SqlAssignement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime>("LastEdit");

                    b.Property<int>("SqlAccountId");

                    b.Property<int>("SqlUserId");

                    b.HasKey("Id");

                    b.HasIndex("SqlAccountId");

                    b.HasIndex("SqlUserId");

                    b.ToTable("SqlAssignements");
                });

            modelBuilder.Entity("Provider.Sql.SqlRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<DateTime>("LastEdit");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SqlRequests");
                });

            modelBuilder.Entity("Provider.Sql.SqlRequestAssignement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("From");

                    b.Property<bool>("IsValid");

                    b.Property<string>("Note");

                    b.Property<int?>("SqlAccountId");

                    b.Property<int?>("SqlRequestId");

                    b.Property<DateTime>("To");

                    b.HasKey("Id");

                    b.HasIndex("SqlAccountId");

                    b.HasIndex("SqlRequestId");

                    b.ToTable("SqlRequestAssignements");
                });

            modelBuilder.Entity("Provider.Sql.SqlUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("Image");

                    b.Property<DateTime>("LastEdit");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("SqlUsers");
                });

            modelBuilder.Entity("Provider.Sql.SqlVeicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SqlVeicles");
                });

            modelBuilder.Entity("Provider.Sql.SqlVeicleAssignement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("From");

                    b.Property<bool>("IsValid");

                    b.Property<string>("Note");

                    b.Property<int?>("SqlAccountId");

                    b.Property<int?>("SqlVeicleId");

                    b.Property<DateTime>("To");

                    b.HasKey("Id");

                    b.HasIndex("SqlAccountId");

                    b.HasIndex("SqlVeicleId");

                    b.ToTable("SqlVeicleAssignements");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountTypes.SqlAdmin", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccount");

                    b.HasDiscriminator().HasValue("SqlAdmin");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountTypes.SqlStandard", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccount");

                    b.HasDiscriminator().HasValue("SqlStandard");
                });

            modelBuilder.Entity("Provider.Sql.SqlAssignement", b =>
                {
                    b.HasOne("Provider.Sql.SqlAccount", "SqlAccount")
                        .WithMany("SqlAssignements")
                        .HasForeignKey("SqlAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Provider.Sql.SqlUser", "SqlUser")
                        .WithMany("SqlAssignements")
                        .HasForeignKey("SqlUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Provider.Sql.SqlRequestAssignement", b =>
                {
                    b.HasOne("Provider.Sql.SqlAccount", "SqlAccount")
                        .WithMany("SqlRequestAssignements")
                        .HasForeignKey("SqlAccountId");

                    b.HasOne("Provider.Sql.SqlRequest", "SqlRequest")
                        .WithMany("SqlRequestAssignements")
                        .HasForeignKey("SqlRequestId");
                });

            modelBuilder.Entity("Provider.Sql.SqlVeicleAssignement", b =>
                {
                    b.HasOne("Provider.Sql.SqlAccount", "SqlAccount")
                        .WithMany("SqlVeicleAssignements")
                        .HasForeignKey("SqlAccountId");

                    b.HasOne("Provider.Sql.SqlVeicle", "SqlVeicle")
                        .WithMany("SqlVeiclesAssignements")
                        .HasForeignKey("SqlVeicleId");
                });
#pragma warning restore 612, 618
        }
    }
}
