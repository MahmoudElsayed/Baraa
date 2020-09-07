using Baraa.Model.Operator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using System.Text;

namespace Baraa.Model.Setting
{
    [Table("Setting.Nationality")]
    public  class Nationality : BaseEntity
    {
        public Nationality()
        {
            OperationUser = new HashSet<OperatorUser>();
            NationalityGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NationalityID { get; set; }

        public Guid NationalityGuid { get; set; }

        [StringLength(50, ErrorMessage = "The maximum allowed is 50 characters ")]
        [Display(Name = "Name AR")]
        public string NameAR { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "The maximum allowed is 50 characters ")]
        [Display(Name = "Name EN")]
        public string NameEN { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OperatorUser> OperationUser { get; set; }


    }
}
