using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Index(nameof(Login), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Link> Links { get; } = new List<Link>();
    }
}
