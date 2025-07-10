using KentBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetAboutusViewComponent : ViewComponent
    {
        private readonly IAboutUsService _aboutUsService;

        public GetAboutusViewComponent(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _aboutUsService.GetListAll();
            return View(values);
        }
    }
}
