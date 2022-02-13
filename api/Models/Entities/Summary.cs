using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.Entities
{
    public class Summary
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
        public string Root { get; set; }
        public int? Year { get; set; }

        [NotMapped] public bool Updatable => Id != 0;
    }
}