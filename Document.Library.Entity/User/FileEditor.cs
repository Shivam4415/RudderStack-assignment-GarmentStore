using Document.Library.Globals.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity.User
{
    public class FileEditor
    {
        public int Id { get; }

        [JsonIgnore]
        public Guid OwnerId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public DateTime DateCreated { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public DateTime DateModified { get; set; }

        [JsonIgnore]
        public Guid ModifiedBy { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = -92)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Permission Permission { get; set; }

    }
}
