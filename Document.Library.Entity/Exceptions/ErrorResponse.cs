using Document.Library.Globals.Enum;
using Newtonsoft.Json;

namespace Document.Library.Entity.Exceptions
{
    public class ErrorResponse
    {
        [JsonProperty("Error")]
        public ErrorObject Error = new ErrorObject();

        public ErrorResponse()
        {
        }
        public ErrorResponse(ErrorResponseCode type, string message, string param = null, string param1 = null, ErrorCode? code = null)
        {
            Error.Type = type;
            Error.Message = message;
            Error.Param = param;
            Error.Param1 = param1;
            Error.Code = code;
        }

    }

}