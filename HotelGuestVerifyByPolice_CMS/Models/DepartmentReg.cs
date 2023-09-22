using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class DepartmentReg
    {
       
        [Required(ErrorMessage = "Username is required.")]
        public string? userId { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? cPassword { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string? mobile { get; set; }
        [EmailAddress]
        [RegularExpression("^[_A-Za-z'`+-.]+([_A-Za-z0-9'+-.]+)*@([A-Za-z0-9-])+(\\.[A-Za-z0-9]+)*(\\.([A-Za-z]*){3,})$", ErrorMessage = "Enter proper email")]
        [Required(ErrorMessage = "Email Address is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format not proper of email address")]
        public string? email { get; set; }

        [Required(ErrorMessage = "Please Select Department Level.")]
        public int? userType { get; set; }

        [Required(ErrorMessage = "Please Select State.")]
        public int? stateId { get; set; }
        public int? distId { get; set; }
        public int? cityId { get; set; }
        public string? stationCode { get; set; }
        [Required(ErrorMessage = "Please Set Your Hotel Location Using Map.")]
        public double? lat { get; set; }
        [Required(ErrorMessage = "Please Set Your Hotel Location Using Map.")]
        public double? _long { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        public string? firstName { get; set; }
        [Required(ErrorMessage = "Last Name Required.")]
        public string? lastName { get; set; }
        public string? diviceIp { get; set; }
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please Verify Your Mobile Number.")]
        public bool isMobileVerify { get; set; }
        public List<SelectListItem>? StateList { get; set; }
        public List<SelectListItem>? DistrictList { get; set; }
        public List<SelectListItem>? CityList { get; set; }
        public List<SelectListItem>? PSList { get; set; }
    }
}
