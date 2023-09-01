using HotelGuestVerifyByPolice_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace HotelGuestVerifyByPolice_CMS.Controllers
{
    public class StatesController : Controller
    {
        Uri baseUri = new Uri("https://hotelapi.ictsbm.com/api/");
        private readonly HttpClient _httpClient;
        public StatesController() {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUri;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<State>? states = new();
         
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage rs = await _httpClient.PostAsync(_httpClient.BaseAddress + "SelectList/GetStates",null);

            
            if(rs.IsSuccessStatusCode)
            {
                string data = rs.Content.ReadAsStringAsync().Result;
                
                 states = JsonConvert.DeserializeObject<List<State>>(data);
                
               
            }
            return View(states);
        }
    }
}
