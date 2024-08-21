using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.DTOs.UserDTOs
{
    public record RegisterPersonalInfoDto
    {
        public string PersonalType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IDNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string MobilNumber { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string UserId { get; set; }
    }

    public class RegisterPersonalInfoDtoValidation:AbstractValidator<RegisterPersonalInfoDto>
    {
        public RegisterPersonalInfoDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(40).WithMessage("Name maximum length is 40");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(50).WithMessage("Name maximum length is 50");
        }
    }
}
