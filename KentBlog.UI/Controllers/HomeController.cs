using System.Diagnostics;
using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAdminSettingsService _adminSettingsService;
        private readonly IOpeningPageService _openingPageService;

        public HomeController(IAdminSettingsService adminSettingsService, IOpeningPageService openingPageService)
        {
            _adminSettingsService = adminSettingsService;
            _openingPageService = openingPageService;
        }


        public IActionResult Index()
        {

            var adminsettings = _adminSettingsService.GetListAll().FirstOrDefault();
            var openingpage = _openingPageService.GetListAll();

            var Viewmodel = new MainPageViewModel
            {
                AdminSettings = adminsettings,
                OpeningPage = openingpage
            };

            ViewData["IsHomePage"] = true;
            return View(Viewmodel);
        }

    }
}
