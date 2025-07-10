using KentBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetSliderWithBlogsViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public GetSliderWithBlogsViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _blogService.GetBlogsWithCategory();
            return View(values);
        }
    }
}

