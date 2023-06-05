using Library.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Library.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _client;
        private Uri baseAddress = new Uri("https://localhost:7164");

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<UserResponseDto> user = new List<UserResponseDto>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "Users/all").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<List<UserResponseDto>>(data);
            }
            return View(user);
        }


        public IActionResult AddUser()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddUser(RegisterDto user)
        {
            HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "Users/AddNew", user).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string errorMessage = "An error occurred while creating the book.";
                ViewBag.ErrorMessage = errorMessage;
                return View("AddUser");
            }
        }

        public async Task<IActionResult> UpdateUser(Guid userId)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Users/" + userId);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                UserUpdateDto user = JsonConvert.DeserializeObject<UserUpdateDto>(data);
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }






        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateDto user)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(_client.BaseAddress + "Users", user);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string errorMessage = "An error occurred while updating the user.";
                ViewBag.ErrorMessage = errorMessage;
                return View(user);
            }
        }

        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            HttpResponseMessage response = await _client.DeleteAsync(baseAddress + "Users/" + userId);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string errorMessage = "An error occurred while deleting the user.";
                ViewBag.ErrorMessage = errorMessage;
                return View("Index");
            }
        }

        public async Task<IActionResult> SearchUsers(string email, string userName)
        {
            List<UserResponseDto> users = new List<UserResponseDto>();
            HttpResponseMessage response = await _client.GetAsync(baseAddress + "Users/search?email=" + email + "&userName=" + userName);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<UserResponseDto>>(data);
            }
            return View("Index", users);
        }



        /* [HttpGet("{userId}")]
         public async Task<ActionResult<UserResponseDto>> GetUser(Guid userId)
         {
             var user = await _client.GetAsync(baseAddress + "Users/" + userId);
             if (user.IsSuccessStatusCode)
             {
                 var data = await user.Content.ReadAsStringAsync();
                 return Ok(JsonConvert.DeserializeObject<UserResponseDto>(data));
             }
             else
             {
                 return NotFound();
             }
         }*/

    }
}
