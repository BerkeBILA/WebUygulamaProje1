using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class AnketTuruController : Controller
    {
        private readonly IAnketTuruRepository _anketTuruRepository;

        public AnketTuruController(IAnketTuruRepository context)
        {
            _anketTuruRepository = context;
        }

        public IActionResult Index()
        {
            List<AnketTuru> objAnketTuruList = _anketTuruRepository.GetAll().ToList();
            return View(objAnketTuruList);
        }


        public IActionResult Ekle()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Ekle(AnketTuru anketTuru)
        {
            if (ModelState.IsValid)
            {
                _anketTuruRepository.Ekle(anketTuru);
                _anketTuruRepository.Kaydet(); // SaveChanges() yapmazsanız bilgiler veri tabanına eklenmez!
                TempData["basarili"] = "Yeni Anket Türü başarıyla oluşturuldu!";
                return RedirectToAction("Index", "AnketTuru");
            }
            return View();
        }



        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            AnketTuru? anketTuruVt = _anketTuruRepository.Get(u => u.Id == id);  // Expression<Func<T, bool>> filtre
            if (anketTuruVt == null)
            {
                return NotFound();
            }
            return View(anketTuruVt);
        }

        [HttpPost]
        public IActionResult Guncelle(AnketTuru anketTuru)
        {
            if (ModelState.IsValid)
            {
                _anketTuruRepository.Guncelle(anketTuru);
                _anketTuruRepository.Kaydet(); // SaveChanges() yapmazsanız bilgiler veri tabanına eklenmez!
                TempData["basarili"] = "Yeni Anket Türü başarıyla güncellendi!";
                return RedirectToAction("Index", "AnketTuru");
            }
            return View();
        }


        // GET ACTION
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            AnketTuru? anketTuruVt = _anketTuruRepository.Get(u => u.Id == id);
            if (anketTuruVt == null)
            {
                return NotFound();
            }
            return View(anketTuruVt);
        }


        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            AnketTuru? anketTuru = _anketTuruRepository.Get(u => u.Id == id);
            if (anketTuru == null)
            {
                return NotFound();
            }
            _anketTuruRepository.Sil(anketTuru);
            _anketTuruRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index", "AnketTuru");
        }
    }
}
