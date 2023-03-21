using _3ch.DataTransfers;
using _3ch.Model;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.Design;
using System.Text.Json;

namespace _3ch.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendComment(int postId, string comment, int? mediaid = null)
        {
            comment = comment.Replace(@"\n", "\n");
            var sendedComment = await CommentDataTransfer.SendComment(postId, comment, mediaid);
            if (sendedComment == null)
                await Clients.Group(postId.ToString()).SendAsync("RecieveComment", sendedComment);
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
