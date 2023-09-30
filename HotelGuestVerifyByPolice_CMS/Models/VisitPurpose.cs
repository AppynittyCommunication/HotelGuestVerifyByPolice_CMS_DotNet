namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class VisitPurpose
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<VisitPurposeData> Data { get; set; }
    }

    public class VisitPurposeData
    {
        public int id { get; set; }
        public string? purpose { get; set; }
    }
}
