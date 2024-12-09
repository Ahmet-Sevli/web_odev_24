using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_odev_24.Models;

namespace web_odev_24.Controllers
{
    public class CalisanController : Controller
    {
        private readonly BerberContext _context;

        public CalisanController(BerberContext context)
        {
            _context = context;
        }

        // GET: Calisan
        public async Task<IActionResult> Calisanlari_goruntule()
        {
            var calisanListesi = from calisan in _context.Calisanlar
                                 join islem in _context.Islemler
                                 on calisan.islemID equals islem.islemID
                                 select new CalisanViewModel
                                 {
                                     calisanID = calisan.calisanID,
                                     calisan_ad = calisan.calisan_ad,
                                     calisan_soyad = calisan.calisan_soyad,
                                     calisan_tecrube = calisan.calisan_tecrube,
                                     islem_ad = islem.islem_ad
                                 };

            return View(await calisanListesi.ToListAsync());
            // return View(await _context.Calisanlar.ToListAsync());
        }

        // GET: Calisan/Details/5
        public async Task<IActionResult> Detay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar
                .FirstOrDefaultAsync(m => m.calisanID == id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // GET: Calisan/Create
        public IActionResult Calisan_ekle()
        {
            ViewBag.IslemlerListe = new SelectList(_context.Islemler.ToList(), "islemID", "islem_ad");
            return View();
        }

        // POST: Calisan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calisan_ekle([Bind("calisanID,calisan_ad,calisan_soyad,calisan_tecrube,islemID")] Calisan calisan)
        {
            
                _context.Add(calisan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Calisanlari_goruntule));
           
            //return View(calisan);
        }

        // GET: Calisan/Edit/5
        public async Task<IActionResult> Duzenle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar.FindAsync(id);
            if (calisan == null)
            {
                return NotFound();
            }
            return View(calisan);
        }

        // POST: Calisan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(int id, [Bind("calisanID,calisan_ad,calisan_soyad,calisan_tecrube,islemID")] Calisan calisan)
        {
            if (id != calisan.calisanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calisan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanExists(calisan.calisanID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Calisanlari_goruntule));
            }
            return View(calisan);
        }

        // GET: Calisan/Delete/5
        public async Task<IActionResult> Calisan_sil(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar
                .FirstOrDefaultAsync(m => m.calisanID == id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // POST: Calisan/Delete/5
        [HttpPost, ActionName("Calisan_sil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calisan = await _context.Calisanlar.FindAsync(id);
            if (calisan != null)
            {
                _context.Calisanlar.Remove(calisan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanExists(int id)
        {
            return _context.Calisanlar.Any(e => e.calisanID == id);
        }
    }
}
