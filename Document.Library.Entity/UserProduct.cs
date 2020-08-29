using Document.Library.Globals.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity
{
    public class UserProduct
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Product Product { get; set; }
        public bool IsSaved { get; set; }
        

    }
}
