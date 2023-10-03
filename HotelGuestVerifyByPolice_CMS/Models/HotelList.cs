namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class HotelList
    {
        public int Code { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public List<HotelData>? Data { get; set; }

    }
    public class HotelData
    {
        public string? hotelRegNo { get; set; }
        public string? hotelName { get; set; }
    }
}
