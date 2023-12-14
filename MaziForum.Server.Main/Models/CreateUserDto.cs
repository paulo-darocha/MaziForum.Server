using System.ComponentModel.DataAnnotations;

namespace MaziForum.Server.Main.Models
{
   public class CreateUserDto
   {
      public string NickName { get; set; }

      [Required]
      public string Email { get; set; }

      [Required]
      public string Password { get; set; }
   }
}
