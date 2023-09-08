using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class DeptTypeList
    {

        [Column("ID")]
        public int Id { get; set; }

        [Key]
        [Column("DeptTypeID")]
        public int StatDeptTypeIDeId { get; set; }

        [StringLength(50)]
        public string? DeptTypeName { get; set; }

        public bool? IsActive { get; set; }
    }
}
