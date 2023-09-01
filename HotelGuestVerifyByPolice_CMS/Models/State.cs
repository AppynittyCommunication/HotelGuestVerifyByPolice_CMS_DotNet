using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class State
    {
        [Column("ID")]
        public int Id { get; set; }

        [Key]
        [Column("StateID")]
        public int StateId { get; set; }

        [StringLength(50)]
        public string? StateName { get; set; }

        public bool? IsActive { get; set; }
    }
}
