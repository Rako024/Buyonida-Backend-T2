using Buyonida.Business.DTOs.UserDTOs;
using Buyonida.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Services.Abstracts
{
    public interface ITokenService
    {
        TokenResponseDto CreateToken(AppUser user, int expireDate = 1440);
    }
}
