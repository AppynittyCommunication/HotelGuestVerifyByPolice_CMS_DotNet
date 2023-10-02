namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class RelationType
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<RelationTypeData> Data { get; set; }
    }

    public class RelationTypeData
    {
        public int id { get; set; }
        public string? name { get; set; }
    }
}
