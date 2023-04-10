using _3ch.DataTransfers;
using _3ch.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Text.Json;

namespace _3ch.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendComment(int postId, string comment, int? mediaId = null)
        {
            comment = comment.Replace(@"\n", "\n");
            var db = new ApplicationContext();
            var sendedComment = await CommentDataTransfer.SendComment(postId, comment, mediaId);
            if (sendedComment != null)
            {
                var commentInfo = new CommentInfo()
                {
                    comment = sendedComment.comment,
                    id = sendedComment.id,
                    postId = sendedComment.postId,
                    Img = (await db.Media.FirstOrDefaultAsync(x => x.id == sendedComment.mediaId)).src
                };
                await Clients.Group(postId.ToString()).SendAsync("RecieveComment", commentInfo);
            }                
        }

        public async Task DeleteComment(int postId, int commentId)
        {
            var deletedComment = await CommentDataTransfer.DeleteComment(commentId);
            if (deletedComment != null)
                await Clients.Group(postId.ToString()).SendAsync("DeleteComment", deletedComment);
        }

        public async Task UpdateComment(int postId, int commentId, string comment, int? mediaid = null)
        {
            comment = comment.Replace(@"\n", "\n");
            var updatedComment = await CommentDataTransfer.UpdateComment(commentId, comment, mediaid);
            if (updatedComment != null)
                await Clients.Group(postId.ToString()).SendAsync("UpdateComment", updatedComment);
        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
