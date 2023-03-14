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
    }
}
