using iettproje.Data;
using iettproje.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;

namespace iettproje.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AppDbContext _databaseContext;
        private readonly IConfiguration _configuration;

        public AccountController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _databaseContext = appDbContext;
            _configuration = configuration;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Sifre + md5Salt;
                string hashedPassword = saltedPassword.MD5();

                Kullanicilar kullanici = _databaseContext.Kullanicilar.SingleOrDefault(x => x.EPosta.ToLower() == model.EPosta.ToLower() 
                && x.Sifre == hashedPassword);   

                if( kullanici != null)
                {
                    if(kullanici.Aktiflik)
                    {
                        ModelState.AddModelError(nameof(model.EPosta), "EPosta Aktif Değil");
                        return View(model);

                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, kullanici.Id.ToString()));
                    claims.Add(new Claim("Eposta", kullanici.EPosta));
                    claims.Add(new Claim("Isim", kullanici.Isim));
                    claims.Add(new Claim("Soyisim", kullanici.Soyisim)); 
                    claims.Add(new Claim(ClaimTypes.Role, kullanici.Role));




                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Anasayfa", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "EPosta veya Şifre Hatalı");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid) 
            {
                if (_databaseContext.Kullanicilar.Any(x => x.EPosta.ToLower() == model.EPosta.ToLower())) 
                {
                    ModelState.AddModelError(nameof(model.EPosta), "EPosta adresi daha önce kullanılmıştır");
                    return View(model);
                }

                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Sifre + md5Salt;
                string hashedPassword = saltedPassword.MD5();

                Kullanicilar kullanici = new()
                {
                    Isim = model.Isim,
                    Sifre = hashedPassword,
                    Soyisim = model.Soyisim,
                    SicilNo = model.SicilNo,
                    EPosta = model.EPosta,
                    TelefonNumarasi = model.TelefonNumarasi,
                    
                };

                _databaseContext.Kullanicilar.Add(kullanici);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "Kullanıcı Eklenemedi");
                }
                else
                {
                    TempData["SuccessMessage"] = "Kayıt başarılı! Giriş sayfasına yönlendiriliyorsunuz...";
                    return RedirectToAction(nameof(Login));
                }
          
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}


