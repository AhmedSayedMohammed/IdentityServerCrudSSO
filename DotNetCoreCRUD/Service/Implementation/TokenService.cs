using IdentityModel.Client;
using LuftBorn.Service.Abstraction;
using Microsoft.Extensions.Options;

namespace LuftBorn.Service.Implementation
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> identityServerSettings;
        public readonly DiscoveryDocumentResponse discoveryDocumentResponse;
        public readonly HttpClient httpClient;
        public TokenService(IOptions<IdentityServerSettings> identityServerSettings)
        {
            this.identityServerSettings = identityServerSettings;
            httpClient = new HttpClient();
            discoveryDocumentResponse = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.DiscovertURL).Result;
            if (discoveryDocumentResponse.IsError)
            {
                throw discoveryDocumentResponse.Exception;
            }
        }
        public async Task<TokenResponse> GetToken(string scope)
        {
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocumentResponse.TokenEndpoint,
                ClientId = identityServerSettings.Value.ClientName,
                ClientSecret = identityServerSettings.Value.ClientPassword,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                throw new Exception("unable to get token", tokenResponse.Exception);
            }
            return tokenResponse;
        }
    }
}
