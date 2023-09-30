namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class Country
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<CountryData> Data { get; set; }
    }

    public class CountryData
    {
        public string? countryCode { get; set; }
        public string? countryName { get; set; }
    }
}
