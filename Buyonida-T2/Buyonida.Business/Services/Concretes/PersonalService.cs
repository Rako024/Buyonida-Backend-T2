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
    public class PersonalService : IPersonalService
    {
        private readonly IPersonalRepository _personalRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public PersonalService(IPersonalRepository personalRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _personalRepository = personalRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddBankingInfoPersonalUserAsync(PersonalBankingInfoDto personalBankingInfo)
        {
            var personal = await _personalRepository.GetAsync(x => x.Id == personalBankingInfo.UserId, null, true);
            if (personal == null)
            {
                throw new GlobalAppException("User not Found!");
            }
            personal.NameOnCard = personalBankingInfo.NameOnCard;
            personal.CardNumber = personalBankingInfo.CardNumber;
            await _personalRepository.CommitAsync();

        }

        public async Task CreatePersonalUserAsync(RegisterPersonalInfoDto personalInfo)
        {
            var user = await _userManager.FindByIdAsync(personalInfo.UserId);
            if (user == null)
            {
                throw new GlobalAppException("User Not Found!");
            }

            Personal personal = _mapper.Map<Personal>(personalInfo);

            personal.Id = user.Id;
            personal.Email = user.Email;
            personal.UserName = user.UserName;
            personal.LockoutEnd = user.LockoutEnd;
            personal.LockoutEnabled = user.LockoutEnabled;
            personal.TwoFactorEnabled = user.TwoFactorEnabled;
            personal.AccessFailedCount = user.AccessFailedCount;
            personal.ConcurrencyStamp = user.ConcurrencyStamp;
            personal.IsDeleted = user.IsDeleted;
            personal.EmailConfirmed = user.EmailConfirmed;
            personal.NormalizedEmail = user.NormalizedEmail;
            personal.NormalizedUserName = user.NormalizedUserName;
            personal.PasswordHash = user.PasswordHash;
            personal.PhoneNumber = user.PhoneNumber;
            personal.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            personal.SecurityStamp = user.SecurityStamp;


            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new GlobalAppException("User Deletion Failed!");
            }

            await _personalRepository.AddAsync(personal);
            await _personalRepository.CommitAsync();
        }

        public Task DeletePersonalUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonalUserDto>> GetAllPersonalUsersAsync(Expression<Func<Personal, bool>>? func = null, Func<IQueryable<Personal>, IIncludableQueryable<Personal, object>>? include = null, Func<IQueryable<Personal>, IOrderedQueryable<Personal>>? orderBy = null, bool EnableTraking = false)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalUserDto> GetPersonalUserAsync(Expression<Func<Personal, bool>> func, Func<IQueryable<Personal>, IIncludableQueryable<Personal, object>>? include = null, bool EnableTraking = true)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalUserDto> GetPersonalUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonalUserDto> GetPersonalUserByIdAsync(string userId)
        {
            var personal = await _personalRepository.GetAsync(x=>x.Id == userId,null,true);
            if (personal == null)
            {
                throw new GlobalAppException("User not Found!");
            }
            PersonalUserDto person = _mapper.Map<PersonalUserDto>(personal);
            return person;
        }
    }
}
