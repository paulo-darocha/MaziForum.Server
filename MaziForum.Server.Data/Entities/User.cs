using Microsoft.AspNetCore.Identity;

namespace MaziForum.Server.Data.Entities
{
    public class User : IdentityUser
    {
        public string NickName { get; set; }

        // Relationships
        public Media UserImage { get; set; }

    }
}
