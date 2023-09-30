namespace HotelGuestVerifyByPolice_CMS.Models.APIModels
{
   
    public class HotelGuestRegBody
    {
        public string? hotelRegNo { get; set; }
        public string? guestName { get; set; }
        public string? guestType { get; set; }
        public string? gender { get; set; }
        public string? email { get; set; }
        public string? country { get; set; }
        public string? state { get; set; }
        public string? city { get; set; }
        public int numberOfGuest { get; set; }
        public int age { get; set; }
        public string? mobile { get; set; }
        public string? visitPurpose { get; set; }
        public string? roomType { get; set; }
        public int roomNo { get; set; }
        public string? comingFrom { get; set; }
        public string? guestIdType { get; set; }
        public string? guestIDProof { get; set; }
        public string? guestPhoto { get; set; }
        public string? paymentMode { get; set; }
        public List<AddOnGuest>? addOnGuest { get; set; }
    }
    public class AddOnGuest
    {
        public string? guestName { get; set; }
        public int age { get; set; }
        public string? mobile { get; set; }
        public string? relationWithGuest { get; set; }
        public string? guestType { get; set; }
        public string? gender { get; set; }
        public string? email { get; set; }
        public string? country { get; set; }
        public string? state { get; set; }
        public string? city { get; set; }
        public string? comingFrom { get; set; }
        public string? guestIdType { get; set; }
        public string? guestIDProof { get; set; }
        public string? guestPhoto { get; set; }
    }

   
}
