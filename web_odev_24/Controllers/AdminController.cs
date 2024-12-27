using Microsoft.AspNetCore.Mvc;
using web_odev_24.Models;
using Newtonsoft.Json;
using System.Text;

namespace web_odev_24.Controllers
{
    public class AdminController : Controller
    {
        private readonly BerberContext _context;

        public AdminController(BerberContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            List<Islem> islemler = new List<Islem>();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:7252/api/Islem_api/");
            var jsondata = await response.Content.ReadAsStringAsync();
            islemler = JsonConvert.DeserializeObject<List<Islem>>(jsondata);
            return View(islemler);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public async Task<IActionResult> Create(Islem islem)
        {
            if (!ModelState.IsValid)
            {
                return View(islem);
            }

            var httpClient = new HttpClient();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(islem), Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("https://localhost:7252/api/Islem_api/", jsonContent);
                response.EnsureSuccessStatusCode(); // Eğer 2xx dönmezse hata fırlatır
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API İsteği Hatası: {ex.Message}");
                return StatusCode(500, "API isteğinde bir hata oluştu.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
