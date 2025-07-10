using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Bu alan zorunludur.");
            RuleFor(x => x.SeoTitle).NotEmpty().WithMessage("Bu alan zorunludur.");

            // Yeni kayıt işleminde (Id = 0) resim zorunlu olsun
            When(x => x.CategoryID == 0, () =>
            {
                RuleFor(x => x.CategoryFile)
            .NotNull().WithMessage("Lütfen bir görsel yükleyin.")
            .Must(file =>
                file != null && new[] { ".jpg", ".jpeg", ".png" }
                .Contains(Path.GetExtension(file.FileName).ToLower())
            )
            .WithMessage("Sadece .jpg, .jpeg, .png veya dosyaları kabul edilir.");
            });

            // Güncelleme işleminde (Id != 0) eğer kullanıcı yeni resim seçmişse uzantı kontrolü yap
            When(x => x.CategoryID != 0 && x.CategoryFile != null, () =>
            {
                RuleFor(x => x.CategoryFile)
                    .Must(file =>
                        file != null && new[] { ".jpg", ".jpeg", ".png" }
                        .Contains(Path.GetExtension(file.FileName).ToLower())
                    )
                    .WithMessage("Sadece .jpg, .jpeg, .png dosyaları kabul edilir.");
            });


        }

    }
}
