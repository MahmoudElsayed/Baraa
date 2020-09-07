using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Baraa.Model
{
    [Table("Pickup.Driver")]
    public class Driver : BaseEntity
    {
        public Driver()
        {
            DriverGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverID { get; set; }
        public int CityID { get; set; }

        public Guid DriverGuid { get; set; }

        [StringLength(50)]
        [Required]
        public string NameAr { get; set; }

        [StringLength(50)]
        [Required]
        public string NameEn { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

       
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(6)]
        public string Gender { get; set; }

        public DateTime? BirthOfDate { get; set; }

        [StringLength(10)]
        public string HijriBirthOfDate { get; set; }

        [StringLength(10)]
        public string NationalID { get; set; }

        [StringLength(20)]
        public string PassportNumber { get; set; }

        [StringLength(20)]
        public string LicenseNumber { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        [StringLength(10)]
        public string HijriLicenseEndDate { get; set; }

        [StringLength(20)]
        public string InsuranceNumber { get; set; }
        public DateTime? InsuranceEndDate { get; set; }

        [StringLength(10)]
        public string HijriInsuranceEndDate { get; set; }

        [StringLength(20)]
        public string FormNumber { get; set; }
        public DateTime? FormEndDate { get; set; }
        [StringLength(10)]
        public string HijriFormEndDate { get; set; }

        public int? DailyDeliveriesCount { get; set; }

        [StringLength(20)]
        public string PersonalPicture { get; set; }

        [StringLength(20)]
        public string LicensePicture { get; set; }

        [StringLength(20)]
        public string NationalIDPicture { get; set; }

        [StringLength(20)]
        public string PassportPicture { get; set; }

    

        [StringLength(20)]
        public string FormPicture { get; set; }

        [StringLength(20)]
        public string CV_Path { get; set; }

        public virtual City City {get;set;}

    }
}
