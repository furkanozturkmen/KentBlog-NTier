using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ViewModels
{
    public class ContactViewModel
    {
        public AdminSettings? AdminSettings { get; set; }
        public GeneralSettings? GeneralSettings { get; set; }
        public Menu? Menu { get; set; }
    }
}
