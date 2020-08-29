using Document.Library.Globals.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Document.Library.Entity.Exceptions
{
    public class ErrorObject
    {
        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ErrorResponseCode Type { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Param", NullValueHandling = NullValueHandling.Ignore)]
        public string Param { get; set; }

        [JsonProperty("Param1", NullValueHandling = NullValueHandling.Ignore)]
        public string Param1 { get; set; }

        [JsonProperty("Code", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorCode? Code { get; set; }

    }

}