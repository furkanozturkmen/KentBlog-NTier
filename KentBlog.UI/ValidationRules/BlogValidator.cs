using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.SeoTitle).NotEmpty().WithMessage("Bu alan zorunludur.");

            // Yeni kayıt işleminde (Id = 0) resim zorunlu olsun
            When(x => x.Id == 0, () =>
            {
                RuleFor(x => x.BlogFile).NotNull().WithMessage("Lütfen bir görsel yükleyin.")
            .Must(file =>
                file != null && new[] { ".jpg", ".jpeg", ".png" }
                .Contains(Path.GetExtension(file.FileName).ToLower())
            )
            .WithMessage("Sadece .jpg, .jpeg, .png veya dosyaları kabul edilir.");
            });

            // Güncelleme işleminde (Id != 0) eğer kullanıcı yeni resim seçmişse uzantı kontrolü yap
            When(x => x.Id != 0 && x.BlogFile != null, () =>
            {
                RuleFor(x => x.BlogFile)
                    .Must(file =>
                        file != null && new[] { ".jpg", ".jpeg", ".png" }
                        .Contains(Path.GetExtension(file.FileName).ToLower())
                    )
                    .WithMessage("Sadece .jpg, .jpeg, .png dosyaları kabul edilir.");
            });

        }
    }
}
