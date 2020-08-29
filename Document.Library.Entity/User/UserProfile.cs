using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity.User
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool HasVerifiedEmail { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsOnline { get; set; }

        public DateTime? LastActive { get; set; }

        [JsonProperty]
        public FileEditor Editor { get; set; }

    }
}
