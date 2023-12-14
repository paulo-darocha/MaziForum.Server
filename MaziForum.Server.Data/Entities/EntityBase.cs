using System;
using System.ComponentModel.DataAnnotations;

namespace MaziForum.Server.Data.Entities
{
    public class EntityBase
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [MaxLength(450)]
        public string UserId { get; set; }
    }
}
