using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.BLL.ViewModel
{
    public class UserRoleViewModel
    {
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Display(Name = "Employee")]
        public Guid? EmpID { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role Required")]
        public string RoleName { get; set; }
        public string UserNameEN { get; set; }
        public string UserNameAR { get; set; }
    }
}
