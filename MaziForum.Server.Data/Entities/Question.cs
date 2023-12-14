using System.Collections.Generic;

namespace MaziForum.Server.Data.Entities
{
   public class Question : EntityBase
   {
      public int QuestionId { get; set; }

      public bool IsDisabled { get; set; } = false;

      public int Views { get; set; } = 0;
      public int Points { get; set; } = 0;
      public string Title { get; set; }
      public string Body { get; set; }

      // Relationships

      public User User { get; set; }
      public Tag Tag { get; set; }
      public int TagId { get; set; }
      public IList<Comment> Comments { get; set; }
      public IList<QuestionPoint> QuestionPoints { get; set; }
   }
}
