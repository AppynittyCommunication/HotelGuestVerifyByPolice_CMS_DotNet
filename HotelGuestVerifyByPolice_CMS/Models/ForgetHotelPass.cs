using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class ForgetHotelPass
    {
        public string? username { get; set; }
        [DataType(DataType.Password)]
        public string? pass { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("pass", ErrorMessage = "The password and confirmation password do not match.")]
        public string? cPass { get; set; }

        public bool otpstatus { get; set; }
    }
}
