using iettproje.Data;
using iettproje.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;


namespace iettproje.Controllers
{
    [Authorize]
    public class IslemlerController : Controller
    {
        private readonly AppDbContext _databaseContext;


        public IslemlerController(AppDbContext appDbContext)
        {
            _databaseContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Islemler(int pageNumber = 1, int pageSize = 10)
        {
            var query = _databaseContext.IslemlerList.AsQueryable();

            int totalItems = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new IslemlerListVM
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,

                // Kategorileri al
                Modeller = _databaseContext.UrunModelleri.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Islemler(IslemlerListVM model)
        {
            if (ModelState.IsValid)
            {
                var ıslemlerList = new IslemlerList
                {
                    Sicil = model.Sicil,
                    Marka = model.Marka,
                    Modeli = model.Modeli,
                    Tarih = DateTime.Now,
                    Adet = model.Adet,
                    Durum=model.Durum,

                     
                };

                _databaseContext.IslemlerList.Add(ıslemlerList);
                _databaseContext.SaveChanges();

                TempData["SuccessMessage"] = "Kategori başarıyla kaydedilmiştir.";

                return RedirectToAction("Islemler");
            }

            model.Modeller = _databaseContext.UrunModelleri.ToList();
            model.Items = _databaseContext.IslemlerList
                .Skip((model.PageNumber - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            model.TotalItems = _databaseContext.IslemlerList.Count();

            return View(model);
        }


        [HttpGet]
        public IActionResult UrunKategorileri(int pageNumber = 1, int pageSize = 10)
        {
            var query = _databaseContext.UrunKategorileri.AsQueryable();

            int totalItems = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new UrunlerVM
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(viewModel);
        }



        [HttpPost]
        public IActionResult UrunKategorileri(UrunlerVM model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.UrunKategorileri.Any(x => x.Urunler.ToLower() == model.Urunler.ToLower()))
                {
                    TempData["ErrorMessage"] = "Bu kategori adı daha önce kullanılmıştır.";

                    model.Items = _databaseContext.UrunKategorileri
                        .Skip((model.PageNumber - 1) * model.PageSize)
                        .Take(model.PageSize)
                        .ToList();

                    model.TotalItems = _databaseContext.UrunKategorileri.Count();
                    return View(model);
                }

                var urunKategorileri = new UrunKategorileri
                {
                    Urunler = model.Urunler,
                    Durum=model.Durum
                };

                _databaseContext.UrunKategorileri.Add(urunKategorileri);
                _databaseContext.SaveChanges();

                TempData["SuccessMessage"] = "Kategori başarıyla kaydedilmiştir.";

                // Güncellenmiş kategori listesini al
                model.Items = _databaseContext.UrunKategorileri
                    .Skip((model.PageNumber - 1) * model.PageSize)
                    .Take(model.PageSize)
                    .ToList();

                model.TotalItems = _databaseContext.UrunKategorileri.Count();

                // Kategori listesini güncelle
                return View(model);
            }

            model.Items = _databaseContext.UrunKategorileri
                .Skip((model.PageNumber - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            model.TotalItems = _databaseContext.UrunKategorileri.Count();

            return View(model);
        }

        [HttpGet]
        public IActionResult UrunModelleri(int pageNumber = 1, int pageSize = 10)
        {
            var query = _databaseContext.UrunModelleri.AsQueryable();

            int totalItems = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new UrunModelleriVM
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
              
               // Kategorileri al
                Kategoriler = _databaseContext.UrunKategorileri.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UrunModelleri(UrunModelleriVM model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.UrunModelleri.Any(x => x.Modeli.ToLower() == model.Modeli.ToLower()))
                {
                    TempData["ErrorMessage"] = "Bu Model adı daha önce kullanılmıştır.";
                }
                else
                {
                    var urunModelleri = new UrunModelleri
                    {
                        Marka = model.Marka,
                        Modeli = model.Modeli,
                        Kategorisi=model.Kategorisi
                    };

                    _databaseContext.UrunModelleri.Add(urunModelleri);
                    _databaseContext.SaveChanges();

                    TempData["SuccessMessage"] = "Kategori başarıyla kaydedilmiştir.";

                    return RedirectToAction("UrunModelleri");
                }
            }

            model.Kategoriler = _databaseContext.UrunKategorileri.ToList();
            model.Items = _databaseContext.UrunModelleri
                .Skip((model.PageNumber - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            model.TotalItems = _databaseContext.UrunModelleri.Count();

            return View(model);
        }


        [HttpPost]
        public IActionResult ActivateIslem(int id)
        {
            var islem = _databaseContext.IslemlerList.Find(id);
            if (islem != null)
            {
                islem.Durum = true;
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("Islemler"); 
        }

        // Pasifleştirme işlemi
        [HttpPost]
        public IActionResult DeactivateIslem(int id)
        {
            var islem = _databaseContext.IslemlerList.Find(id);
            if (islem != null)
            {
                islem.Durum = false;
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("Islemler"); 
        }



             [HttpPost]
        public IActionResult ActivateModel(int id)
        {
            var islem = _databaseContext.UrunModelleri.Find(id);
            if (islem != null)
            {
                islem.Durum = true;
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("UrunModelleri"); // Sayfayı yenileyin
        }

        // Pasifleştirme işlemi
        [HttpPost]
        public IActionResult DeactivateModel(int id)
        {
            var islem = _databaseContext.UrunModelleri.Find(id);
            if (islem != null)
            {
                islem.Durum = false;
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("UrunModelleri"); // Sayfayı yenileyin
        }



            [HttpPost]
        public IActionResult ActivateKategoriler(int id)
        {
            var islem = _databaseContext.UrunKategorileri.Find(id);
            if (islem != null)
            {
                islem.Durum = true;
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("UrunKategorileri"); // Sayfayı yenileyin
        }

        // Pasifleştirme işlemi
        [HttpPost]
        public IActionResult DeactivateKategoriler(int id)
        {
            var islem = _databaseContext.UrunKategorileri.Find(id);
            if (islem != null)
            {
                islem.Durum = false;
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("UrunKategorileri"); // Sayfayı yenileyin
        }



    }
}





