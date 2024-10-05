using KBZLifeInsuranceCodeTest.DTOs.Features.PaymentMethod;
using KBZLifeInsuranceCodeTest.Shared;
using KBZLifeInsuranceCodeTest.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PaymentMethod.GetPaymentMethodList
{
    [Route("api/v1/payment-method")]
    [ApiController]
    public class GetPaymentMethodListEndpoint : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetPaymentMethodListAsync(CancellationToken cs)
        {
            Result<List<PaymentMethodDTO>> result;
            try
            {
                string jsonStr = await System.IO.File.ReadAllTextAsync("Data/Payment_Method.json", cs);
                var lst = jsonStr.ToObject<List<PaymentMethodDTO>>();
                result = Result<List<PaymentMethodDTO>>.Success(lst);
            }
            catch (Exception ex)
            {
                result = Result<List<PaymentMethodDTO>>.Fail(ex);
            }

            return Content(result);
        }
    }
}
