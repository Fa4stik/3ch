using _3ch.DataTransfers;
using _3ch.Model;
using _3ch.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3ch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IFileManager _fileManager;
        public CommentController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet]
        public async Task<IResult> GetComments(int postId, int start = 0, int end = 1)
        {
            return await CommentDataTransfer.GetComments(postId, start, end);
        }

        [HttpGet("{id:int}")]
        public async Task<IResult> GetComment(int id)
        {
            return await CommentDataTransfer.GetComment(id);
        }
    }
}
