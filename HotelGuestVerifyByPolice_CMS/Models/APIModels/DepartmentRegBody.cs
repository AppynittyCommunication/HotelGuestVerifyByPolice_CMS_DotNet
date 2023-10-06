namespace HotelGuestVerifyByPolice_CMS.Models.APIModels
{
    public class DepartmentRegBody
    {
   
        public string? userId { get; set; }
        public string? userType { get; set; }
        public string? mobile { get; set; }
        public string? email { get; set; }
        public int? stateId { get; set; }
        public int? distId { get; set; }
        public int? cityId { get; set; }
        public int? stationCode { get; set; }
        public double? lat { get; set; }
        public double? _long { get; set; }
        public string? diviceIp { get; set; }
        public string? password { get; set; }
        public bool? isMobileVerify { get; set; }
        public string? policeName { get; set; }
        public string? authPin { get; set;}
    }
}
