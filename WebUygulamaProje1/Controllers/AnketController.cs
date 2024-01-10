using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using WebUygulamaProje1.Models;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Controllers
{

    public class AnketController : Controller
    {
        private readonly IAnketRepository _anketRepository;
        private readonly IAnketTuruRepository _anketTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public AnketController(IAnketRepository anketRepository, IAnketTuruRepository anketTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _anketRepository = anketRepository;
            _anketTuruRepository = anketTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Index()
        {
            List<Anket> objAnketList = _anketRepository.GetAll(includeProps: "AnketTuru").ToList();
            return View(objAnketList);
        }


        // Get
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> AnketTuruList = _anketTuruRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString()
                });
            ViewBag.AnketTuruList = AnketTuruList;

            if (id == null || id == 0)
            {
                // ekle
                return View();
            }
            else
            {
                // guncelleme
                Anket? anketVt = _anketRepository.Get(u => u.Id == id);  // Expression<Func<T, bool>> filtre
                if (anketVt == null)
                {
                    return NotFound();
                }
                return View(anketVt);
            }

        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(Anket anket, IFormFile? file)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string anketPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(anketPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    anket.ResimUrl = @"\img\" + file.FileName;
                }

                if (anket.Id == 0)
                {
                    _anketRepository.Ekle(anket);
                    TempData["basarili"] = "Yeni Anket başarıyla oluşturuldu!";
                }
                else
                {
                    _anketRepository.Guncelle(anket);
                    TempData["basarili"] = "Anket güncelleme başarılı!";
                }

                _anketRepository.Kaydet(); // SaveChanges() yapmazsanız bilgiler veri tabanına eklenmez!			
                return RedirectToAction("Index", "Anket");
            }
            return View();
        }



        // GET ACTION
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Anket? anketVt = _anketRepository.Get(u => u.Id == id);
            if (anketVt == null)
            {
                return NotFound();
            }
            return View(anketVt);
        }


        [HttpPost, ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPOST(int? id)
        {
            Anket? anket = _anketRepository.Get(u => u.Id == id);
            if (anket == null)
            {
                return NotFound();
            }
            _anketRepository.Sil(anket);
            _anketRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index", "Anket");
        }
    }
}
