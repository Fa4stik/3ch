using _3ch.DAL;
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
        private readonly UnitOfWork _unitOfWork;
        public CommentController(IFileManager fileManager, UnitOfWork unitOfWork)
        {
            _fileManager = fileManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{postId:int}/{start:int}/{end:int}")]
        public async Task<IActionResult> GetComments(int postId, int start = 0, int end = 1)
        {
            var result = (await _unitOfWork.CommentRepository.GetList())
                .Where(c => c.postId == postId)
                .Take(new Range(start, end));
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var result = _unitOfWork.CommentRepository.Get(id);
            return result == null ? BadRequest(result) : Ok(result);
        }
    }
}
