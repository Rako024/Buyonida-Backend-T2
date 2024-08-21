using AutoMapper;
using Buyonida.Business.DTOs.UserDTOs;
using Buyonida.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Profilies
{
    public class UserMapProfilies:Profile
    {
        public UserMapProfilies()
        {
            CreateMap<InitialRegisterDto, AppUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<PersonalUserDto, Personal>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.IDNumber, opt => opt.MapFrom(src => src.IDNumber))
                .ForMember(d => d.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(d => d.MobilNumber, opt => opt.MapFrom(src => src.MobilNumber))
                .ForMember(d => d.Adress, opt => opt.MapFrom(src => src.Adress))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City))
                .ForMember(d => d.District, opt => opt.MapFrom(src => src.District))
                .ForMember(d => d.NameOnCard, opt => opt.MapFrom(src => src.NameOnCard))
                .ForMember(d => d.CardNumber, opt => opt.MapFrom(src => src.CardNumber));

            CreateMap<Personal, PersonalUserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.IDNumber, opt => opt.MapFrom(src => src.IDNumber))
                .ForMember(d => d.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(d => d.MobilNumber, opt => opt.MapFrom(src => src.MobilNumber))
                .ForMember(d => d.Adress, opt => opt.MapFrom(src => src.Adress))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City))
                .ForMember(d => d.District, opt => opt.MapFrom(src => src.District))
                .ForMember(d => d.NameOnCard, opt => opt.MapFrom(src => src.NameOnCard))
                .ForMember(d => d.CardNumber, opt => opt.MapFrom(src => src.CardNumber));


            CreateMap<RegisterPersonalInfoDto, Personal>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.IDNumber, opt => opt.MapFrom(src => src.IDNumber))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.MobilNumber, opt => opt.MapFrom(src => src.MobilNumber))
                .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District));

        }
    }
}
