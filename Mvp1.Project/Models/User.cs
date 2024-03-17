using System.Text.Json;

namespace Mvp1.Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ERole Role { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public enum ERole { Admin, User }
}