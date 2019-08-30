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
    [Migration("20190517154754_edit user and account tables")]
    partial class edituserandaccounttables
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

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<DateTime>("From");

                    b.Property<bool>("IsMailingList");

                    b.Property<string>("Nickname");

                    b.Property<int?>("SqlUserId");

                    b.Property<DateTime>("To");

                    b.HasKey("Id");

                    b.HasIndex("SqlUserId");

                    b.ToTable("SqlAccounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SqlAccount");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlContext", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SqlContexts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SqlContext");
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

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("SqlUsers");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountTypes.SqlDPO", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccount");

                    b.HasDiscriminator().HasValue("SqlDPO");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountTypes.SqlRSGSI", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccount");

                    b.HasDiscriminator().HasValue("SqlRSGSI");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountTypes.SqlStakeHolder", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccount");

                    b.HasDiscriminator().HasValue("SqlStakeHolder");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccountTypes.SqlStandard", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlAccount");

                    b.HasDiscriminator().HasValue("SqlStandard");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlApplication", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlContextes.SqlContext");

                    b.Property<int?>("SqlAreaId");

                    b.Property<int?>("SqlTechnologyId");

                    b.HasIndex("SqlAreaId");

                    b.HasIndex("SqlTechnologyId");

                    b.HasDiscriminator().HasValue("SqlApplication");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlArea", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlContextes.SqlContext");

                    b.HasDiscriminator().HasValue("SqlArea");
                });

            modelBuilder.Entity("Provider.Sql.SqlContextes.SqlTechnology", b =>
                {
                    b.HasBaseType("Provider.Sql.SqlContextes.SqlContext");

                    b.HasDiscriminator().HasValue("SqlTechnology");
                });

            modelBuilder.Entity("Provider.Sql.SqlAccount", b =>
                {
                    b.HasOne("Provider.Sql.SqlUser", "SqlUser")
                        .WithMany("Accounts")
                        .HasForeignKey("SqlUserId");
                });

            modelBuilder.Entity("Provider.Sql.SqlPossibleRole", b =>
                {
                    b.HasOne("Provider.Sql.SqlContextes.SqlContext", "SqlContext")
                        .WithMany("SqlPossibleRoles")
                        .HasForeignKey("SqlContextId");
                });

            modelBuilder.Entity("Provider.Sql.SqlRole", b =>
                {
                    b.HasOne("Provider.Sql.SqlAccount", "SqlAccount")
                        .WithMany("SqlRoles")
                        .HasForeignKey("SqlAccountId");

                    b.HasOne("Provider.Sql.SqlContextes.SqlContext", "SqlContext")
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
