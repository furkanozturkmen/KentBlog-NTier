using KentBlog.Business.Abstract;
using KentBlog.Business.Concrete;
using KentBlog.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetKeywordsViewComponent : ViewComponent
    {
        private readonly IKeywordsService _keywordsService;

        public GetKeywordsViewComponent(IKeywordsService keywordsService)
        {
            _keywordsService = keywordsService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _keywordsService.GetListAll();
            return View(values);
        }
    }
}
