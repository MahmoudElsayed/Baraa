using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using System;
using System.Linq;

namespace Baraa.DAL
{
    public class AppContext : IdentityDbContext
    {
      
        public AppContext(DbContextOptions<AppContext> option) :
            base(option)
        {


        }
        

        #region Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            base.OnModelCreating(modelBuilder);
             //modelBuilder.Seed();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
              optionsBuilder.EnableSensitiveDataLogging();

        }

        #endregion

        #region Entitites

      

        #endregion

    }
}