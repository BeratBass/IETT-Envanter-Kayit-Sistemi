using iettproje.Data;
using iettproje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iettproje.Controllers
{
    [Authorize]
    public class EditController : Controller
    {
        private readonly AppDbContext _databaseContext;


        public EditController(AppDbContext appDbContext)
        {
            _databaseContext = appDbContext;
        }

        [HttpGet]
        public IActionResult EditIslemler(int id)
        {
            var islem = _databaseContext.IslemlerList.FirstOrDefault(i => i.Id == id);
            if (islem == null)
            {
                return NotFound();
            }

            var viewModel = new IslemlerListVM
            {
                Id = islem.Id,
                Sicil = islem.Sicil,
                Marka = islem.Marka,
                Modeli = islem.Modeli,
                Tarih = islem.Tarih,
                Adet = islem.Adet,
                Modeller = _databaseContext.UrunModelleri.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateIslemler(IslemlerListVM model)
        {
            if (ModelState.IsValid)
            {
                var islem = _databaseContext.IslemlerList.FirstOrDefault(i => i.Id == model.Id);
                if (islem == null)
                {
                    return NotFound();
                }

                islem.Sicil = model.Sicil;
                islem.Marka = model.Marka;
                islem.Modeli = model.Modeli;
                islem.Tarih = model.Tarih;
                islem.Adet = model.Adet;

                _databaseContext.SaveChanges();

                TempData["SuccessMessage"] = "Kayıt başarıyla güncellenmiştir.";
                return RedirectToAction("Islemler","Islemler"); 
            }

            model.Modeller = _databaseContext.UrunModelleri.ToList();
            return View("EditIslemler",model);
        }


        [HttpGet]
        public IActionResult EditKategoriler(int id)
        {
            var islem = _databaseContext.UrunKategorileri.FirstOrDefault(i => i.Id == id);
            if (islem == null)
            {
                return NotFound();
            }

            var viewModel = new UrunlerVM
            {
                Id = islem.Id,
                Urunler = islem.Urunler
                
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateKategoriler(UrunlerVM model)
        {
            if (ModelState.IsValid)
            {
                var islem = _databaseContext.UrunKategorileri.FirstOrDefault(i => i.Id == model.Id);
                if (islem == null)
                {
                    return NotFound();
                }

                islem.Urunler = model.Urunler;
             
                _databaseContext.SaveChanges();

                TempData["SuccessMessage"] = "Kayıt başarıyla güncellenmiştir.";
                return RedirectToAction("UrunKategorileri","Islemler");
            }

            return View("EditKategoriler", model);
        }

        [HttpGet]
        public IActionResult EditModeller(int id)
        {
            var islem = _databaseContext.UrunModelleri.FirstOrDefault(i => i.Id == id);
            if (islem == null)
            {
                return NotFound();
            }

            var viewModel = new UrunModelleriVM
            {
                Id = islem.Id,
                Marka = islem.Marka,
                Modeli = islem.Modeli,
                Kategorisi=islem.Kategorisi,
                Kategoriler = _databaseContext.UrunKategorileri.ToList()

            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateModeller(UrunModelleriVM model)
        {
            if (ModelState.IsValid)
            {
                var islem = _databaseContext.UrunModelleri.FirstOrDefault(i => i.Id == model.Id);
                if (islem == null)
                {
                    return NotFound();
                }

                islem.Marka = model.Marka;
                islem.Modeli = model.Modeli;
                islem.Kategorisi = model.Kategorisi;

                _databaseContext.SaveChanges();

                TempData["SuccessMessage"] = "Kayıt başarıyla güncellenmiştir.";
                return RedirectToAction("UrunModelleri", "Islemler");
            }

            model.Kategoriler = _databaseContext.UrunKategorileri.ToList();
            return View("EditModeller", model);
        }
    }
}

