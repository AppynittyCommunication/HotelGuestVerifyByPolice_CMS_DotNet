namespace HotelGuestVerifyByPolice_CMS.Models.APIModels
{
    public class ForgetHotelPassBody
    {
        public bool otpstatus { get; set; }
        public string? hUserId { get; set; }
        public string? hPassword { get; set; }
    }
}
