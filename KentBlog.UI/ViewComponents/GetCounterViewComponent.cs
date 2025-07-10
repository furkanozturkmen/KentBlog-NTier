using KentBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetCounterViewComponent : ViewComponent
    {
        private readonly ICounterService _counterService;

        public GetCounterViewComponent(ICounterService counterService)
        {
            _counterService = counterService;
        }


        public IViewComponentResult Invoke()
        {
            var values = _counterService.GetListAll();
            return View(values);
        }
    }
}
