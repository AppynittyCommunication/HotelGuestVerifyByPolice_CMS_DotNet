using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelGuestVerifyByPolice_CMS.Areas.HotelDashboard.Models
{
    public class HotelDashboardRes
    {
        public int code { get; set; }
        public string? status { get; set; }
        public string? message { get; set; }
        public List<HotelDashResData> data { get; set; }
    }
    public class HotelDashResData
    {
        public int totalGuest { get; set; }
        public int todaysCheckIn { get; set; }
        public int todaysCheckOut { get; set; }
        public List<GuestDetail> guestDetails { get; set; }
    }

    public class GuestDetail
    {
        public string guestName { get; set; }
        public string guestPhoto { get; set; }
        public string reservation {  get; set; }
        public string mobile { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string checkInDate { get; set; }
    }
}
