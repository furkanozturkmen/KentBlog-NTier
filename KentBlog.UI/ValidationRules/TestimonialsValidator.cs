using FluentValidation;
using KentBlog.Entity.Concrete;

namespace KentBlog.UI.ValidationRules
{
    public class TestimonialsValidator : AbstractValidator<Testimonials>
    {
        public TestimonialsValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan zorunludur.");

            // Yeni kayıt işleminde (Id = 0) resim zorunlu olsun
            When(x => x.Id == 0, () =>
            {
                RuleFor(x => x.TestimonialFile).NotNull().WithMessage("Lütfen bir görsel yükleyin.")
                .Must(file =>
                    file != null && new[] { ".jpg", ".jpeg", ".png" }
                    .Contains(Path.GetExtension(file.FileName).ToLower())
                )
                .WithMessage("Sadece .jpg, .jpeg, .png veya dosyaları kabul edilir.");
            });

            // Güncelleme işleminde (Id != 0) eğer kullanıcı yeni resim seçmişse uzantı kontrolü yap
            When(x => x.Id != 0 && x.TestimonialFile != null, () =>
            {
                RuleFor(x => x.TestimonialFile)
                    .Must(file =>
                        file != null && new[] { ".jpg", ".jpeg", ".png" }
                        .Contains(Path.GetExtension(file.FileName).ToLower())
                    )
                    .WithMessage("Sadece .jpg, .jpeg, .png dosyaları kabul edilir.");
            });

        }
    }
}
