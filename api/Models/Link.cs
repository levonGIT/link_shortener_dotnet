using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class Link
    {
        public int Id { get; set; }
        public string OriginLink { get; set; } = string.Empty;
        public string ShortLink { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int VisitCount { get; set; }

        public int? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}
