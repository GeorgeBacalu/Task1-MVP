using System.Collections.Generic;
using System.Text.Json;

namespace Mvp1.Project.Models
{
    public class Game
    {
        public int Id { get; set; }
        public User User { get; set; }
        public IList<Word> Words { get; set; }
        public int NrGuessedWords { get; set; } = 0;
        public string Hint { get; set; }
        public GameState GameState { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public enum GameState { InProgress, Over }
}