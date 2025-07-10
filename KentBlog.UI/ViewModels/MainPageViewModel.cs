using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class MainPageViewModel
    {
        public AdminSettings? AdminSettings { get; set; }

        public List<OpeningPage>? OpeningPage { get; set; }
    }
}
