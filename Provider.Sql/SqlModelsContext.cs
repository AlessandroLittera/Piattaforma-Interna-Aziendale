using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Provider.Sql.SqlAccountTypes;
using Provider.Sql;


namespace Provider.Sql
{

    public class SqlModelsContext : DbContext
    {
        private static DbContextOptions CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlModelsContext>();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(@"Server=tcp:lillo-server.database.windows.net,1433;Initial Catalog=AzureDb;Persist Security Info=False;User ID=Lillo;Password=Oronzo123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return optionsBuilder.Options;
        }
        //private DbContextOptionsBuilder test = new DbContextOptionsBuilder<SqlModelsContext>(new DbContextOptions<SqlModelsContext>(""));

        public SqlModelsContext() : base(CreateDbContext())
        {
        }

        public SqlModelsContext(DbContextOptions<SqlModelsContext> options)
            : base(options)
        {

        }
        //table list with dbset
        public DbSet<SqlUser> SqlUsers { get; set; }
        public DbSet<SqlAssignement> SqlAssignements { get; set; }
        public DbSet<SqlAccount> SqlAccounts { get; set; }
       
        public DbSet<SqlRequest> SqlRequests { get; set; }
      
        public DbSet<SqlStandard> SqlStandards { get; set; }
       
        
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<SqlAssignement>(entity =>
            {
                entity
                .HasOne(x => x.SqlUser)
                .WithMany(x=>x.SqlAssignements);
            });
            modelBuilder.Entity<SqlAssignement>(entity =>
            {
                entity
                .HasOne(x => x.SqlAccount)
                .WithMany(x=>x.SqlAssignements);
            });
          
          //mettere quelli per le request


            base.OnModelCreating(modelBuilder);
        }
    }
}
