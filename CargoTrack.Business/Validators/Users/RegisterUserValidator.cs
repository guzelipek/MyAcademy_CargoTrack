using CargoTrack.DTO.DTOs.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoTrack.Business.Validators.Users
{
    public class RegisterUserValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi girilmelidir.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş bırakılamaz.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrarı boş bırakılamaz.")
                .Matches(x => x.Password).WithMessage("Şifreler birbiriyle uyumlu değil.");
            RuleFor(x=>x.FirstName).NotEmpty().WithMessage("Ad boş bırakılamaz.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş bırakılamaz.");
        }
    }
}
