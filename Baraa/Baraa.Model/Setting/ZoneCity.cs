using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.Model.Setting
{
    [Table("Setting.ZoneCity")]
    public  class ZoneCity : BaseClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ZoneCityID { get; set; }
        public int ZoneID { get; set; }
        public int CityID { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual City City { get; set; }
    }
}
