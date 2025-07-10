using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class AdminSettingsValidator : AbstractValidator<AdminSettings>
    {
        public AdminSettingsValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.MetaDescription).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.MetaKeyword).NotEmpty().WithMessage("Bu alan zorunludur.");
        }
    }
}
