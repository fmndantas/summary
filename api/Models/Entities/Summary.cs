using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using api.Models.Dto;
using Newtonsoft.Json;

namespace api.Models.Entities
{
    public class Summary
    {
        public Summary()
        {
            root = new EmptyContainer();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string SerializedRoot { get; set; }

        public int? Year { get; set; }

        [NotMapped] private Container root { get; set; }

        [NotMapped] public bool IsRootEmpty => root.isEmpty();

        [NotMapped]
        [JsonIgnore]
        public Container Root
        {
            get
            {
                if (root.isEmpty())
                {
                    root = JsonConvert.DeserializeObject<Container>(SerializedRoot);
                }

                return root;
            }
        }

        [NotMapped] public bool Updatable => Id != 0;

        public string Print(int tab = 0)
        {
            return new StringBuilder()
                .Append($"Title = {Title}\n")
                .Append($"Author = {Author}\n")
                .Append($"{Root.Print()}")
                .ToString();
        }
    }
}