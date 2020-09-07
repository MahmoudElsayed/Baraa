using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Baraa.Model
{
    [Table("Permission.PermissionControllerAction")]
    public partial class PermissionControllerAction
    {
        public PermissionControllerAction()
        {
            Permissions = new HashSet<Permission>();
            PermissionControllerActionGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int PermissionControllerActionID { get; set; }
        public Guid PermissionControllerActionGuid { get; set; }

        public int PermissionControllerID { get; set; }

        public int PermissionActionID { get; set; }

        public virtual PermissionController PermissionControllers { get; set; }

        public virtual PermissionAction PermissionActions { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
