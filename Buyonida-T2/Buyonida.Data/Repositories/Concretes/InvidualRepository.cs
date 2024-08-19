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
    public class InvidualRepository : Repository<Invidual>, IInvidualRepository
    {
        public InvidualRepository(BuyonidaContext context) : base(context)
        {
        }
    }
}
