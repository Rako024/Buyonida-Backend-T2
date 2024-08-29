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
    public interface IInvidualService
    {
        Task CreateInvidualUserAsync(RegisterPersonalInfoDto InvidualInfo);
        Task AddBankingInfoInvidualUserAsync(InvidualBankingInfoDto InvidualBankingInfo);
        Task DeleteInvidualUserAsync(string userId);
        Task<InvidualUserDto> GetInvidualUserByIdAsync(string userId);
        Task<InvidualUserDto> GetInvidualUserByEmailAsync(string email);
        Task<InvidualUserDto> GetInvidualUserAsync
            (
            Expression<Func<Invidual, bool>> func,
            Func<IQueryable<Invidual>, IIncludableQueryable<Invidual, object>>? include = null,
            bool EnableTraking = true
            );
        Task<List<InvidualUserDto>> GetAllInvidualUsersAsync
            (
            Expression<Func<Invidual, bool>>? func = null,
            Func<IQueryable<Invidual>, IIncludableQueryable<Invidual, object>>? include = null,
            Func<IQueryable<Invidual>, IOrderedQueryable<Invidual>>? orderBy = null,
            bool EnableTraking = false
            );
    }
}
