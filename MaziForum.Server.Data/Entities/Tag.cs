using System.Collections.Generic;

namespace MaziForum.Server.Data.Entities
{
    public class Tag : EntityBase
    {
        public int TagId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        // Relationships

        public IList<Question> Questions { get; set; }
    }
}
