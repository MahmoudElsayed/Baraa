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
    
    [Table("Setting.Company")]
  public  class Company : BaseEntity
    {
        public Company()
        {
            CompanyGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }
        public int CityID { get; set; }

        public Guid CompanyGuid { get; set; }

        [StringLength(50)]
        [Required]
        public string NameAr { get; set; }

        [StringLength(50)]
        [Required]
        public string NameEn { get; set; }
        [StringLength(50)]
        public string CompanyName { get; set; }


        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }


        public virtual City City { get; set; }

      

    }
}
