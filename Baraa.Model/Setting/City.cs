using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Baraa.Model.Setting
{
    [Table("Setting.City")]
    public partial class City: BaseEntity
    {
        public City()
        {
            CityGuid = Guid.NewGuid();
            ZoneCity = new  HashSet<ZoneCity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CityID { get; set; }
        public Guid CityGuid { get; set; }
        public int CountryID { get; set; }

        public string Code { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }

        //------------------- ��� ����� -------------------  
        #region Map Data
        public string Place { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Zoom { get; set; }
        #endregion

        //--------------------------------------------------------
        public virtual Country Country { get; set; }
        public virtual ICollection<ZoneCity> ZoneCity { get; set; }
        public virtual User User { get; set; }


    }
}
