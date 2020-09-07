using Baraa.Model.Operator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Baraa.Model.Setting
{
    [Table("Setting.Positions")]
    public  class Positions : BaseEntity
    {
        public Positions()
        {
            OperationUser = new HashSet<OperatorUser>();
            PositionGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PositionID { get; set; }

        public Guid PositionGuid { get; set; }

        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [Display(Name = "Name Ar")]
        public string NameAR { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [Display(Name = "Name En")]
        public string NameEN { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OperatorUser> OperationUser { get; set; }
    }
}
