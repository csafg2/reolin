
namespace Reolin.Web.Security.Jwt
{

    public interface IJwtTokenProvider
    {
        string ProvideJwt(TokenProviderOptions options);
    }
}
