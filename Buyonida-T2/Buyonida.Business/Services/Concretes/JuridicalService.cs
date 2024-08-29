using AutoMapper;
using Buyonida.Business.DTOs.UserDTOs;
using Buyonida.Business.Exceptions;
using Buyonida.Business.Services.Abstracts;
using Buyonida.Core.Entities;
using Buyonida.Data.Repositories.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Services.Concretes
{
    public class JuridicalService:IJuridicalService
    {
        private readonly IJuridicalRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public JuridicalService(IJuridicalRepository repository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddBankingInfoJuridicalUserAsync(JuridicalBankingInfoDto JuridicalBankingInfo)
        {
            var Juridical = await _repository.GetAsync(x => x.Id == JuridicalBankingInfo.UserId, null, true);
            if (Juridical == null)
            {
                throw new GlobalAppException("User not Found!");
            }
            Juridical.Voen = JuridicalBankingInfo.Voen;
            Juridical.BankVoen = JuridicalBankingInfo.BankVoen;
            Juridical.BankName = JuridicalBankingInfo.BankName;
            Juridical.Code = JuridicalBankingInfo.Code;
            Juridical.Mh = JuridicalBankingInfo.Mh;
            Juridical.SwiftBik = JuridicalBankingInfo.SwiftBik;
            Juridical.Hh = JuridicalBankingInfo.Hh;
            Juridical.Director = JuridicalBankingInfo.Director;
            Juridical.CompanyName = JuridicalBankingInfo.CompanyName;

            await _repository.CommitAsync();
        }

        public async Task CreateJuridicalUserAsync(RegisterPersonalInfoDto JuridicalInfo)
        {
            var user = await _userManager.FindByIdAsync(JuridicalInfo.UserId);
            if (user == null)
            {
                throw new GlobalAppException("User Not Found!");
            }

            Juridical Juridical = _mapper.Map<Juridical>(JuridicalInfo);

            Juridical.Id = user.Id;
            Juridical.Email = user.Email;
            Juridical.UserName = user.UserName;
            Juridical.LockoutEnd = user.LockoutEnd;
            Juridical.LockoutEnabled = user.LockoutEnabled;
            Juridical.TwoFactorEnabled = user.TwoFactorEnabled;
            Juridical.AccessFailedCount = user.AccessFailedCount;
            Juridical.ConcurrencyStamp = user.ConcurrencyStamp;
            Juridical.IsDeleted = user.IsDeleted;
            Juridical.EmailConfirmed = user.EmailConfirmed;
            Juridical.NormalizedEmail = user.NormalizedEmail;
            Juridical.NormalizedUserName = user.NormalizedUserName;
            Juridical.PasswordHash = user.PasswordHash;
            Juridical.PhoneNumber = user.PhoneNumber;
            Juridical.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            Juridical.SecurityStamp = user.SecurityStamp;


            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new GlobalAppException("User Deletion Failed!");
            }

            await _repository.AddAsync(Juridical);
            await _repository.CommitAsync();
        }

        public async Task DeleteJuridicalUserAsync(string userId)
        {
            var Juridical = await _repository.GetAsync(x => x.Id == userId, null, true);
            if (Juridical == null)
            {
                throw new GlobalAppException("Juridical is not found!");
            }

            Juridical.IsDeleted = true;
            Juridical.Email = null;
            Juridical.NormalizedEmail = null;
            await _repository.CommitAsync();
        }

        public async Task<List<JuridicalUserDto>> GetAllJuridicalUsersAsync(Expression<Func<Juridical, bool>>? func = null, Func<IQueryable<Juridical>, IIncludableQueryable<Juridical, object>>? include = null, Func<IQueryable<Juridical>, IOrderedQueryable<Juridical>>? orderBy = null, bool EnableTraking = false)
        {
            IList<Juridical> Juridicals = await _repository.GetAllAsync(func, include, orderBy, EnableTraking);
            List<JuridicalUserDto> dtos = new List<JuridicalUserDto>();
            foreach (var Juridical in Juridicals)
            {
                JuridicalUserDto dto = _mapper.Map<JuridicalUserDto>(Juridical);
                dtos.Add(dto);
            }
            return dtos;
        }

        public async Task<JuridicalUserDto> GetJuridicalUserAsync(Expression<Func<Juridical, bool>> func, Func<IQueryable<Juridical>, IIncludableQueryable<Juridical, object>>? include = null, bool EnableTraking = true)
        {
            Juridical Juridical = await _repository.GetAsync(func, include, EnableTraking);
            if (Juridical == null)
            {
                throw new GlobalAppException("Juridical is not found");
            }
            JuridicalUserDto dto = _mapper.Map<JuridicalUserDto>(Juridical);
            return dto;
        }

        public async Task<JuridicalUserDto> GetJuridicalUserByEmailAsync(string email)
        {
            Juridical Juridical = await _repository.GetAsync(x => x.Email == email);
            if (Juridical == null)
            {
                throw new GlobalAppException("Juridical is not found");
            }
            JuridicalUserDto dto = _mapper.Map<JuridicalUserDto>(Juridical);
            return dto;
        }

        public async Task<JuridicalUserDto> GetJuridicalUserByIdAsync(string userId)
        {
            Juridical Juridical = await _repository.GetAsync(x => x.Id == userId);
            if (Juridical == null)
            {
                throw new GlobalAppException("Juridical is not found");
            }
            JuridicalUserDto dto;
            try
            {

                dto = _mapper.Map<JuridicalUserDto>(Juridical);
            }
            catch (Exception ex)
            {
                throw new GlobalAppException("Cannot mapping");
            }
            return dto;
        }
    }
}
