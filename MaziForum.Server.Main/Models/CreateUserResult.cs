using System.Collections.Generic;

namespace MaziForum.Server.Main.Models
{
   public class CreateUserResult
   {
      public bool Succeeded { get; set; }

      public IList<string> Message { get; set; }
   }
}
