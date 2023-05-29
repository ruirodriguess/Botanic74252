using Microsoft.AspNetCore.Identity;

namespace RuiRumos74252.Models
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
