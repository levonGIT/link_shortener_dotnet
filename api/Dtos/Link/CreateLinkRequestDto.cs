namespace api.Dtos.Link
{
    public class CreateLinkRequestDto
    {
        public string OriginLink { get; set; } = string.Empty;
        public DateTime CreatedOn { get; } = DateTime.Now;
        public int VisitCount { get; } = 0;
    }
}
