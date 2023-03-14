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
        public async static Task<IEnumerable<Comment>> GetComments(int postId, int start, int end = 0)
        {
            return await CommentDataTransfer.GetComments(postId, start, end);
        }

        [HttpGet(Name = "GetComment")]
        public async static Task<Comment> GetComment(int id)
        {
            return await CommentDataTransfer.GetComment(id);
        }
    }
}
