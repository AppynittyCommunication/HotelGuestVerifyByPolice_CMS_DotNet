﻿namespace HotelGuestVerifyByPolice_CMS.Areas.Department.Models
{
    public class DeptSearchHotelRes
    {
        public int code { get; set; }
        public string? status { get; set; }
        public string? message { get; set; }

        public List<HotelTitle>? hotelTitle { get; set; }
        public List<HotelGuest>? hotelGuests { get; set; }
        public List<LastVisitor>? lastVisitors { get; set; }
    }

    public class HotelGuest
    {
        public string? guestName { get; set; }
        public int reservation { get; set; }
        public int nightStayed { get; set; }
        public string? lastVisit { get; set; }
        public string? mobile { get; set; }
        public string? city { get; set; }
        public object? address { get; set; }
        public string? country { get; set; }
    }

    public class HotelTitle
    {
        public string? hotelName { get; set; }
        public string? address { get; set; }
        public string? mobile { get; set; }
        public string? city { get; set; }
        public string? policeSation { get; set; }
    }

    public class LastVisitor
    {
        public string? guestName { get; set; }
        public int age { get; set; }
        public string? city { get; set; }
        public string? purpose { get; set; }
        public string? commingFrom { get; set; }
        public string? reservaion { get; set; }
        public string? checkInDate { get; set; }
    }
}
