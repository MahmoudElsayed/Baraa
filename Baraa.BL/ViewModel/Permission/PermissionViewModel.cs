using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Baraa.BLL.ViewModel
{
    public class PermissionViewModel
    {
        [Required(ErrorMessage = "مطلوب")]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "مطلوب")]
        public int UserId { get; set; }

        public string CheckedItems { get; set; }

        public List<PermissionControllerViewModel> Controllers { get; set; }
    }

    public class PermissionControllerViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<PermissionActionViewModel> Actions { get; set; }
    }

    public class PermissionActionViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool Allow { get; set; }
    }
}
