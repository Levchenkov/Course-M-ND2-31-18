using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Domain.Contracts.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
