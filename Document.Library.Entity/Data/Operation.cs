using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity.Data
{
    public class Operation
    {
        public string Insert { get; set; }

        public int? Retain { get; set; }

        public int? Delete { get; set; }

        public Attributor Attributes { get; set; }


    }
}
