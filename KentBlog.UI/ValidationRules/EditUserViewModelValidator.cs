using FluentValidation;
using KentBlog.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.UI.ValidationRules
{
    public class EditUserViewModelValidator : AbstractValidator<EditUserViewModel>
    {
        public EditUserViewModelValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad soyad alanı zorunludur.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı zorunludur.");

            RuleFor(x => x.SelectedRole)
                .NotEmpty().WithMessage("Rol seçimi zorunludur.");


            When(x => !string.IsNullOrEmpty(x.Password) || !string.IsNullOrEmpty(x.ConfirmPassword), () =>
            {
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
                    .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

                RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor.");
            });


        }
    }
}
