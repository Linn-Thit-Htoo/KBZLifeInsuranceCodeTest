using KBZLifeInsuranceCodeTest.Utils.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.Utils
{
    public class Result<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public EnumStatusCode StatusCode { get; set; }

        public static Result<T> Success(string message = "Success.", EnumStatusCode statusCode = EnumStatusCode.OK) =>
            new Result<T> { Message = message, StatusCode = statusCode, IsSuccess = true };

        public static Result<T> Success(T data, string message = "Success.", EnumStatusCode statusCode = EnumStatusCode.OK) =>
            new Result<T> { Message = message, Data = data, StatusCode = statusCode, IsSuccess = true };

        public static Result<T> Fail(string message = "Fail.", EnumStatusCode statusCode = EnumStatusCode.BadRequest) =>
            new Result<T> { Message = message, StatusCode = statusCode, IsSuccess = false };

        public static Result<T> Fail(Exception ex) =>
            new Result<T> { Message = ex.ToString(), IsSuccess = false, StatusCode = EnumStatusCode.InternalServerError};

        public static Result<T> NotFound(string message = "No data found.") =>
            Result<T>.Fail(message, EnumStatusCode.NotFound);

        public static Result<T> Duplicate(string message = "Duplicate data.") =>
            Result<T>.Fail(message, EnumStatusCode.Conflict);

        public static Result<T> UnAuthorized(string message = "UnAuthorized.") =>
            Result<T>.Fail(message, EnumStatusCode.Unauthorized);
    }
}
