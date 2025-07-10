using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class AboutUsValidator : AbstractValidator<AboutUs>
    {
        public AboutUsValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.");

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Lütfen bir icon ismi yazınız.");


            // Yeni kayıt işleminde (Id = 0) resim zorunlu olsun
            When(x => x.Id == 0, () =>
            {
                RuleFor(x => x.AboutFile)
            .NotNull().WithMessage("Lütfen bir görsel yükleyin.")
            .Must(file =>
                file != null && new[] { ".jpg", ".jpeg", ".png" }
                .Contains(Path.GetExtension(file.FileName).ToLower())
            )
            .WithMessage("Sadece .jpg, .jpeg, .png veya dosyaları kabul edilir.");
            });

            // Güncelleme işleminde (Id != 0) eğer kullanıcı yeni resim seçmişse uzantı kontrolü yap
            When(x => x.Id != 0 && x.AboutFile != null, () =>
            {
                RuleFor(x => x.AboutFile)
                    .Must(file =>
                        file != null && new[] { ".jpg", ".jpeg", ".png" }
                        .Contains(Path.GetExtension(file.FileName).ToLower())
                    )
                    .WithMessage("Sadece .jpg, .jpeg, .png dosyaları kabul edilir.");
            });

        }
    }
}
