namespace _3ch.Model.Responses
{
    public class CommentInfo
    {
        public int id { get; set; }
        public int postId { get; set; }
        public string? comment { get; set; }
        public DateTime? commentDate { get; set; }
        public string? Img { get; set; }
        public int? parentComment { get; set; }
    }
}
