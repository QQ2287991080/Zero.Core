using Microsoft.EntityFrameworkCore;
using System;
using Zero.Core.Domain.Entities;
using Zero.Core.EfCore.EntityConfigs;

namespace Zero.Core.EfCore
{
    public class EfCoreDbContext:DbContext
    {
        //readonly DbContextOptions<EfCoreDbContext> _options;
        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options):base(options)
        {
            //_options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = this.GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }


        public DbSet<User> Users{ get; set; }
        public DbSet<UserRole>  UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenu>  RoleMenus { get; set; }
        public DbSet<Dictionaries> Dictionaries { get; set; }
    }
}
