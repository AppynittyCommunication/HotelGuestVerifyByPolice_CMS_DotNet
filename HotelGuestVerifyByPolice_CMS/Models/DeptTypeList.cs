using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class DeptTypeList
    {

        public int departmentTypeID { get; set; }
        public string? departmentTypeName { get; set; }
    }
}
