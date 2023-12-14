using System.Collections.Generic;

namespace MaziForum.Server.Data.Entities
{
    public class Comment : EntityBase
    {
        public int CommentId { get; set; }

        public bool IsDisabled { get; set; }

        public int Views { get; set; } = 0;
        public int Points { get; set; } = 0;
        public string Body { get; set; }

        // Relationship

        public int QuestionId { get; set; }
        public IList<CommentPoint> CommentPoinst { get; set; }
    }
}
