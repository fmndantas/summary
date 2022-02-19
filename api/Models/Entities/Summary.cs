using System.ComponentModel.DataAnnotations.Schema;
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

        private Container root { get; set; }

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
            return $"Title = {Title}\nAuthor = {Author}\n\n{Root.Print()}";
        }
    }
}