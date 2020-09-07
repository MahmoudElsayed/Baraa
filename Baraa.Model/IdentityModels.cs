using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Baraa.Model.Setting;
using Microsoft.AspNetCore.Identity;

namespace Baraa.Model
{
    public class User : IdentityUser
        //<int ,CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public User()
        {
            Country = new HashSet<Country>();
            City = new HashSet<City>();
        }
        public int UserType { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Country> Country { get; set; }
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int>)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

    }

    public class CustomRole : IdentityRole
        //<CustomUserRole>
    {
        public CustomRole()
        {
            Permissions = new HashSet<Permission>();
        }

        public virtual ICollection<Permission> Permissions { get; set; }
    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }
}