using Baraa.Model;
using Baraa.Model.Operator;
using Baraa.Model.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using System;
using System.Linq;
using System.Security.Policy;

namespace Baraa.DAL
{
    public class BaraaContext : IdentityDbContext
    {

        public BaraaContext(DbContextOptions<BaraaContext> option) :
            base(option)
        {


        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Setting
           // modelBuilder.Entity<Country>().HasMany(e => e.City).WithOne(e => e.Country).OnDelete(DeleteBehavior.Cascade).IsRequired(true).HasPrincipalKey(p => p.CountryID).IsRequired(false);
            //modelBuilder.Entity<Baraa.Model.Setting.Zone>().HasMany(e => e.ZoneCity).WithOne(e => e.Zone).OnDelete(DeleteBehavior.Cascade).IsRequired(true).HasPrincipalKey(p => p.ZoneID).IsRequired(false);


            #endregion
       
            #region Identity
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
            //modelBuilder.Entity<CustomUserRole>().ToTable("UserRole");
            //modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogin");
            //modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimId");
            //modelBuilder.Entity<CustomRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");
            #endregion

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

        }
        public virtual DbSet<User>  User { get; set; }


        #region Premessions
        public virtual DbSet<OperatorAgent>  OperatorAgents { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionControllerAction> PermissionControllerActions { get; set; }
        public virtual DbSet<PermissionAction> PermissionActions { get; set; }
        public virtual DbSet<PermissionController> PermissionControllers { get; set; }
        #endregion
        #region Setting
        public virtual DbSet<Agent>  Agents { get; set; }
        public virtual DbSet<Nationality>  Nationalities { get; set; }
        public virtual DbSet<Positions>  Positions { get; set; }

        public virtual DbSet<Country>  Countries { get; set; }
        public virtual DbSet<City>  Cities { get; set; }
        public virtual DbSet<Model.Setting.Zone>  Zones { get; set; }
        public virtual DbSet<ZoneCity>  ZoneCities { get; set; }



        #endregion

        //#region Pickup
        //public virtual DbSet<Driver>  Drivers { get; set; }
        //public virtual DbSet<Taxi>  Taxis { get; set; }
        //public virtual DbSet<DriverTaxi>  DriverTaxis { get; set; }


        //#endregion



    }
}