using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Baraa.Model.Setting
{
    [Table("Setup.City")]
    public partial class City:BaseClass
    {
        public City()
        {
            CityGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CityID { get; set; }
        public Guid CityGuid { get; set; }
        public string Code { get; set; }
        [StringLength(75)]
        public string CityNameAR { get; set; }
        [StringLength(75)]
        public string CityNameEN { get; set; }
        public int CountryID { get; set; }
        public bool? IsCovered { get; set; }

        //------------------- ��� ����� -------------------  

        //--------------------------------------------------------
        #region Map Data
        public string place_id { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Zoom { get; set; }

        public string bounds_northeast_Lat { get; set; }
        public string bounds_northeast_Long { get; set; }
        public string bounds_southwest_Lat { get; set; }
        public string bounds_southwest_Long { get; set; }


        public string viewport_northeast_Lat { get; set; }
        public string viewport_northeast_Long { get; set; }
        public string viewport_southwest_Lat { get; set; }
        public string viewport_southwest_Long { get; set; }
        #endregion


        public virtual Country Country { get; set; }
        public virtual ICollection<ZoneCity> ZoneCity { get; set; }
        
    }
}
