namespace HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Models
{
    public class SearchHotel
    {
        public int? stateId { get; set; }
        public int? distId { get; set; }
        public int? cityId { get; set; }
        public string? stationCode { get; set; }

        public string? hotelRegNo { get; set; }
    }
}
