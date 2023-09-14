namespace HotelGuestVerifyByPolice_CMS.Models.APIModels
{
    public class ForgetDepartPassBody
    {
        public bool otpstatus { get; set; }
        public string? dUserId { get; set; }
        public string? dPassword { get; set; }
    }
}
