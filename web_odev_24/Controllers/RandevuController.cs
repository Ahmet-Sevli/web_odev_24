using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_odev_24.Models;

namespace web_odev_24.Controllers
{
    public class RandevuController : Controller
    {
        private readonly BerberContext _context;

        public RandevuController(BerberContext context)
        {
            _context = context;
        }

        // GET: Randevu/Create
        public IActionResult Randevu_Al()
        {
            var musteriIDClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (musteriIDClaim == null)
            {
                return RedirectToAction("Login", "Hesap");
            }

            try
            {
                int musteriID = int.Parse(musteriIDClaim);
                var musteri = _context.Musteriler.FirstOrDefault(m => m.musteriID == musteriID);

                if (musteri == null)
                {
                    return Unauthorized();
                }

                ViewBag.Islemler = new SelectList(_context.Islemler, "islemID", "islem_ad");
                ViewBag.MusteriAd = musteri.musteri_ad + " " + musteri.musteri_soyad;
                ViewBag.MusteriID = musteri.musteriID;

                return View();
            }
            catch (FormatException)
            {
                return RedirectToAction("Login", "Hesap");
            }
        }

        // POST: Randevu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Randevu_Al([Bind("randevu_tarihi,randevu_saati,islemID,calisanID")] Randevu randevu)
        {
            var musteriIDClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (musteriIDClaim == null)
            {
                return RedirectToAction("Login", "Hesap");
            }

            try
            {
                int musteriID = int.Parse(musteriIDClaim);
                randevu.musteriID = musteriID;

                // 9:00 ile 17:00 arasında saat kontrolü
                if (randevu.randevu_saati < TimeSpan.FromHours(9) || randevu.randevu_saati > TimeSpan.FromHours(17))
                {
                    ModelState.AddModelError("", "Randevu saati 9:00 ile 17:00 arasında olmalıdır.");
                    return View(randevu);
                }

                // Saat başı olup olmadığını kontrol et
                if (randevu.randevu_saati.Minutes != 0)
                {
                    ModelState.AddModelError("", "Randevu saati sadece saat başı olmalıdır.");
                    return View(randevu);
                }

                bool mevcutRandevu = _context.Randevular
                    .Any(r => r.calisanID == randevu.calisanID
                              && r.randevu_tarihi == randevu.randevu_tarihi
                              && r.randevu_saati == randevu.randevu_saati
                              && !r.onay_durumu);

                if (mevcutRandevu)
                {
                    ModelState.AddModelError("", "Seçilen tarih ve saat için çalışan zaten meşgul.");
                    return View(randevu);
                }

                if (ModelState.IsValid)
                {
                    _context.Randevular.Add(randevu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Randevu_Istekleri", "Admin");
                }

                return View(randevu);
            }
            catch (FormatException)
            {
                return RedirectToAction("Login", "Hesap");
            }
        }
        // Dinamik çalışan listesi için AJAX endpoint
        [HttpGet]
       public JsonResult GetCalisanlarByIslem(int islemID)
{
    // Seçilen işleme uygun çalışanları filtrele
    var calisanlar = _context.Calisanlar
        .Where(c => c.islemID == islemID)  // Seçilen işleme uygun çalışanlar
        .Select(c => new
        {
            c.calisanID,
            AdSoyad = c.calisan_ad + " " + c.calisan_soyad
        })
        .ToList();

    return Json(calisanlar);
}

        // Randevu isteklerini listeleme (Admin)
        public async Task<IActionResult> Randevu_Istekleri()
        {
            var randevular = _context.Randevular
                .Include(r => r.Islem)
                .Include(r => r.Calisan)
                .Include(r => r.Musteri)
                .Where(r => !r.onay_durumu);

            return View(await randevular.ToListAsync());
        }

        // Admin randevuyu onaylama
        public async Task<IActionResult> Onayla(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null) return NotFound();

            randevu.onay_durumu = true;
            _context.Update(randevu);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Randevu_Istekleri));
        }

        // Admin randevuyu reddetme
        public async Task<IActionResult> Reddet(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null) return NotFound();

            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Randevu_Istekleri));
        }
    }
}
