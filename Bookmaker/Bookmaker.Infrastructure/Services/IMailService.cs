using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Services
{
    public interface IMailService
    {
        Task Send(string mailFrom, string mailTo, string subject, string message);
    }
}
