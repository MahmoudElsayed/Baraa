using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text;

namespace Baraa.Model
{
    /// <summary>
    /// Model For Companies Table
    /// </summary>
  public  class Company : BaseClass
    {
        public Company()
        {
            CompanyGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }

        public Guid CompanyGuid { get; set; }
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
        [StringLength(10)]

        public string PhoneNumber { get; set; }
        [StringLength(200)]

        public string Address { get; set; }
        [StringLength(50)]

        public string CompanyName { get; set; }

        [ForeignKey("City")]
        public Guid CityGuid { get; set; }
        public virtual City City { get; set; }

      

    }
}
