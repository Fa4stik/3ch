using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DataTransfers
{
    public class CommentDataTransfer
    {
        public async static Task<IResult> GetComments(int postId, int start, int end = 0)
        {
            await using var AppContext = new ApplicationContext();
            return Results.Ok((await AppContext.Comment.ToListAsync()).Where(x => x.postId == postId).Take(new Range(start, end)));
        }

        public async static Task<IResult> GetComment(int id)
        {
            await using var appContext = new ApplicationContext();
            return Results.Ok(await appContext.Comment.FirstOrDefaultAsync(x => x.id == id));
        }

        public async static Task<IResult> SendComment(int postId, string comment, int? mediaid = null)
        {
            await using var appContext = new ApplicationContext();
            var sendedComment = (await appContext.Comment.AddAsync(new Comment() { postId = postId, mediaId = mediaid, comment = comment })).Entity;
            await appContext.SaveChangesAsync();
            return Results.Ok(sendedComment);
        }

        public async static Task<IResult> DeleteComment(int commentId)
        {
            await using var appContext = new ApplicationContext();
            var comment = await appContext.Comment.FirstOrDefaultAsync(x => x.id == commentId);
            if (comment != null)
            {
                appContext.Comment.Remove(comment);
                await appContext.SaveChangesAsync();
                return Results.Ok(comment);
            }
            
            return Results.NotFound("Comment does not exist");
        }

        public async static Task<IResult> UpdateComment(int commentId, string comment, int? mediaid = null)
        {
            await using var appContext = new ApplicationContext();
            var sendedComment = (await appContext.Comment.FirstOrDefaultAsync(x => x.id == commentId));
            if (sendedComment != null)
            {
                sendedComment.comment = comment;
                sendedComment.mediaId = mediaid;
            }
            await appContext.SaveChangesAsync();
            return Results.Ok(sendedComment);
        }
    }
}
