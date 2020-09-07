using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Baraa.Model
{
    public class Driver : BaseClass
    {
        public Driver()
        {
            DriverGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverID { get; set; }
        public Guid DriverGuid { get; set; }
        [StringLength(50)]
        [Required]
        public string FirstNameAr { get; set; }
        [StringLength(50)]
        [Required]

        public string FirstNameEn { get; set; }
        [StringLength(50)]
        [Required]

        public string LastNameAr { get; set; }
        [StringLength(50)]
        [Required]

        public string LastNameEn { get; set; }

        
        public string Email { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [StringLength(6)]
        public string Gender { get; set; }
        public DateTime? BirthOfDate { get; set; }
        public DateTime? HijriBirthOfDate { get; set; }
        [StringLength(10)]
        public string NationalID { get; set; }
        [StringLength(20)]
        public string PassportNumber { get; set; }
        [StringLength(20)]
        public string LicenseNumber { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public DateTime? HijriLicenseEndDate { get; set; }
        [StringLength(20)]
        public string InsuranceNumber { get; set; }
        public DateTime? InsuranceEndDate { get; set; }
        public DateTime? HijriInsuranceEndDate { get; set; }
        [StringLength(20)]
        public string FormNumber { get; set; }
        public DateTime? FormEndDate { get; set; }
        public DateTime? HijriFormEndDate { get; set; }
        [ForeignKey("City")]
        public int CityID { get; set; }

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
