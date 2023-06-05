using Library.Domain.Entities;
using Library.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private Uri baseAddress = new Uri("https://localhost:7164");

        private readonly HttpClient _client;

        public BookController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }



        public IActionResult Index()
        {
            List<Book> booklist = new List<Book>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Books").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                booklist = JsonConvert.DeserializeObject<List<Book>>(data);
            }
            return View(booklist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookCreateDto book)
        {
            HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "Books", book).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string errorMessage = "An error occurred while creating the book.";
                ViewBag.ErrorMessage = errorMessage;
                return View("Create", book);
            }
        }


        public async Task<IActionResult> DeleteBook(Guid id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "Books/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string errorMessage = "An error occurred while deleting the book.";
                ViewBag.ErrorMessage = errorMessage;
                return View("Index");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Books/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                BookUpdateDto book = JsonConvert.DeserializeObject<BookUpdateDto>(data);
                return View(book);
            }
            else
            {
                string errorMessage = "An error occurred while retrieving the book.";
                ViewBag.ErrorMessage = errorMessage;
                return View("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookUpdateDto book)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(_client.BaseAddress + "Books", book);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string errorMessage = "An error occurred while updating the book.";
                ViewBag.ErrorMessage = errorMessage;
                return View("Edit", book);
            }
        }



        public async Task<IActionResult> Details(Guid id)
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Books/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Book book = JsonConvert.DeserializeObject<Book>(data);
                return View(book);
            }
            else
            {
                string errorMessage = "An error occurred while retrieving the book.";
                ViewBag.ErrorMessage = errorMessage;
                return View("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Search(string isbn, string title, string author, string publisher, string yearPublished)
        {
            string searchUrl = _client.BaseAddress + "Books/search?";
            if (!string.IsNullOrWhiteSpace(isbn))
            {
                searchUrl += "isbn=" + isbn + "&";
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                searchUrl += "title=" + title + "&";
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                searchUrl += "author=" + author + "&";
            }
            if (!string.IsNullOrWhiteSpace(publisher))
            {
                searchUrl += "publisher=" + publisher + "&";
            }
            if (!string.IsNullOrWhiteSpace(yearPublished))
            {
                searchUrl += "yearPublished=" + yearPublished + "&";
            }

            HttpResponseMessage response = await _client.GetAsync(searchUrl);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<Book> searchResult = JsonConvert.DeserializeObject<List<Book>>(data);
                return View("Index", searchResult);
            }
            else
            {
                string errorMessage = "An error occurred while searching for books.";
                ViewBag.ErrorMessage = errorMessage;
                return View("Index");
            }
        }



    }
}
