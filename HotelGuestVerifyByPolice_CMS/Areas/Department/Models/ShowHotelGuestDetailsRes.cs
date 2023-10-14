namespace HotelGuestVerifyByPolice_CMS.Areas.Department.Models
{
    public class ShowHotelGuestDetailsRes
    {
        public int code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public ShowHotelGuestDetailsResData? data { get; set; }
      
    }

    public class ShowHotelGuestDetailsResData {
        public List<HotelGuestDetail> hotelGuestDetails { get; set; }
        public List<AddOnGuestDetails1> addOnGuestDetails1 { get; set; }
    }

    public class AddOnGuestDetails1
    {
        public string relationWithGuest { get; set; }
        public string guestName { get; set; }
    }

    public class HotelGuestDetail
    {
        public string roomBookingId { get; set; }
        public string guestName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public object address { get; set; }
        public string guestPhoto { get; set; }
    }
}
