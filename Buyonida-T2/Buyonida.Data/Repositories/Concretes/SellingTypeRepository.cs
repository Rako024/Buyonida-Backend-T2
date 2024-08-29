using Buyonida.Core.Entities;
using Buyonida.Data.DAL;
using Buyonida.Data.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Data.Repositories.Concretes
{
    public class SellingTypeRepository : Repository<SellingType>, ISellingTypeRepository
    {
        public SellingTypeRepository(BuyonidaContext context) : base(context)
        {
        }
    }
}
