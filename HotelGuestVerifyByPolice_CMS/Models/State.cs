using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelGuestVerifyByPolice_CMS.Models
{
    public class State
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<StateData> Data { get; set; }
    }
    public class StateData
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
