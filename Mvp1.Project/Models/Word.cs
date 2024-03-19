using System.Text.Json;

namespace Mvp1.Project.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
        public Category Category { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}