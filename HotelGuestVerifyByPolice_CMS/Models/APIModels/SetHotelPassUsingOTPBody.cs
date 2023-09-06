using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models.APIModels
{
    public class SetHotelPassUsingOTPBody
    {
        public string? hUsername { get; set; }
        public string? otp { get; set; }
        public string? pass { get; set; }

    }
}
