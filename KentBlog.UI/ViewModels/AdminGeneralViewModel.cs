using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class AdminGeneralViewModel
    {
        public AdminSettings? AdminSettings { get; set; }
        public List<GeneralSettings>? GeneralSettings { get; set; }
    }
}
