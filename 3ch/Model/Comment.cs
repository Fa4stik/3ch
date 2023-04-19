namespace _3ch.Model
{
    public class Comment
    {
        public int id { get; set; }
        public int postId { get; set; }
        public string? comment { get; set; }
        public DateTime? commentDate { get; set; }
        public int? mediaId { get; set; }
        public int? parentComment { get; set; }
    }
}
