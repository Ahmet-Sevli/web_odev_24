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
    public class IslemController : Controller
    {
        private readonly BerberContext _context;

        public IslemController(BerberContext context)
        {
            _context = context;
        }

        // GET: Islem
        public async Task<IActionResult> Index()
        {
            var berberContext = _context.Islemler.Include(i => i.calisan);
            return View(await berberContext.ToListAsync());
        }

        // GET: Islem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islemler
                .Include(i => i.calisan)
                .FirstOrDefaultAsync(m => m.islemID == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // GET: Islem/Create
        public async Task <IActionResult> Create()
        {
           ViewBag.CalisanlarListesi = new SelectList(await _context.Calisanlar.ToListAsync(), "calisanID", "calisan_ad");

            return View();
        }

        // POST: Islem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("islemID,islem_ad,islem_ucret,calisanID")] Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Islemler.Add(islem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CalisanlarListesi = new SelectList(await _context.Calisanlar.ToListAsync(), "calisanID", "calisan_ad");

            return RedirectToAction("Index","Home");
            // return View(islem);
        }

        // GET: Islem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islemler.FindAsync(id);
            if (islem == null)
            {
                return NotFound();
            }
            ViewData["calisanID"] = new SelectList(_context.Calisanlar, "calisanID", "calisan_ad", islem.calisanID);
            return View(islem);
        }

        // POST: Islem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("islemID,islem_ad,islem_ucret,calisanID")] Islem islem)
        {
            if (id != islem.islemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(islem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslemExists(islem.islemID))
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
            ViewData["calisanID"] = new SelectList(_context.Calisanlar, "calisanID", "calisan_ad", islem.calisanID);
            return View(islem);
        }

        // GET: Islem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islemler
                .Include(i => i.calisan)
                .FirstOrDefaultAsync(m => m.islemID == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // POST: Islem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var islem = await _context.Islemler.FindAsync(id);
            if (islem != null)
            {
                _context.Islemler.Remove(islem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IslemExists(int id)
        {
            return _context.Islemler.Any(e => e.islemID == id);
        }
    }
}
