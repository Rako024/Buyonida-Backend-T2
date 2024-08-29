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
    public interface IJuridicalService
    {
        Task CreateJuridicalUserAsync(RegisterPersonalInfoDto JuridicalInfo);
        Task AddBankingInfoJuridicalUserAsync(JuridicalBankingInfoDto JuridicalBankingInfo);
        Task DeleteJuridicalUserAsync(string userId);
        Task<JuridicalUserDto> GetJuridicalUserByIdAsync(string userId);
        Task<JuridicalUserDto> GetJuridicalUserByEmailAsync(string email);
        Task<JuridicalUserDto> GetJuridicalUserAsync
            (
            Expression<Func<Juridical, bool>> func,
            Func<IQueryable<Juridical>, IIncludableQueryable<Juridical, object>>? include = null,
            bool EnableTraking = true
            );
        Task<List<JuridicalUserDto>> GetAllJuridicalUsersAsync
            (
            Expression<Func<Juridical, bool>>? func = null,
            Func<IQueryable<Juridical>, IIncludableQueryable<Juridical, object>>? include = null,
            Func<IQueryable<Juridical>, IOrderedQueryable<Juridical>>? orderBy = null,
            bool EnableTraking = false
            );
    }
}
