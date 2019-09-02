﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Provider.Sql;

namespace Provider.Sql.Migrations
{
    [DbContext(typeof(SqlModelsContext))]
    [Migration("20190509154525_firstMigration")]
    partial class firstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Provider.Sql.SqlAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nickname");

                    b.Property<string>("Password");

                    b.Property<int?>("SqlUserId");

                    b.HasKey("Id");

                    b.HasIndex("SqlUserId");

                    b.ToTable("SqlAccounts");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountType.SqlAccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsMailingList");

                    b.HasKey("Id");

                    b.ToTable("SqlAccountTypes");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SqlAccountType");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SqlContexts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SqlRequest");
                });

            modelBuilder.Entity("Provider.Sql.SqlPossibleRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("SqlContextId");

                    b.HasKey("Id");

                    b.HasIndex("SqlContextId");

                    b.ToTable("SqlPossibleRoles");
                });

            modelBuilder.Entity("Provider.Sql.SqlRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("From");

                    b.Property<int?>("SqlAccountId");

                    b.Property<int?>("SqlContextId");

                    b.Property<int?>("SqlPossibleRoleId");

                    b.Property<DateTime>("To");

                    b.HasKey("Id");

                    b.HasIndex("SqlAccountId");

                    b.HasIndex("SqlContextId");

                    b.HasIndex("SqlPossibleRoleId");

                    b.ToTable("SqlRoles");
                });

            modelBuilder.Entity("Provider.Sql.SqlUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("SqlUsers");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountType.SqlDPO", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccountType.SqlAccountType");

                    b.HasDiscriminator().HasValue("SqlDPO");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountType.SqlRSGSI", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccountType.SqlAccountType");

                    b.HasDiscriminator().HasValue("SqlRSGSI");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountType.SqlStakeHolder", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccountType.SqlAccountType");

                    b.HasDiscriminator().HasValue("SqlStakeHolder");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountType.SqlStandard", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccountType.SqlAccountType");

                    b.HasDiscriminator().HasValue("SqlStandard");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlApplication", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlContextes.SqlRequest");

                    b.Property<int?>("SqlAreaId");

                    b.Property<int?>("SqlTechnologyId");

                    b.HasIndex("SqlAreaId");

                    b.HasIndex("SqlTechnologyId");

                    b.HasDiscriminator().HasValue("SqlApplication");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlArea", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlContextes.SqlRequest");

                    b.HasDiscriminator().HasValue("SqlArea");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlTechnology", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlContextes.SqlRequest");

                    b.HasDiscriminator().HasValue("SqlTechnology");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccount", b =>
                {
                    b.HasOne("Provider.Sql.SqlUser", "SqlUser")
                        .WithMany("SqlAccounts")
                        .HasForeignKey("SqlUserId");
                });

            modelBuilder.Entity("Provider.Sql.SqlPossibleRole", b =>
                {
                    b.HasOne("Provider.Sql.SqlContextes.SqlRequest", "SqlRequest")
                        .WithMany("SqlPossibleRoles")
                        .HasForeignKey("SqlContextId");
                });

            modelBuilder.Entity("Provider.Sql.SqlRole", b =>
                {
                    b.HasOne("Provider.Sql.SqlAccount", "SqlAccount")
                        .WithMany("SqlRoles")
                        .HasForeignKey("SqlAccountId");

                    b.HasOne("Provider.Sql.SqlContextes.SqlRequest", "SqlRequest")
                        .WithMany("SqlRoles")
                        .HasForeignKey("SqlContextId");

                    b.HasOne("Provider.Sql.SqlPossibleRole", "SqlPossibleRole")
                        .WithMany("SqlRoles")
                        .HasForeignKey("SqlPossibleRoleId");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlApplication", b =>
                {
                    b.HasOne("Provider.Sql.SqlContextes.SqlArea", "SqlArea")
                        .WithMany("SqlApplications")
                        .HasForeignKey("SqlAreaId");

                    b.HasOne("Provider.Sql.SqlContextes.SqlTechnology", "SqlTechnology")
                        .WithMany("SqlApplications")
                        .HasForeignKey("SqlTechnologyId");
                });
#pragma warning restore 612, 618
        }
    }
}
