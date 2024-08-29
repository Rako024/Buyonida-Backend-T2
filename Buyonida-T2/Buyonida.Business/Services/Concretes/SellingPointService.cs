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
    public class SellingPointService : ISellingPointService
    {
        private readonly ISellingPointRepository _sellingPointRepository;

        public SellingPointService(ISellingPointRepository sellingPointRepository)
        {
            _sellingPointRepository = sellingPointRepository;
        }

        public async Task CreateSellingPoint(SellingPointDto sellingPointDto)
        {
            SellingPoint sell = new SellingPoint()
            {
                SellingPointName = sellingPointDto.SellingPointName
            };
            await _sellingPointRepository.AddAsync(sell);
            await _sellingPointRepository.CommitAsync();
        }
    }
}
