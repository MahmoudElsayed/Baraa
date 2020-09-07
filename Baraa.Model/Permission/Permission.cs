 using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks.Sources;

namespace Baraa.Model
{
    [Table("Permission.Permission")]
    public partial class Permission   
    {

        public Permission()
        {
            PermissionGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int PermissionID { get; set; }

        public Guid PermissionGuid { get; set; }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public int PermissionControllerActionID { get; set; }

        public virtual PermissionControllerAction PermissionControllerActions { get; set; }

        public virtual CustomRole Role { get; set; }

        public virtual User User { get; set; }
    }
}
