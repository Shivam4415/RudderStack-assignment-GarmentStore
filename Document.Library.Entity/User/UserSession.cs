using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity.User
{
    public class UserSession
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool IsPersistent { get; set; }

        public DateTime LastAccess { get; set; }

        public string AccessToken { get; set; }

        public DateTime? LastCheckAccessToken { get; set; }

        public DateTime? AccessTokenExpiry { get; set; }
    }
}
