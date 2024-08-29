using AutoMapper;
using Buyonida.Business.DTOs.UserDTOs;
using Buyonida.Business.Exceptions;
using Buyonida.Business.Services.Abstracts;
using Buyonida.Core.Entities;
using Buyonida.Data.Repositories.Abstracts;
using Buyonida.Data.Repositories.Concretes;
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
    public class InvidualService : IInvidualService
    {
        private readonly IInvidualRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public InvidualService(IInvidualRepository repository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddBankingInfoInvidualUserAsync(InvidualBankingInfoDto InvidualBankingInfo)
        {
            var invidual = await _repository.GetAsync(x => x.Id == InvidualBankingInfo.UserId, null, true);
            if (invidual == null)
            {
                throw new GlobalAppException("User not Found!");
            }
            invidual.Voen = InvidualBankingInfo.Voen;
            invidual.BankVoen = InvidualBankingInfo.BankVoen;
            invidual.BankName = InvidualBankingInfo.BankName;
            invidual.Code = InvidualBankingInfo.Code;
            invidual.Mh = InvidualBankingInfo.Mh;
            invidual.SwiftBik = InvidualBankingInfo.SwiftBik;
            invidual.Hh = InvidualBankingInfo.Hh;

            await _repository.CommitAsync();
        }

        public async Task CreateInvidualUserAsync(RegisterPersonalInfoDto InvidualInfo)
        {
            var user = await _userManager.FindByIdAsync(InvidualInfo.UserId);
            if (user == null)
            {
                throw new GlobalAppException("User Not Found!");
            }

            Invidual invidual = _mapper.Map<Invidual>(InvidualInfo);

            invidual.Id = user.Id;
            invidual.Email = user.Email;
            invidual.UserName = user.UserName;
            invidual.LockoutEnd = user.LockoutEnd;
            invidual.LockoutEnabled = user.LockoutEnabled;
            invidual.TwoFactorEnabled = user.TwoFactorEnabled;
            invidual.AccessFailedCount = user.AccessFailedCount;
            invidual.ConcurrencyStamp = user.ConcurrencyStamp;
            invidual.IsDeleted = user.IsDeleted;
            invidual.EmailConfirmed = user.EmailConfirmed;
            invidual.NormalizedEmail = user.NormalizedEmail;
            invidual.NormalizedUserName = user.NormalizedUserName;
            invidual.PasswordHash = user.PasswordHash;
            invidual.PhoneNumber = user.PhoneNumber;
            invidual.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            invidual.SecurityStamp = user.SecurityStamp;


            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new GlobalAppException("User Deletion Failed!");
            }

            await _repository.AddAsync(invidual);
            await _repository.CommitAsync();
        }

        public async Task DeleteInvidualUserAsync(string userId)
        {
            var invidual = await _repository.GetAsync(x => x.Id == userId, null, true);
            if (invidual == null)
            {
                throw new GlobalAppException("Invidual is not found!");
            }

            invidual.IsDeleted = true;
            invidual.Email = null;
            invidual.NormalizedEmail = null;
            await _repository.CommitAsync();
        }

        public async Task<List<InvidualUserDto>> GetAllInvidualUsersAsync(Expression<Func<Invidual, bool>>? func = null, Func<IQueryable<Invidual>, IIncludableQueryable<Invidual, object>>? include = null, Func<IQueryable<Invidual>, IOrderedQueryable<Invidual>>? orderBy = null, bool EnableTraking = false)
        {
            IList<Invidual> inviduals = await _repository.GetAllAsync(func, include, orderBy, EnableTraking);
            List<InvidualUserDto> dtos = new List<InvidualUserDto>();
            foreach (var invidual in inviduals)
            {
                InvidualUserDto dto = _mapper.Map<InvidualUserDto>(invidual);
                dtos.Add(dto);
            }
            return dtos;
        }

        public async Task<InvidualUserDto> GetInvidualUserAsync(Expression<Func<Invidual, bool>> func, Func<IQueryable<Invidual>, IIncludableQueryable<Invidual, object>>? include = null, bool EnableTraking = true)
        {
            Invidual Invidual = await _repository.GetAsync(func, include, EnableTraking);
            if (Invidual == null)
            {
                throw new GlobalAppException("Invidual is not found");
            }
            InvidualUserDto dto = _mapper.Map<InvidualUserDto>(Invidual);
            return dto;
        }

        public async Task<InvidualUserDto> GetInvidualUserByEmailAsync(string email)
        {
            Invidual Invidual = await _repository.GetAsync(x=>x.Email == email);
            if (Invidual == null)
            {
                throw new GlobalAppException("Invidual is not found");
            }
            InvidualUserDto dto = _mapper.Map<InvidualUserDto>(Invidual);
            return dto;
        }

        public async Task<InvidualUserDto> GetInvidualUserByIdAsync(string userId)
        {
            Invidual Invidual = await _repository.GetAsync(x => x.Id == userId);
            if (Invidual == null)
            {
                throw new GlobalAppException("Invidual is not found");
            }
            InvidualUserDto dto;
            try
            {
                dto = _mapper.Map<InvidualUserDto>(Invidual);

            }catch (Exception ex)
            {
                throw new GlobalAppException("Cannot mapping");
            }
            return dto;
        }
    }
}
