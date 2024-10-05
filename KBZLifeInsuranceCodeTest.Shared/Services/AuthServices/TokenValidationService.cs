namespace KBZLifeInsuranceCodeTest.Shared.Services.AuthServices;

public class TokenValidationService
{
    private readonly IConfiguration _configuration;

    public TokenValidationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!);

            TokenValidationParameters parameters = new()
            {
                RequireExpirationTime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidIssuer = _configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);

            return principal;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
