namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class IdProofType
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<IdProofTypeData> Data { get; set; }
    }

    public class IdProofTypeData
    {
        public int id { get; set; }
        public string? idProofType { get; set; }
    }
}
