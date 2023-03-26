using _3ch.DAL;
using _3ch.DataTransfers;
using _3ch.Model;
using _3ch.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace _3ch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private UnitOfWork unitOfWork;
        public PostController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int start = 0, int end = 1)
        {
            return Ok(await unitOfWork.PostRepository.GetList(start, end));
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(unitOfWork.PostRepository.Get(id));
        }

        [HttpPost]
        public IActionResult CreatePost([FromForm]string? heading, [FromForm]string content, [FromForm] int tagId, [FromForm] int? mediaId = null)
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
            unitOfWork.PostRepository.Create(post);
            unitOfWork.Save();
            return Ok("post created");
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
            unitOfWork.PostRepository.Update(post);
            unitOfWork.Save();
            return Ok("post updated");
        }
    }
}
