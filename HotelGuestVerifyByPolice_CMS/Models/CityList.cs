namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class CityList
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<CityData> Data { get; set; }
      
    }
    public class CityData
    {
        public int cityId { get; set; }

        public string? cityName { get; set; }
    }
}
