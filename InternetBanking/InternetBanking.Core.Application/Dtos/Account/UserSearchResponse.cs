
namespace InternetBanking.Core.Application.Dtos.Account
{
    public class UserSearchResponse
    {
        public string? IdUser { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public bool? HasError { get; set; }
    }
}
