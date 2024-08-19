using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Buyonida.Business.DTOs.UserDTOs
{
    public record InitialRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }


    public class InitialRegisterDtoValidation:AbstractValidator<InitialRegisterDto>
    {
        public InitialRegisterDtoValidation()
        {
            RuleFor(x => x.Email).NotEmpty()
            .WithMessage("The email cannot be empty!")
            .EmailAddress()
            .WithMessage("Email format is not correct!").Must(u => !u.Contains(" "))
            .WithMessage("The Email cannot contain spaces!");

            RuleFor(x => x.Password).NotEmpty()
            .WithMessage("The password cannot be empty!")
            .Must(r =>
            {
                Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{6,}$");
                Match match = passwordRegex.Match(r);
                return match.Success;
            }).WithMessage("Password format is not correct!")
            .Must(p => !p.Contains(" "))
            .WithMessage("The password cannot contain spaces!");
        }
    }
}
