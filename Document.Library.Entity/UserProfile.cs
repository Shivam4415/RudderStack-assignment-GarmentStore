using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Entity
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserProfile
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string Phone { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string ConfirmPassword { get; set; }
        [JsonIgnore]
        public bool HasVerifiedEmail { get; set; }

        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public bool IsOnline { get; set; }
        
        public DateTime? LastActive { get; set; }
    }
}
