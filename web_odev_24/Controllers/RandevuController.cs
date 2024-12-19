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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Randevu_Al([Bind("RandevuTarihi, RandevuSaati, IslemID, CalisanID, MusteriID")] RandevuViewModel model)
        {
            var musteriIDClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (musteriIDClaim == null)
            {
                return RedirectToAction("Login", "Hesap");
            }

            try
            {
                int musteriID = int.Parse(musteriIDClaim);

                // Saatin 9:00 ile 17:00 arasında olduğundan emin olalım
                TimeSpan randevuSaati = model.RandevuSaati; // Eğer RandevuSaati TimeSpan ise
                int hour = randevuSaati.Hours; // Saat olarak al

                if (hour < 9 || hour > 17)
                {
                    ModelState.AddModelError("", "Randevular sadece 9:00 ile 17:00 arasında alınabilir.");
                    return View(model);
                }

                var randevu = new RandevuViewModel
                {
                    RandevuTarihi = model.RandevuTarihi,
                    RandevuSaati = hour,
                    IslemID = model.IslemID,
                    CalisanID = model.CalisanID,
                    MusteriID = musteriID
                };

                bool mevcutRandevu = _context.Randevular
                    .Any(r => r.calisanID == randevu.CalisanID
                              && r.randevu_tarihi == randevu.RandevuTarihi
                              && r.randevu_saati == randevu.RandevuSaati
                              && !r.onay_durumu);

                if (mevcutRandevu)
                {
                    ModelState.AddModelError("", "Seçilen tarih ve saat için çalışan zaten meşgul.");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    _context.Randevular.Add(randevu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Randevu_Istekleri", "Admin");
                }

                return View(model);
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
