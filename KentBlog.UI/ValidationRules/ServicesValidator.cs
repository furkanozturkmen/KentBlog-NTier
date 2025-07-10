using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class ServicesValidator : AbstractValidator<Services>
    {
        public ServicesValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu alan zorunludur.");
        }
    }
}
