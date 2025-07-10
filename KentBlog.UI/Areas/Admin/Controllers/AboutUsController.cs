using FluentValidation;
using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AboutUsController : Controller
    {

        private readonly IAboutUsService _aboutUsService;
        private readonly IValidator<AboutUs> _validator;

        public AboutUsController(IAboutUsService aboutUsService, IValidator<AboutUs> validator)
        {
            _aboutUsService = aboutUsService;
            _validator = validator;
        }

        public IActionResult Index()
        {
            var values = _aboutUsService.GetListAll().OrderBy(m => m.Order).ToList();
            return View(values);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AboutUs item)
        {
            var result = _validator.Validate(item);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(item);
            }

            try
            {
                if (item.AboutFile != null && item.AboutFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(item.AboutFile.FileName).ToLower();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.AboutFile.FileName)
                            .Replace(" ", "-").ToLower();
                        var uniqueName = $"{fileNameWithoutExtension}-{Guid.NewGuid():N}.{fileExtension.TrimStart('.')}";

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "aboutus", uniqueName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.AboutFile.CopyTo(stream);
                        }

                        item.Image = "/images/aboutus/" + uniqueName;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Geçersiz dosya formatı.";
                        return View(item);
                    }
                }

                _aboutUsService.TAdd(item);
                TempData["SuccessMessage"] = "İşlem başarıyla tamamlandı.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Hata: " + ex.Message;
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {

            var item = _aboutUsService.GetByID(id);

            try
            {
                _aboutUsService.TDelete(item);
                TempData["SuccessMessage"] = "İşlem başarıyla tamamlandı";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bilinmeyen bir hata oluştu: " + ex.Message;
            }


            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {

            var menu = _aboutUsService.GetByID(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Öğe bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AboutUs pageitem)
        {

            var result = _validator.Validate(pageitem);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                TempData["ErrorMessage"] = "Lütfen formu eksiksiz ve doğru şekilde doldurun.";
                return View(pageitem);
            }

            try
            {
                var existingItem = _aboutUsService.GetByID(pageitem.Id);

                if (existingItem != null)
                {
                    if (pageitem.AboutFile != null && pageitem.AboutFile.Length > 0)
                    {
                        var fileExtension = Path.GetExtension(pageitem.AboutFile.FileName).ToLower();
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            // Dosya adını güvenli ve benzersiz hale getir
                            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pageitem.AboutFile.FileName);
                            fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "-").ToLower();
                            var uniqueName = fileNameWithoutExtension + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fileExtension;

                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "aboutus", uniqueName);

                            // Eğer daha önce bir resim kaydedilmişse eski dosyayı silebilirsin (isteğe bağlı)
                            if (!string.IsNullOrEmpty(existingItem.Image))
                            {
                                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.Image.TrimStart('/'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                pageitem.AboutFile.CopyTo(stream);
                            }

                            // Yeni resmi kaydet
                            existingItem.Image = "/images/aboutus/" + uniqueName;
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .jpg, .jpeg veya .png dosyası yükleyin.";
                            return RedirectToAction("Index");
                        }
                    }

                    // Diğer alanların güncellenmesi
                    existingItem.Title = pageitem.Title;
                    existingItem.Icon = pageitem.Icon;
                    existingItem.Description = pageitem.Description;
                    existingItem.Status = pageitem.Status;


                    _aboutUsService.TUpdate(existingItem);

                    TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı";
                }
                else
                {
                    TempData["ErrorMessage"] = "Öğe bulunamadı";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }





        [HttpPost]
        public IActionResult UpdateOrder(List<int> orderedIds)
        {
            if (orderedIds != null && orderedIds.Count > 0)
            {


                for (int i = 0; i < orderedIds.Count; i++)
                {
                    var MainItem = _aboutUsService.GetByID(orderedIds[i]);
                    if (MainItem != null)
                    {
                        MainItem.Order = i;
                        _aboutUsService.TUpdate(MainItem);
                       
                    }
                }
                
                return Json(new { success = true, message = "İşlem başarıyla tamamlandı." });
               
            }
            return Json(new { success = false, message = "Bilinmeyen bir hata oluştu" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(int id)
        {

            var item = _aboutUsService.GetByID(id);

            if (item != null)
            {
                item.Status = !item.Status; // True ise False yap, False ise True yap
                _aboutUsService.TUpdate(item);
                return Json(new { success = true, newStatus = item.Status });
            }

            return Json(new { success = false });
        }


    }
}
