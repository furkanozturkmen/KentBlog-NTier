using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class CounterValidator : AbstractValidator<Counter>
    {
        public CounterValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.Number).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.Symbol).NotEmpty().WithMessage("Bu alan zorunludur.");
        }
    }
}
