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
    public class Zone : BaseClass
    {
        public Zone()
        {
            ZoneGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ZoneID { get; set; }
        public Guid ZoneGuid { get; set; }
        [StringLength(75 )]
        public string ZoneNameAr { get; set; }
        [StringLength(75)]
        public string ZoneNameEn { get; set; }

        public virtual ICollection<ZoneCity> ZoneCity { get; set; }
        public virtual ICollection<Agent> Agent { get; set; }
    }
}
