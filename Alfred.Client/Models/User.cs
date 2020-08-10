using System.Collections.Generic;

namespace Alfred.Client.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}