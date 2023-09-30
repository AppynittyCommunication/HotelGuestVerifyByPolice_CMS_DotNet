using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class DeptTypeList
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<DeptData> Data { get; set; }
       
    }

    public class DeptData
    {
        public int DeptTypeId { get; set; }
        public string? DeptTypeName { get; set; }
    }
}
