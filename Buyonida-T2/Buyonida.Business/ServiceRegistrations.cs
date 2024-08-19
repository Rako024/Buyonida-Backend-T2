using Buyonida.Business.Services.Abstracts;
using Buyonida.Business.Services.Concretes;
using Buyonida.Data.Repositories.Abstracts;
using Buyonida.Data.Repositories.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business
{
    public static class ServiceRegistrations
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonalRepository, PersonalRepository>();
            services.AddScoped<IInvidualRepository, InvidualRepository>();
            services.AddScoped<IJuridicalRepository, JuridicalRepository>();
            services.AddScoped<IMailService,MailService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<ITokenService,TokenService>();
        }
    }
}
