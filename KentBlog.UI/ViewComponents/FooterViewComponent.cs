using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {

        private readonly IGeneralSettingsService _generalSettingsService;

        public FooterViewComponent(IGeneralSettingsService generalsettingsService)
        {
            _generalSettingsService = generalsettingsService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _generalSettingsService.GetListAll().FirstOrDefault();
            return View(values);
        }
    }
}
