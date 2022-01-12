using System.ComponentModel.DataAnnotations.Schema;

namespace summary.Models
{
    public class Summary
    {
        public int id { get; set; }
        public string title { get; set; }

        public string author { get; set; }
        public string content { get; set; }
        
        [NotMapped]
        public bool Updatable => id != 0;
    }
}