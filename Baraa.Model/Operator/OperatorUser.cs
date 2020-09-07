using Baraa.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Baraa.Model.Operator
{
    [Table("Operator.OperatorUser")]
    public class OperatorUser : BaseEntity
    {
        public OperatorUser()
        {
            OperatorGUID = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperatorID { get; set; } // pk
        public Guid? OperatorGUID { get; set; }

        [Required(ErrorMessage = "Nationality ID Is Required")]
        [Display(Name = "Nationality ID")]
        public int NationalityID { get; set; } // fk

        [Required(ErrorMessage = "Position Is Required")]
        [Display(Name = "Position")]
        public int PositionID { get; set; } // fk

        [Required(ErrorMessage = "CityIs Required")]
        [Display(Name = "City")]
        public int CityID { get; set; } // fk


        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [Display(Name = "Name Ar")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "Name En Is Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression("^[a-zA-Z0-9 _.-]*$", ErrorMessage = "Special Chars Not Allowed")]
        [Display(Name = "Name En")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "Mobile No Is Required")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Min Length Allowed 9 Number")]
        [RegularExpression(@"^[5][0-9 +-]+$", ErrorMessage = "You can enter numbers only And Start With 5")]
        [Display(Name = "MobileNo")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please Enter File Number")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "6 Number at Least")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "File Number Accepted only numbers")]
        [Display(Name = "File Number")]
        public string FileNo { get; set; }

        [Required(ErrorMessage = "Please Enter ID Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Must be 10 digits")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "ID Number Accepted only numbers")]
        [Display(Name = "ID Number")]
        public string IDNo { get; set; }

        [Required(ErrorMessage = "Address1 Is Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression("^[a-zA-Z0-9 _.-]*$", ErrorMessage = "Special Chars Not Allowed")]
        [Display(Name = "Main Address")]
        public string MainAddress { get; set; }

        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "Special Chars Not Allowed")]
        [Display(Name = "Alternative Address")]
        public string AlternativeAddress { get; set; }
        [Required(ErrorMessage = "Email Is Required")]

        [StringLength(50, ErrorMessage = "The maximum allowed is 50 characters")]
        [Display(Name = "E-mail")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gender Is Required")]
        [Display(Name = "gender")]
        public byte Gender { get; set; } // enum Gender
        [Display(Name = "Expiry Date ID Type")]
        public byte? ExpiryDateIDType { get; set; } // enum DateTypeEnum

        [Display(Name = "Expiry Date ID")]
        public string ExpiryDateID { get; set; }
        [Display(Name = "Date Start Work")]
        public DateTime DateStartWork { get; set; }
        [Display(Name = "ID Image")]
        public string IDImage { get; set; }
        [Display(Name = "Personal Image")]
        public string PersonalImage { get; set; }
        [Display(Name = "IsEnable")]
        public byte IsEnable { get; set; } // enum StatusEnable

        public virtual User User { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Positions Positions { get; set; }
        public virtual City City { get; set; }
        public bool IsMobileActivated { get; set; }
        public bool? IsEmailActivated { get; set; }
        public string ActivationCode { get; set; }
        public virtual ICollection<OperatorAgent> OperatorAgent { get; set; }
    }
}
