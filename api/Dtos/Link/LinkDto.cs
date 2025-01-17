namespace api.Dtos.Link
{
    public class LinkDto
    {
        public int Id { get; set; }
        public string OriginLink { get; set; } = string.Empty;
        public string ShortLink { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int VisitCount { get; set; }
    }
}
