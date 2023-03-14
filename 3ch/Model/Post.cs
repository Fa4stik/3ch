namespace _3ch.Model
{
    public class Post
    {
        public int id { get; set; }
        public string heading { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public int tag { get; set; }
        public int mediaId { get; set; }
    }
}
