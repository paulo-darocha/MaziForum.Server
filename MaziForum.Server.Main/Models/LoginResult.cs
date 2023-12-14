namespace MaziForum.Server.Main.Models
{
   public class LoginResult
   {
      public string Id { get; set; }
      public bool Succeeded { get; set; }
      public string Message { get; set; }
      public string Token { get; set; }
      public string NickName { get; set; }
      public string Email { get; set; }
      public string ImageUrl { get; set; }
   }
}
