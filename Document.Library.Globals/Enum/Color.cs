using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Globals.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Color
    {
        Red = 1,
        Blue = 2,
        Green = 3
    }
}
