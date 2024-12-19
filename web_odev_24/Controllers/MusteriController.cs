using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_odev_24.Models;

namespace web_odev_24.Controllers
{
    public class MusteriController : Controller
    {
        private readonly BerberContext _context;

        public MusteriController(BerberContext context)
        {
            _context = context;
        }

        // GET: Musteri
        public async Task<IActionResult> Index()
        {
            return View(await _context.Musteriler.ToListAsync());
        }

        // GET: Musteri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musteri = await _context.Musteriler
                .FirstOrDefaultAsync(m => m.musteriID == id);
            if (musteri == null)
            {
                return NotFound();
            }

            return View(musteri);
        }

        // GET: Musteri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musteri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("musteriID,musteri_ad,musteri_soyad,musteri_telefon,musteri_email,musteri_sifre")] Musteri musteri)
        {

            // E-posta kontrolü (müşteri daha önce kayıtlı mı?)
            var existingCustomer = _context.Musteriler.FirstOrDefault(m => m.musteri_email == musteri.musteri_email);
            if (existingCustomer != null)
            {
                ModelState.AddModelError("musteri_email", "Bu e-posta adresiyle zaten bir hesap kayıtlı.");
                return View(musteri);
            }


            if (ModelState.IsValid)
            {
                _context.Add(musteri);
                await _context.SaveChangesAsync();

                // Claims oluştur ve kullanıcıyı giriş yapmış gibi işaretle
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, musteri.musteri_ad),
                    new Claim(ClaimTypes.Role, "Musteri"),
                    new Claim(ClaimTypes.Email, musteri.musteri_email),
                    new Claim(ClaimTypes.NameIdentifier,musteri.musteriID.ToString())
                    //new Claim("musteriID", musteri.musteriID.ToString())
                };

                foreach (var claim in claims)
                {
                    Console.WriteLine($"Claim: {claim.Type}, Value: {claim.Value}");
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                


                return RedirectToAction("Index","Home");
            }
            return View(musteri);
        }

        // GET: Musteri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musteri = await _context.Musteriler.FindAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }
            return View(musteri);
        }

        // POST: Musteri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("musteriID,musteri_ad,musteri_soyad,musteri_telefon,musteri_email,musteri_sifre")] Musteri musteri)
        {
            if (id != musteri.musteriID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musteri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusteriExists(musteri.musteriID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(musteri);
        }

        // GET: Musteri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musteri = await _context.Musteriler
                .FirstOrDefaultAsync(m => m.musteriID == id);
            if (musteri == null)
            {
                return NotFound();
            }

            return View(musteri);
        }

        // POST: Musteri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musteri = await _context.Musteriler.FindAsync(id);
            if (musteri != null)
            {
                _context.Musteriler.Remove(musteri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusteriExists(int id)
        {
            return _context.Musteriler.Any(e => e.musteriID == id);
        }
    }
}
