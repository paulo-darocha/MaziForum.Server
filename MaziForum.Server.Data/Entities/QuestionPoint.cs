namespace MaziForum.Server.Data.Entities
{
    public class QuestionPoint : EntityBase
    {
        public int QuestionPointId { get; set; }

        public bool IsDecrement { get; set; } = false;

        // Relationship

        public int QuestionId { get; set; }
    }
}
