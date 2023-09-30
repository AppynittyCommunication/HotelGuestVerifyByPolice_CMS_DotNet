namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class DistictList
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<DistData> Data { get; set; }
     
    }
    public class DistData
    {
        public int distId { get; set; }
        public string? distName { get; set; }
    }
}
