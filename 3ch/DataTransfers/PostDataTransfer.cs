using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DataTransfers
{
    public class PostDataTransfer
    {
        public async static Task<IResult> GetPosts(int start, int end = 0)
        {
            await using var appContext = new ApplicationContext();
            return Results.Ok((await appContext.Post.ToListAsync()).Take(new Range(start, end)));
        }

        public async static Task<IResult> GetPost(int id)
        {
            await using var AppContext = new ApplicationContext();
            return Results.Ok(await AppContext.Post.FirstOrDefaultAsync(x => x.id == id));
        }

        public async static Task<IResult> CreatePosts(string heading, string content, int idTag, int? idMedia = null)
        {
            await using var appContext = new ApplicationContext();
            var post = (await appContext.Post.AddAsync(new Post() { heading = heading, content = content, tag = idTag, date = DateTime.UtcNow, mediaId = idMedia })).Entity;
            await appContext.SaveChangesAsync();
            return Results.Ok(post);
        }

        public async static Task<IResult> UpdatePosts(int idPost, string heading, string content, int idTag, int? idMedia = null)
        {
            await using var appContext = new ApplicationContext();
            var post = await appContext.Post.FirstOrDefaultAsync(x => x.id == idPost);
            if (post != null)
            {
                post.heading = heading;
                post.content = content;
                post.tag = idTag;
                post.mediaId = idMedia;
            }
            await appContext.SaveChangesAsync();
            return Results.Ok(post);
        }
    }
}
