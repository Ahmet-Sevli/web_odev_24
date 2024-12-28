using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using web_odev_24.Models;

namespace web_odev_24.Controllers
{
    public class FotoController : Controller
    {


        [HttpGet]
        public IActionResult Yukle()
        {
            return View(); // Razor View'i çağırır
        }

        [HttpPost]
        public async Task<IActionResult> Yukle(IFormFile image, string hairType)
        {
            if (image == null || image.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bir resim yükleyin.");
                return View("Yukle");
            }

            if (string.IsNullOrEmpty(hairType))
            {
                ModelState.AddModelError("", "Lütfen bir saç tipi belirtin.");
                return View("Yukle");
            }

            var apiKey = "b3798db1d6msh72ec194f92edcfdp180d5fjsnb7516b7f7041";
            var apiHost = "hairstyle-changer.p.rapidapi.com";
            var apiUrl = "https://hairstyle-changer.p.rapidapi.com/huoshan/facebody/hairstyle";

            using var httpClient = new HttpClient();
            using var formData = new MultipartFormDataContent();

            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                var imageContent = new ByteArrayContent(stream.ToArray());
                imageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "image_target",
                    FileName = image.FileName
                };
                formData.Add(imageContent, "image_target");
            }

            var hairTypeContent = new StringContent(hairType);
            hairTypeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "hair_type"
            };
            formData.Add(hairTypeContent);

            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", apiHost);

            var response = await httpClient.PostAsync(apiUrl, formData);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // JSON verisini ayrıştır
                var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                string base64Image = responseObject?.data?.image;

                if (!string.IsNullOrEmpty(base64Image))
                {
                    ViewBag.Base64Image = base64Image; // Base64 veriyi ViewBag ile View'a gönderiyoruz.
                    return View("Sonuc");
                }
                else
                {
                    ModelState.AddModelError("", "API yanıtında görsel verisi bulunamadı.");
                    return View("Yukle");
                }
            }
            else
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"API isteği başarısız oldu: {errorDetails}");
                return View("Yukle");
            }
        }






    }
}
