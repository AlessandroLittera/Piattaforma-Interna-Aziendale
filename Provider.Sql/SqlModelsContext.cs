using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Provider.Sql.SqlAccountTypes;
using Provider.Sql.SqlContextes;


namespace Provider.Sql
{

    public class SqlModelsContext : DbContext
    {
        private static DbContextOptions CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlModelsContext>();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(@"data source=srv-db-dev;initial catalog=Test42;persist security info=True;user id=Test42User;password=Test42Password;");
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
        public DbSet<SqlRole> SqlRoles { get; set; }
        public DbSet<SqlPossibleRole> SqlPossibleRoles { get; set; }
        public DbSet<SqlContext> SqlContexts { get; set; }
        public DbSet<SqlApplication> SqlApplications { get; set; }
        public DbSet<SqlTechnology> SqlTechnologies { get; set; }
        public DbSet<SqlArea> SqlAreas { get; set; }
        public DbSet<SqlDPO> SqlDPOs { get; set; }
        public DbSet<SqlRSGSI> SqlRSGSIs { get; set; }
        public DbSet<SqlStakeHolder> SqlStakeHolders { get; set; }
        public DbSet<SqlStandard> SqlStandards { get; set; }
        public DbSet<SqlGod> SqlGods { get; set; }
        
      
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
            modelBuilder.Entity<SqlRole>(entity => {
                entity.HasOne(x => x.SqlAccount)
                        .WithMany(x => x.SqlRoles);
                entity.HasOne(x => x.SqlContext)
                       .WithMany(x => x.Roles);
                entity.HasOne(x => x.SqlPossibleRole)
                       .WithMany(x => x.SqlRoles);
            });
            modelBuilder.Entity<SqlContextes.SqlArea>(entity => {
                entity.HasMany(x => x.SqlApplications).WithOne(x=> x.SqlArea);
            });
            modelBuilder.Entity<SqlContextes.SqlTechnology>(entity => {
                entity.HasMany(x => x.SqlApplications).WithOne(x => x.SqlTechnology);
 
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
