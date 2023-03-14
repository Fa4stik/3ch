using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DataTransfers
{
    public class CommentDataTransfer
    {
        public async static Task<IEnumerable<Comment>> GetComments(int postId, int start, int end = 0)
        {
            using (var AppContext = new ApplicationContext())
                return (await AppContext.Comment.ToListAsync()).Where(x => x.postId == postId).Take(new Range(start, end));
        }

        public async static Task<Comment> GetComment(int id)
        {
            using (var AppContext = new ApplicationContext())
                return (await AppContext.Comment.FirstOrDefaultAsync(x => x.id == id));
        }

        public async static Task<Comment> SendComment(int postId, string comment, int? mediaid = null)
        {
            using (var AppContext = new ApplicationContext())
            {
                var sendedComment = (await AppContext.Comment.AddAsync(new Comment() { postId = postId, mediaId = mediaid, comment = comment })).Entity;
                await AppContext.SaveChangesAsync();
                return sendedComment;
            }
        }

        public async static Task<Comment> DeleteComment(int commentId)
        {
            using (var AppContext = new ApplicationContext())
            {
                var comment = await AppContext.Comment.FirstOrDefaultAsync(x => x.id == commentId);
                if (comment != null)
                {
                    AppContext.Comment.Remove(comment);
                    await AppContext.SaveChangesAsync();
                    return comment;
                }
                else
                    return null;
            }                
        }

        public async static Task<Comment> UpdateComment(int commentId, string comment, int? mediaid = null)
        {
            using (var AppContext = new ApplicationContext())
            {
                var sendedComment = (await AppContext.Comment.FirstOrDefaultAsync(x => x.id == commentId));
                if (sendedComment != null)
                {
                    sendedComment.comment = comment;
                    sendedComment.mediaId = mediaid;
                }
                await AppContext.SaveChangesAsync();
                return sendedComment;
            }
        }
    }
}
