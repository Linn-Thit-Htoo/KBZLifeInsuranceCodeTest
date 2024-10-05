using KBZLifeInsuranceCodeTest.Shared;
using KBZLifeInsuranceCodeTest.Shared.Services.AuthServices;
using KBZLifeInsuranceCodeTest.Shared.Services.SecurityServices;
using KBZLifeInsuranceCodeTest.Utils;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenValidationService _tokenValidationService;

        public AuthenticationMiddleware(
            RequestDelegate next,
            TokenValidationService tokenValidationService
        )
        {
            _next = next;
            _tokenValidationService = tokenValidationService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Result<string> result;
            try
            {
                string authHeader = context.Request.Headers.Authorization!;
                string requestPath = context.Request.Path;

                if (ShouldPass(requestPath))
                {
                    await _next.Invoke(context);
                    return;
                }

                if (authHeader is null || !authHeader.StartsWith("Bearer"))
                {
                    goto result;
                }

                string[] header_token = authHeader.Split(" ");
                string header = header_token[0];
                string token = header_token[1];

                var principal = _tokenValidationService.ValidateToken(token);
                if (principal is null)
                {
                    goto result;
                }

                await _next.Invoke(context);
                return;
            }
            catch (Exception ex)
            {
                goto result;
            }

            result:
            result = Result<string>.UnAuthorized();
            await context.Response.WriteAsync(result.ToJson());
        }

        private bool ShouldPass(string requestPath)
        {
            return GetPublicAccessEndpoints().Any(x => x.Equals(requestPath));
        }

        private static List<string> GetPublicAccessEndpoints()
        {
            return new List<string>()
            {
                "/api/v1/account/register",
                "/api/v1/account/login",
                "/api/v1/encrypt",
                "/api/v1/decrypt"
            };
        }
    }
}
