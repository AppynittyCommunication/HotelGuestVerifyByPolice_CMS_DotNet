using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class DepartLogin
    {
        [Required(ErrorMessage = "Department Username required.")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
