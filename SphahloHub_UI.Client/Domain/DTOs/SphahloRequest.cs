namespace SphahloHub_UI.Client.Domain.DTOs
{
    public class SphahloRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
