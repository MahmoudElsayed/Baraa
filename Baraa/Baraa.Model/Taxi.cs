﻿using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Baraa.Model
{
   public class Taxi :BaseClass
    {
        public Taxi()
        {
            TaxiGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaxiID { get; set; }
        public Guid TaxiGuid { get; set; }
        [StringLength(10)]
        [Required]
        public string TaxiNumber { get; set; }
        public string OwnerName { get; set; }
        [ForeignKey("Company")]
        public int? CompanyID { get; set; }
        public int? ModelID { get; set; }
        public int? IndustryID { get; set; }
        public int? ManufacturingYearID { get; set; }

        public int? ColorID { get; set; }

        public DateTime? MotorExpirationDate { get; set; }
        public DateTime? HijiriMotorExpirationDate { get; set; }


        public int? InsuranceCount { get; set; }
        public DateTime? InsuranceEndDate { get; set; }
        public DateTime? HijiriInsuranceEndDate { get; set; }

        public string FormNumber { get; set; }
        public DateTime? FormEndDate { get; set; }
        public DateTime? HijiriFormEndDate { get; set; }

        public int? CarSpeed { get; set; }
        public int? LowestCarSpeed { get; set; }
        public int? MaximumLoad { get; set; }
        [ForeignKey("City")]
        public int CityID { get; set; }
        [StringLength(20)]
        public string CarPicture { get; set; }




        public virtual Company Company { get; set; }

        public virtual City City { get; set; }




    }
}
