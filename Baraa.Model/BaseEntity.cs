using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.Model
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
    }
}
