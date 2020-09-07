using Baraa.Model.Operator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.Model.Setting
{
    [Table("Operator.Agent")]
    public partial class Agent : BaseEntity
    {
        public Agent()
        {
            OperatorAgent = new HashSet<OperatorAgent>();
            AgentGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentID { get; set; }

        public int CityID { get; set; }

        public Guid AgentGuid { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [Display(Name = "Name Ar")]
        public string NameAR { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [Display(Name = " Name En")]
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Fax { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string AgentManger { get; set; }
        public virtual User User { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<OperatorAgent> OperatorAgent { get; set; }
    }
}
