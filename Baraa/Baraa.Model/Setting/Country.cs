using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Baraa.Model.Setting
{
    [Table("Setup.Country")]
    public partial class Country:BaseClass
    {
        public Country()
        {
            CountryGuid = Guid.NewGuid();
        }

        public int CountryID { get; set; }
        public Guid CountryGuid { get; set; }


        [StringLength(75)]
        public string CountryNameAR { get; set; }
        [StringLength(75)]
        public string CountryNameEN { get; set; }
        public string Extension { get; set; }
        public string Flag { get; set; }
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



        public bool IsDeleted { get; set; }
        public virtual ICollection<City> City { get; set; }
    }
}
