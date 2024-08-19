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
    public class JuridicalRepository : Repository<Juridical>, IJuridicalRepository
    {
        public JuridicalRepository(BuyonidaContext context) : base(context)
        {
        }
    }
}
