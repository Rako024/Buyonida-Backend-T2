using Buyonida.Business.DTOs.DataDtos;
using Buyonida.Business.Services.Abstracts;
using Buyonida.Core.Entities;
using Buyonida.Data.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Services.Concretes
{
    public class SellingTypeService : ISellingTypeService
    {
        private readonly ISellingTypeRepository _sellingTypeRepository;

        public SellingTypeService(ISellingTypeRepository sellingTypeRepository)
        {
            _sellingTypeRepository = sellingTypeRepository;
        }

        public async Task CreateSellingType(SellingTypeDto sellingType)
        {
            SellingType sell = new SellingType()
            {
                YourIdea = sellingType.YourIdea,
                IdeaType = sellingType.IdeaType
            };
            await _sellingTypeRepository.AddAsync(sell);
            await _sellingTypeRepository.CommitAsync();
        }
    }
}
