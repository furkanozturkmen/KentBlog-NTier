using KentBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetCopyrightsViewComponent : ViewComponent
    {
        private readonly IAdminSettingsService _adminSettingsService;

        public GetCopyrightsViewComponent(IAdminSettingsService adminSettingsService)
        {
            _adminSettingsService = adminSettingsService;
        }


        public IViewComponentResult Invoke()
        {
            var values = _adminSettingsService.GetListAll().FirstOrDefault();
            return View(values);
        }
    }
}
