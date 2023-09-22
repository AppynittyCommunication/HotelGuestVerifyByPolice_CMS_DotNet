namespace HotelGuestVerifyByPolice_CMS.Models.APIModels
{
    public class SaveAuthBody
    {
        public string? authPin { get; set; }
        public string? useFor { get; set; }
        public string? userID { get; set; }
        public bool? isUse { get; set; }
    }
}
