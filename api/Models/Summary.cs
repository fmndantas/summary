using System.ComponentModel.DataAnnotations.Schema;

namespace summary.Models
{
    public class Summary
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
        public string Content { get; set; }
        
        [NotMapped]
        public bool Updatable => Id != 0;
    }
}