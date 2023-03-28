using _3ch.DAL;
using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DataTransfers
{
    public class CommentDataTransfer
    {
        public async static Task<Comment?> SendComment(int postId, string comment, int? mediaid = null)
        {
            await using var appContext = new ApplicationContext();
            var sendedComment = (await appContext.Comment.AddAsync(new Comment() { postId = postId, mediaId = mediaid, comment = comment })).Entity;
            await appContext.SaveChangesAsync();
            return sendedComment;
        }

        public async static Task<Comment?> DeleteComment(int commentId)
        {
            await using var appContext = new ApplicationContext();
            var comment = await appContext.Comment.FirstOrDefaultAsync(x => x.id == commentId);
            if (comment != null)
            {
                appContext.Comment.Remove(comment);
                await appContext.SaveChangesAsync();
            }

            return comment;
            //return Results.NotFound("Comment does not exist");
        }

        public async static Task<Comment?> UpdateComment(int commentId, string comment, int? mediaid = null)
        {
            await using var appContext = new ApplicationContext();
            var sendedComment = (await appContext.Comment.FirstOrDefaultAsync(x => x.id == commentId));
            if (sendedComment != null)
            {
                sendedComment.comment = comment;
                sendedComment.mediaId = mediaid;
            }
            await appContext.SaveChangesAsync();
            return sendedComment;
        }
    }
}
