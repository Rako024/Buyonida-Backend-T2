using Buyonida.Business.DTOs.DataDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Services.Abstracts
{
    public interface ISellingPointService
    {
        Task CreateSellingPoint(SellingPointDto sellingPointDto);
    }
}
