using Document.Library.Globals.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity
{
    public class Product
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public List<Size> Size { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public int Quantity { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public Price Price { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public List<Color> Color { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 7)]
        public string Source { get; set; }
    }
}
