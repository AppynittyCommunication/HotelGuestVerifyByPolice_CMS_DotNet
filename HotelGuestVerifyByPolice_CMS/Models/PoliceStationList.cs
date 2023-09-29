namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class PoliceStationList
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PSList> Data { get; set; }
     
    }
    public class PSList
    {
        public int stationID { get; set; }
        //public string? stationCode { get; set; }
        public string? stationName { get; set; }
    }
}
