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
    [Table("Setting.Agent")]
    public partial class Agent : BaseClass
    {
        public Agent()
        {
            AgentGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentID { get; set; }
        public Guid AgentGuid { get; set; }
        [StringLength(75)]
        public string AgentNameAR { get; set; }
        [StringLength(75)]
        public string AgentNameEN { get; set; }
        public string AddressAr { get; set; }
        public string AddressEn { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Fax { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string AgentManger { get; set; }
        public int CityID { get; set; }
        public virtual City  City { get; set; }


    }
}
