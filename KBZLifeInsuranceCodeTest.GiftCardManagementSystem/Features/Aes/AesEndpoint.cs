namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Aes;

[Route("api/v1")]
[ApiController]
public class AesEndpoint : BaseController
{
    private readonly AesService _aesService;

    public AesEndpoint(AesService aesService)
    {
        _aesService = aesService;
    }

    [HttpPost("encrypt")]
    public IActionResult Encrypt(string plainText)
    {
        Result<string> result;
        try
        {
            var encryptedText = _aesService.Encrypt(plainText);
            result = Result<string>.Success(data: encryptedText);
        }
        catch (Exception ex)
        {
            result = Result<string>.Fail(ex);
        }

        return Content(result);
    }

    [HttpPost("decrypt")]
    public IActionResult Decrypt(string encryptedText)
    {
        Result<string> result;
        try
        {
            var decryptedText = _aesService.Decrypt(encryptedText);
            result = Result<string>.Success(data: decryptedText);
        }
        catch (Exception ex)
        {
            result = Result<string>.Fail(ex);
        }

        return Content(result);
    }
}
