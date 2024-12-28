using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using web_odev_24.Models;
using Microsoft.EntityFrameworkCore;

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



            // İşlemleri ViewBag'a ekliyoruz
            ViewBag.Islemler = new SelectList(_context.Islemler, "islemID", "islem_ad");

            // Çalışanları ViewBag'a ekliyoruz
            var calisanlar = _context.Calisanlar.ToList();
            ViewBag.Calisanlar = new SelectList(_context.Calisanlar, "calisanID", "AdSoyad");

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
    public async Task<IActionResult> Randevu_Al([Bind("randevu_tarihi, randevu_saati, islemID, calisanID")] Randevu randevu)
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

            /* // Seçilen işlem ve çalışanın eşleşip eşleşmediğini kontrol et
             var calisan = _context.Calisanlar.FirstOrDefault(c => c.calisanID == randevu.calisanID);
             if (calisan == null || calisan.islemID != randevu.islemID)
             {




                 ModelState.AddModelError("", "Seçilen işlem ile çalışan uyumsuz. Lütfen doğru çalışanı seçin.");
                 return View(randevu);
             }*/

            // Mevcut randevu kontrolü
            bool mevcutRandevu = _context.Randevular
                .Any(r => r.calisanID == randevu.calisanID && r.randevu_tarihi == randevu.randevu_tarihi && r.randevu_saati == randevu.randevu_saati && !r.onay_durumu);
            if (mevcutRandevu)
            {
                ModelState.AddModelError("", "Seçilen tarih ve saat için çalışan zaten meşgul.");

                var musteri = _context.Musteriler.FirstOrDefault(m => m.musteriID == musteriID);
                // İşlemleri ViewBag'a ekliyoruz
                ViewBag.Islemler = new SelectList(_context.Islemler, "islemID", "islem_ad");

                // Çalışanları ViewBag'a ekliyoruz
                var calisanlar = _context.Calisanlar.ToList();
                ViewBag.Calisanlar = new SelectList(_context.Calisanlar, "calisanID", "AdSoyad");

                ViewBag.MusteriAd = musteri.musteri_ad + " " + musteri.musteri_soyad;
                ViewBag.MusteriID = musteri.musteriID;
                return View(randevu);
            }

            if (ModelState.IsValid)
            {
                _context.Randevular.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(randevu);
        }
        catch (FormatException)
        {
            return RedirectToAction("Login", "Hesap");
        }
    }









    public IActionResult Randevular()
    {
        // Admin olarak giriş yapan kullanıcıya ait randevuların listelenmesi
        var randevular = _context.Randevular
            .Include(r => r.Musteri)
            .Include(r => r.Calisan)
            .Include(r => r.Islem)
            .ToList();

        return View(randevular);
    }

    // Randevu Onaylama
    [HttpGet]

    public async Task<IActionResult> Onayla(int id)
    {
        var randevu = await _context.Randevular
                .FirstOrDefaultAsync(r => r.randevuID == id);

        if (randevu == null)
        {
            return NotFound();
        }

        randevu.onay_durumu = true; // Randevu onaylandı

        try
        {
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Randevular)); // Onay sonrası randevular sayfasına yönlendirme
        }
        catch (Exception)
        {
            // Hata durumunda bir mesaj ekleyebilirsiniz
            TempData["ErrorMessage"] = "Randevu onaylanırken bir hata oluştu.";
            return RedirectToAction(nameof(Randevular));
        }
    }




    // Müşteri Randevularını Görüntüleme
    public IActionResult Randevum()
    {
        var musteriIDClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (musteriIDClaim == null)
        {
            return RedirectToAction("Login", "Hesap");
        }

        try
        {
            int musteriID = int.Parse(musteriIDClaim);

            // Sadece müşteriye ait randevuları getir
            var musteriRandevulari = _context.Randevular
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Where(r => r.musteriID == musteriID)
                .ToList();

            return View(musteriRandevulari);
        }
        catch (FormatException)
        {
            return RedirectToAction("Login", "Hesap");
        }
    }





















}




