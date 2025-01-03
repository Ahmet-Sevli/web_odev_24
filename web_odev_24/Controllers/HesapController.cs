﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web_odev_24.Models;

namespace web_odev_24.Controllers
{
    public class HesapController : Controller
    {
        private readonly BerberContext _context; // Veritabanı bağlamı (EF Core veya benzeri)

        public HesapController(BerberContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string _email, string _sifre)
        {
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_sifre))
            {
                ModelState.AddModelError("", "E-posta ve şifre alanları doldurulmalıdır.");
                return View();
            }

            // Admin tablosunda kontrol
            var admin = _context.Adminler.FirstOrDefault(a => a.admin_email == _email && a.admin_sifre == _sifre);
            if (admin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _email),
                    new Claim(ClaimTypes.Role, "Admin"), // Admin rolü
                    new Claim(ClaimTypes.Email, _email),
                    new Claim("adminID", admin.adminID.ToString()) // Kendi ID'sini taşıyan özel claim
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home"); // Admin anasayfasına yönlendir
            }

            // Müşteri tablosunda kontrol
            var musteri = _context.Musteriler.FirstOrDefault(c => c.musteri_email == _email && c.musteri_sifre == _sifre);
            if (musteri != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _email),
                    new Claim(ClaimTypes.Role, "Musteri"), // Müşteri rolü
                    new Claim(ClaimTypes.Email, _email),
                    new Claim(ClaimTypes.NameIdentifier, musteri.musteriID.ToString()),
                    new Claim("musteriID", musteri.musteriID.ToString()) // Kendi ID'sini taşıyan özel claim
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home"); // Müşteri paneline yönlendir
            }

            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}