using IdentityModel.Client;

namespace LuftBorn.Service.Abstraction
{
    public interface ITokenService
    {

        public Task<TokenResponse> GetToken(string scope);
    }
}
