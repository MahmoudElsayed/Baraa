using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Baraa.Model.Setting
{
    [Table("Setting.Country")]
    public class Country: BaseEntity
    {
        public Country()
        {
            CountryGuid = Guid.NewGuid();
            City = new HashSet<City>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }
        public Guid CountryGuid { get; set; }

        [StringLength(75)]
        public string NameAR { get; set; }

        [StringLength(75)]
        public string NameEN { get; set; }
        public string Extension { get; set; }
        public string Flag { get; set; }
        #region Map Data
        public string Place { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Zoom { get; set; }
        #endregion
        public virtual ICollection<City> City { get; set; }
        public virtual User User { get; set; }

    }
}
