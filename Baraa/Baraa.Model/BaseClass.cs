using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Baraa.Model
{
   public class BaseClass
    {
        public BaseClass()
        {
            AddedTime = DateTime.UtcNow;

        }
        public DateTime AddedTime { get; set; }
        public DateTime HijriAddedTime { get; set; }

        [ForeignKey("User")]

        public string AddedUser { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public DateTime? HijriUpdatedTime { get; set; }

        [ForeignKey("UpdatedUser")]
        public string LastUpdatedUser { get; set; }
        
        public bool? IsDeleted { get; set; }
        public virtual IdentityUser User { get; set; }

        public virtual IdentityUser UpdatedUser { get; set; }
    }
}
