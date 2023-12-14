namespace MaziForum.Server.Data.Entities
{
    public class CommentPoint : EntityBase
    {
        public int CommentPointId { get; set; }

        public bool IsDecrement { get; set; }

        // Relationship

        public int CommentId { get; set; }
    }
}
