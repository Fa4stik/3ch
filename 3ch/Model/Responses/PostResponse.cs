namespace _3ch.Model.Responses
{
    public class PostResponse
    {
        public int id { get; set; }
        public string? heading { get; set; }
        public string? content { get; set; }
        public DateTime date { get; set; }
        public int tag { get; set; }
        public string tagName { get; set; }
        public string tagShortName { get; set; }
        public int? mediaId { get; set; }
        public string? mediaSrc { get; set; }
    }
}
