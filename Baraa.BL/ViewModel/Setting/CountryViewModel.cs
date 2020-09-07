using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraa.BLL.ViewModel.Setting
{
    public class CountryViewModel
    {
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Country Name Arabic Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessage = "You can enter numbers or arabic characters only")]
        [Display(Name = "Name Ar")]
        public string CountryNameAR { get; set; }
        [Required(ErrorMessage = "Country Name English Required")]
        [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessage = "You can enter numbers or english characters only")]
        [Display(Name = " Name En")]
        public string CountryNameEN { get; set; }
        [Display(Name = " Flag")]
        public string Flag { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
    }
}
