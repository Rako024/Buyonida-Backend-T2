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
       
        }
    }
}
