

using InternetBanking.Core.Application.Dtos.Email;

namespace InternetBanking.Core.Application.Interfaces.Service
{
    public interface IEmailServices
    {
        Task sendAsync(EmailRequest request);
    }
}
