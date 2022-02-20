using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSUserPermission.Entities;
using WSUserPermission.Utils;
using EF = Microsoft.EntityFrameworkCore;
namespace WSUserPermission.Context
{
    public class DbContext : EF.DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionsTypes { get; set; }

        string connectionString = ConfigurationManager.AppSetting["ConnectionStrings:n5Permissions"];

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string env = connectionString;
            optionsBuilder.UseSqlServer(env);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
