using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class HotelReg
    {
        [Required(ErrorMessage = "Hotel Name is required.")]
        public string? hotelName { get; set; }
        [Required(ErrorMessage = "Hotel Registration Number is required.")]
        public string hotelRegNo { get; set; } = null!;
        [Required(ErrorMessage = "Username is required.")]
        public string? userId { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string? firstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string? lastName { get; set; }
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string? mobile { get; set; }
        [EmailAddress]
        [RegularExpression("^[_A-Za-z'`+-.]+([_A-Za-z0-9'+-.]+)*@([A-Za-z0-9-])+(\\.[A-Za-z0-9]+)*(\\.([A-Za-z]*){3,})$", ErrorMessage = "Enter proper email")]
        [Required(ErrorMessage = "Email Address is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format not proper of email address")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Hotel Address is required.")]
        public string? address { get; set; }
        [Required(ErrorMessage = "PinCode is required.")]
        public int? pinCode { get; set; }
        [Required(ErrorMessage = "Please Select State.")]
        public int? stateId { get; set; }
        [Required(ErrorMessage = "Please Select District.")]
        public int? distId { get; set; }
        [Required(ErrorMessage = "Please Select City.")]
        public int? cityId { get; set; }
        [Required(ErrorMessage = "Please Select Police Station.")]
        public string? stationCode { get; set; }
        [Required(ErrorMessage = "Please Set Your Hotel Location Using Map.")]
        public double? lat { get; set; }
        [Required(ErrorMessage = "Please Set Your Hotel Location Using Map.")]
        public double? _long { get; set; }
        public string? diviceIp { get; set; }

        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> DistrictList { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public List<SelectListItem> PSList { get; set; }
    }
}
