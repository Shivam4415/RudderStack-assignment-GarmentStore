using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity.Data
{
    public class ReaderData
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int Ordinal { get; set; }

        public int EditorId { get; set; }

        public int Length { get; set; }

        public string Value { get; set; }

        public Attributor Attributes { get; set; }

    }
}
