using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Baraa.Model
{
  public  class DriverTaxi:BaseClass
    {
        public DriverTaxi()
        {
            DriverTaxiGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverTaxiID { get; set; }
        public Guid DriverTaxiGuid { get; set; }
        [ForeignKey("Company")]
        public int? CompanyID { get; set; }
        [ForeignKey("Driver")]
        public int DriverID { get; set; }
        [ForeignKey("Taxi")]
        public int TaxiID { get; set; }
        [ForeignKey("City")]
        public int CityID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime HijiriFromDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime HijiriEndDate { get; set; }


        public virtual Driver Driver  { get; set; }
        public virtual Taxi Taxi  { get; set; }


        public virtual Company Company { get; set; }

        public virtual City City { get; set; }

    }
}
