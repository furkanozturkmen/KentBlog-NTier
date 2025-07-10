using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class MenuValidator: AbstractValidator<Menu>
    {
        public MenuValidator()
        {
            RuleFor(x => x.PageType).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.SeoTitle).NotEmpty().WithMessage("Bu alan zorunludur.").When(x => x.PageType == "1"); ;
         
            RuleFor(x => x.MenuAdres).NotEmpty().WithMessage("Menü adresi zorunludur.").When(x => x.PageType == "2");
        }
    }
}
