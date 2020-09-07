using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Baraa.Model
{
    [Table("Permission.PermissionAction")]
    public partial class PermissionAction
    {
        public PermissionAction()
        {
            PermissionControllerActions = new HashSet<PermissionControllerAction>();
            PermissionActionGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionActionID { get; set; }

        public Guid PermissionActionGuid { get; set; }


        [StringLength(50)]
        public string PermissionActionNameAr { get; set; }

        [StringLength(50)]
        public string PermissionActionNameEn { get; set; }

        public virtual ICollection<PermissionControllerAction> PermissionControllerActions { get; set; }
    }
}
