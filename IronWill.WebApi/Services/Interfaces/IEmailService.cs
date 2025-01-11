using System;
namespace IronWill.WebApi.Services.Interfaces
{
	public interface IEmailService
	{
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}

