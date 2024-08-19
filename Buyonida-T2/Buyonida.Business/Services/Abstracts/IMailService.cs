using AutoMapper.Internal;
using Buyonida.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonida.Business.Services.Abstracts
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
