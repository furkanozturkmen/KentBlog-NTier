using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class GeneralSettingsValidator : AbstractValidator<GeneralSettings>
    {
        public GeneralSettingsValidator()
        {
            RuleFor(x => x.BusinessName).NotEmpty().WithMessage("Bu alan zorunludur.");
        }
    }
}
