using FluentValidation;
using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialsService _testimonialsService;
        private readonly IValidator<Testimonials> _validator;

        public TestimonialController(ITestimonialsService testimonialsService, IValidator<Testimonials> validator)
        {
            _testimonialsService = testimonialsService;
            _validator = validator;
        }

        public IActionResult Index()
        {
            var values = _testimonialsService.GetListAll().OrderBy(m => m.Order).ToList();
            return View(values);
        }


        [HttpGet]
        public IActionResult Create()
        {

            var menus = _testimonialsService.GetListAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Testimonials item)
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
                if (item.TestimonialFile != null && item.TestimonialFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(item.TestimonialFile.FileName).ToLower();
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        // Dosya adını güvenli ve benzersiz hale getir
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.TestimonialFile.FileName);
                        fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "-").ToLower();
                        var uniqueName = fileNameWithoutExtension + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fileExtension;

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "testimonial", uniqueName);

                        // Aynı isimde varsa sil
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.TestimonialFile.CopyTo(stream);
                        }

                        item.Avatar = "/images/testimonial/" + uniqueName;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .jpg, .jpeg, .png veya .gif dosyası yükleyin.";
                        return RedirectToAction("Index");
                    }
                }

                _testimonialsService.TAdd(item);
                TempData["SuccessMessage"] = "İşlem başarıyla tamamlandı";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bilinmeyen bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {

            var item = _testimonialsService.GetByID(id);

            try
            {
                _testimonialsService.TDelete(item);
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

            var menu = _testimonialsService.GetByID(id);
            if (menu == null)
            {
                TempData["ErrorMessage"] = "Öğe bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Testimonials pageitem)
        {

            var result = _validator.Validate(pageitem);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(pageitem);
            }


            try
            {
                var existingItem = _testimonialsService.GetByID(pageitem.Id);

                if (existingItem != null)
                {
                    if (pageitem.TestimonialFile != null && pageitem.TestimonialFile.Length > 0)
                    {
                        var fileExtension = Path.GetExtension(pageitem.TestimonialFile.FileName).ToLower();
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            // Dosya adını güvenli ve benzersiz hale getir
                            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pageitem.TestimonialFile.FileName);
                            fileNameWithoutExtension = fileNameWithoutExtension.Replace(" ", "-").ToLower();
                            var uniqueName = fileNameWithoutExtension + "-" + Guid.NewGuid().ToString().Substring(0, 8) + fileExtension;

                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "testimonial", uniqueName);

                            // Eğer daha önce bir resim kaydedilmişse eski dosyayı silebilirsin (isteğe bağlı)
                            if (!string.IsNullOrEmpty(existingItem.Avatar))
                            {
                                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingItem.Avatar.TrimStart('/'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                pageitem.TestimonialFile.CopyTo(stream);
                            }

                            // Yeni resmi kaydet
                            existingItem.Avatar = "/images/testimonial/" + uniqueName;
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .jpg, .jpeg veya .png dosyası yükleyin.";
                            return RedirectToAction("Index");
                        }
                    }

                    // Diğer alanların güncellenmesi
                    existingItem.Status = pageitem.Status;
                    existingItem.Name = pageitem.Name;
                    existingItem.Description = pageitem.Description;


                    _testimonialsService.TUpdate(existingItem);

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
                    var MainItem = _testimonialsService.GetByID(orderedIds[i]);
                    if (MainItem != null)
                    {
                        MainItem.Order = i;
                        _testimonialsService.TUpdate(MainItem);
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

            var item = _testimonialsService.GetByID(id);

            if (item != null)
            {
                item.Status = !item.Status; // True ise False yap, False ise True yap
                _testimonialsService.TUpdate(item);
                return Json(new { success = true, newStatus = item.Status });
            }

            return Json(new { success = false });
        }
    }
}
