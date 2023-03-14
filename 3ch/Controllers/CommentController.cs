using _3ch.DataTransfers;
using _3ch.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3ch.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        [HttpGet(Name = "GetComments")]
        public async Task<IEnumerable<Comment>> GetComments(int postId, int start = 0, int end = 1)
        {
            return await CommentDataTransfer.GetComments(postId, start, end);
        }

        [HttpGet(Name = "GetComment")]
        public async Task<Comment> GetComment(int id)
        {
            return await CommentDataTransfer.GetComment(id);
        }
    }
}
