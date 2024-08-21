using Buyonida.Business.DTOs.UserDTOs;
using Buyonida.Core.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Services.Abstracts
{
    public interface IPersonalService
    {
        Task CreatePersonalUserAsync(RegisterPersonalInfoDto personalInfo);
        Task AddBankingInfoPersonalUserAsync(PersonalBankingInfoDto personalBankingInfo);
        Task DeletePersonalUserAsync(string userId);
        Task<PersonalUserDto> GetPersonalUserByIdAsync(string userId);
        Task<PersonalUserDto> GetPersonalUserByEmailAsync(string email);
        Task<PersonalUserDto> GetPersonalUserAsync
            (
            Expression<Func<Personal, bool>> func,
            Func<IQueryable<Personal>, IIncludableQueryable<Personal, object>>? include = null,
            bool EnableTraking = true
            );
        Task<List<PersonalUserDto>> GetAllPersonalUsersAsync
            (
            Expression<Func<Personal, bool>>? func = null,
            Func<IQueryable<Personal>, IIncludableQueryable<Personal, object>>? include = null,
            Func<IQueryable<Personal>, IOrderedQueryable<Personal>>? orderBy = null,
            bool EnableTraking = false
            );
    }
}
