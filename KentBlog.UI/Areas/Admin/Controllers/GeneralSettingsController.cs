using FluentValidation;
using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GeneralSettingsController : Controller
    {

        private readonly IGeneralSettingsService _generalSettingsService;
        private readonly IValidator<GeneralSettings> _generalSettingsValidator;

        public GeneralSettingsController(IGeneralSettingsService generalSettingsService, IValidator<GeneralSettings> validator)
        {
         _generalSettingsService = generalSettingsService;
         _generalSettingsValidator = validator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var values = _generalSettingsService.GetListAll().FirstOrDefault();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(GeneralSettings model)
        {

            var result = _generalSettingsValidator.Validate(model);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(model);
            }


            if (model.LogoFile != null && model.LogoFile.Length > 0)
            {
                var fileExtension = Path.GetExtension(model.LogoFile.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                if (allowedExtensions.Contains(fileExtension))
                {
                    var fileName = "logo" + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logos", fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.LogoFile.CopyTo(stream);
                    }

                    var existingSettings = _generalSettingsService.GetListAll().FirstOrDefault();

                    if (existingSettings != null)
                    {
                        existingSettings.Logo = "/images/logos/" + fileName;
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Geçersiz dosya formatı. Lütfen .jpg, .jpeg, .png veya .gif dosyası yükleyin.";
                    return RedirectToAction("Index");
                }
            }

            var settings = _generalSettingsService.GetListAll().FirstOrDefault();

            if (settings != null)
            {
                settings.BusinessName = model.BusinessName;
                settings.Mail = model.Mail;
                settings.Mail2 = model.Mail2;
                settings.Phone = model.Phone;
                settings.Phone2 = model.Phone2;
                settings.Fax = model.Fax;
                settings.Fax2 = model.Fax2;
                settings.Address = model.Address;
                settings.Address2 = model.Address2;
                settings.GoogleMaps = model.GoogleMaps;
                settings.Facebook = model.Facebook;
                settings.Twitter = model.Twitter;
                settings.Instagram = model.Instagram;
                settings.WhatsApp = model.WhatsApp;
                settings.YouTube = model.YouTube;
                settings.LinkedIn = model.LinkedIn;
                settings.Pinterest = model.Pinterest;
                settings.AboutUs = model.AboutUs;

                _generalSettingsService.TUpdate(settings);
                TempData["SuccessMessage"] = "İşlem Başarıyla Tamamlandı!";
            }
            else
            {
                TempData["ErrorMessage"] = "Genel ayarlar bulunamadı.";
            }

            return RedirectToAction("Index");
        }
    }
}
