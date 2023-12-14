using System.ComponentModel.DataAnnotations;

namespace MaziForum.Server.Data.Entities
{
    public class Media : EntityBase
    {
        public int MediaId { get; set; }

        [StringLength(450)]
        public string Caption { get; set; }

        public int FileSize { get; set; }

        [StringLength(450)]
        public string FileName { get; set; }

        public MediaType MediaType { get; set; }
    }
}
