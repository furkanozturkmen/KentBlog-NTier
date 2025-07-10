using KentBlog.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KentBlog.UI.ViewComponents
{
    public class GetTestimonialViewComponent : ViewComponent
    {
        private readonly ITestimonialsService _testimonialsService;
        public GetTestimonialViewComponent(ITestimonialsService testimonialsService) 
        {
            _testimonialsService = testimonialsService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _testimonialsService.GetListAll();
            return View(values);
        }
    }
}
