using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.DTOs.UserDTOs
{
    public record LoginDto
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidation : AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(x => x.UserNameOrEmail).NotEmpty()
              .WithMessage("The Username/Email cannot be empty!");
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password cannot be empty!");
        }
    }
}
