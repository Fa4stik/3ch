using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DataTransfers
{
    public class PostDataTransfer
    {
        public async static Task<IEnumerable<Post>> GetPosts(int start, int end = 0)
        {
            using (var AppContext = new ApplicationContext())
                return (await AppContext.Post.ToListAsync()).Take(new Range(start, end));
        }

        public async static Task<Post> GetPost(int id)
        {
            using (var AppContext = new ApplicationContext())
                return await AppContext.Post.FirstOrDefaultAsync(x => x.id == id);
        }

        public async static Task<Post> CreatePosts(string heading, string content, int tagId, int? mediaId = null)
        {
            using (var AppContext = new ApplicationContext())
            {
                var post = (await AppContext.Post.AddAsync(new Post() { heading = heading, content = content, tag = tagId, date = DateTime.Now, mediaId = mediaId })).Entity;
                await AppContext.SaveChangesAsync();
                return post;
            }              
        }

        public async static Task<Post> UpdatePosts(int postId, string heading, string content, int tagId, int? mediaId = null)
        {
            using (var AppContext = new ApplicationContext())
            {
                var post = await AppContext.Post.FirstOrDefaultAsync(x => x.id == postId);
                if (post != null)
                {
                    post.heading = heading;
                    post.content = content;
                    post.tag = tagId;
                    post.mediaId = mediaId;
                }
                await AppContext.SaveChangesAsync();
                return post;
            }
        }
    }
}
