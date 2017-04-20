using Bookmaker.Infrastructure.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Bookmaker.Infrastructure.Services
{
    public class LocalMailService : IMailService
    {        
        public async Task Send(string mailFrom, string mailTo, string subject, string message)
        {
            Debug.WriteLine($"Mail from {mailFrom} to {mailTo}, with LocalMailService.");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");

            await Task.CompletedTask;
        }
    }
}
