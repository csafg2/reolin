namespace Reolin.Web.Security.Jwt
{

    public interface IJwtTokenProvider
    {
        string ProvideToken(TokenProviderOptions options);
    }
}
