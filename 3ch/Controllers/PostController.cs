using _3ch.DAL;
using _3ch.Model;
using _3ch.Model.Responses;
using _3ch.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3ch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        public PostController(IFileManager fileManager, UnitOfWork unitOfWork)
        {
            _fileManager = fileManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string shortTagName, [FromQuery]int start = 0, [FromQuery]int end = 1)
        {
            return Ok(await _unitOfWork.PostRepository.GetPostsByTag(shortTagName, start, end));
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var x = _unitOfWork.PostRepository.Get(id);
            return Ok(new PostResponse()
            {
                id = x.id,
                content = x.content,
                date = x.date,
                heading = x.heading,
                mediaId = x.mediaId,
                tag = x.tag,
                mediaSrc = x.mediaId.HasValue ? _unitOfWork.MediaRepository.Get(x.mediaId.Value).src : null,
                tagName = _unitOfWork.TagRepository.Get(x.tag).name,
                tagShortName = _unitOfWork.TagRepository.Get(x.tag).shortName
            });
        }

        [HttpPost("{CreatePost}")]
        public async Task<IActionResult> CreatePost([FromForm] string? heading,
            [FromForm] string content,
            [FromForm] int tagId,
            [FromForm] int? mediaId = null)
        {
            content = content.Replace(@"\n", "\n");
            Post post = new Post()
            {
                heading = heading,
                content = content,
                tag = tagId,
                mediaId = mediaId,
                date = DateTime.UtcNow,
            };
            _unitOfWork.PostRepository.Create(post);
            _unitOfWork.Save();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] string? heading,
            [FromForm] string content,
            [FromForm] int tagId,
            [FromForm] IFormFile? file = null)
        {
            Post post = null;
            Media mediaResult = null;
            content = content.Replace(@"\n", "\n");
            if (file != null)
                mediaResult = await _fileManager.UploadFile(file);
            if (file != null)
                post = new Post()
                {
                    heading = heading,
                    content = content,
                    tag = tagId,
                    mediaId = mediaResult.id,
                    date = DateTime.UtcNow,
                };
            else
                post = new Post()
                {
                    heading = heading,
                    content = content,
                    tag = tagId,
                    mediaId = null,
                    date = DateTime.UtcNow,
                };         
            _unitOfWork.PostRepository.Create(post);
            _unitOfWork.Save();
            return Ok(post);
        }

        [HttpPut]
        public IActionResult UpdatePost([FromForm] int postId, [FromForm] string heading, [FromForm] string content, [FromForm] int tagId, [FromForm] int? mediaId = null)
        {
            content = content.Replace(@"\n", "\n");
            Post post = new Post()
            {
                id = postId,
                heading = heading,
                content = content,
                tag = tagId,
                mediaId = mediaId,
                date = DateTime.UtcNow,
            };
            _unitOfWork.PostRepository.Update(post);
            _unitOfWork.Save();
            return Ok(post);
        }
    }
}
