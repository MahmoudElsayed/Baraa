using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Baraa.Model
{
    [Table("Pickup.DriverTaxi")]

    public class DriverTaxi:BaseEntity
    {
        public DriverTaxi()
        {
            DriverTaxiGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverTaxiID { get; set; }
        public int CityID { get; set; }
        public int? CompanyID { get; set; }
        public int DriverID { get; set; }
        public int TaxiID { get; set; }
        public Guid DriverTaxiGuid { get; set; }
     
        public DateTime FromDate { get; set; }

        [StringLength(10)]
        public string HijiriFromDate { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(10)]
        public string HijiriEndDate { get; set; }
        public virtual Driver Driver  { get; set; }
        public virtual Taxi Taxi  { get; set; }
        public virtual Company Company { get; set; }
        public virtual City City { get; set; }

    }
}
