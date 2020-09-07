using Baraa.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.BLL.ViewModel.Setting
{
    public class AgentViewModel : BaseEntity
    {
        public int AgentID { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessage = "You can enter numbers or arabic characters only")]
        [Display(Name = "Name Ar")]
        public string AgentNameAR { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessage = "You can enter numbers or english characters only")]
        [Display(Name = "Name En")]
        public string AgentNameEN { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessage = "You can enter numbers or arabic characters only")]
        [Display(Name = "Address Ar")]
        public string AddressAr { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessage = "You can enter numbers or english characters only")]
        [Display(Name = "Address En")]
        public string AddressEn { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string FaxNumber { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessage = "You can enter numbers or english characters only")]
        [Display(Name = "Mobile")]
        public string FirstMobileNo { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [Display(Name = "Email")]
        public string FirstEamil { get; set; }
        public string SeconedEmail { get; set; }
        public string WebSite { get; set; }
        public string AgentManger { get; set; }
        public string SeconedMobileNo { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Zone")]
        public int? ZoneID { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "DeliveryPrice")]
        public decimal DeliveryPrice { get; set; }
        public string ZoneName { get; set; }
    }
}
