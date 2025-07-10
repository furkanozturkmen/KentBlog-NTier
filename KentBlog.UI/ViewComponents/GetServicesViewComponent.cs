using KentBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetServicesViewComponent : ViewComponent
    {

        private readonly IServicesService _servicesService;

        public GetServicesViewComponent(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _servicesService.GetListAll();
            return View(values);
        }
    }
}
