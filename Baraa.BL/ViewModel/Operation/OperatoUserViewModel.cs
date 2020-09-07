using Baraa.Model.Operator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.BLL.ViewModel.Operation
{
  
    public class OperatoUserViewModel : OperatorUser
    {
        [StringLength(100, ErrorMessage = "Max Length Allowed 100 char")]
        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "Special Chars Not Allowed")]
        [Display(Name = "Position")]
        public string PositionTypeName { get; set; }
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [Display(Name = "Block Name English")]
        public string BlockNameEN { get; set; }

        [StringLength(50, ErrorMessage = "The maximum allowed is 50 characters ")]
        [Display(Name = "Nationality Name")]
        public string NationalityName { get; set; }
        public string RoleList { get; set; }

        [Display(Name = "ID Expiry Date")]
        public DateTime OUserIDExpiryDateChrist { get; set; }

        [Display(Name = "Passport Expiry Date")]
        public DateTime OUserPassportExpiryDateChrist { get; set; }
        //public StatusEnable OUserStatus { get; set; } // enum StatusEnable

        [Display(Name = "Roles")]
        public List<string> RolesID { get; set; }  
        [Display(Name = "City")]
        public int CityID { get; set; }
        [Display(Name = "Country")]
        public int CountryID { get; set; }
        public string OUserStatus_Str { get; set; }
        public string CityNameEN { get; set; }
        public string CountryNameEN { get; set; }
        public string CityNameAR { get; set; }
    }
}
