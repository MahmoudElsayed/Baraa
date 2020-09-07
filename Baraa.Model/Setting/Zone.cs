using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.Model.Setting
{
    [Table("Setting.Zone")]
    public class Zone : BaseEntity
    {
        public Zone()
        {
            ZoneGuid = Guid.NewGuid();
            ZoneCity = new HashSet<ZoneCity>();
            Agent = new HashSet<Agent>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ZoneID { get; set; }
        public Guid ZoneGuid { get; set; }

        [StringLength(75 )]
        public string NameAr { get; set; }

        [StringLength(75)]
        public string NameEn { get; set; }

        public virtual ICollection<ZoneCity> ZoneCity { get; set; }
        public virtual ICollection<Agent> Agent { get; set; }
        public virtual User User { get; set; }

    }
}
