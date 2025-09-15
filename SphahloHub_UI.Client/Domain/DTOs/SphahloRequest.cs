using System.Text.Json.Serialization;

namespace SphahloHub_UI.Client.Domain.DTOs
{
    public class SphahloRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
