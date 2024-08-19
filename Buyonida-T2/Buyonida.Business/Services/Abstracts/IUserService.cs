using Buyonida.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Services.Abstracts
{
    public interface IUserService
    {
        Task Register(InitialRegisterDto registerDto);
    }
}
