using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baraa.DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppContext>
    {

        public AppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=BaraaDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AppContext(optionsBuilder.Options);
        }
    }
}
