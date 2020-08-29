using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Globals
{
    public static class Constants
    {
        public static TimeSpan CacheSlidingExpirationTimeSpan { get; set; } = new TimeSpan(0, 60, 0); //60 Minutes

    }
}
