using System;
using System.Linq;

namespace Baraa.BLL.ViewModel
{
    public class UserPermission
    {
         public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Confirm { get; set; }
        public bool Approve { get; set; }
        public bool Enable { get; set; }
        public bool Disable { get; set; }
    }
}
