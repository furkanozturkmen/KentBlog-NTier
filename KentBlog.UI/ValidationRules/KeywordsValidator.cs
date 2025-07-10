using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class KeywordsValidator : AbstractValidator<Keywords>
    {
        public KeywordsValidator()
        {
            RuleFor(x => x.KeywordName).NotEmpty().WithMessage("Bu alan zorunludur.");
        }
    }
}
