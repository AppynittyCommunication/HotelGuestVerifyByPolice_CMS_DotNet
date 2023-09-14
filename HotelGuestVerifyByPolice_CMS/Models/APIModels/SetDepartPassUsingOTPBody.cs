namespace HotelGuestVerifyByPolice_CMS.Models.APIModels
{
    public class SetDepartPassUsingOTPBody
    {
        public string? dUsername { get; set; }
        public string? otp { get; set; }
        public string? pass { get; set; }
    }
}
