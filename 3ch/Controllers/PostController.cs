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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet(Name = "GetPosts")]
        async public Task<IResult> GetPosts(int start = 0, int end = 1)
        {
            return await PostDataTransfer.GetPosts(start, end);
        }

        [HttpGet(Name = "GetPost")]
        async public Task<IResult> GetPost(int id)
        {
            return await PostDataTransfer.GetPost(id);
        }

        [HttpPost(Name = "CreatePost")]
        //
        async public Task<IResult> CreatePost([FromForm]string? heading, [FromForm]string content, [FromForm] int idTag, [FromForm] int? mediaId = null)
        {
            content = content.Replace(@"\n", "\n");
            return await PostDataTransfer.CreatePosts(heading, content.ToString(), idTag, mediaId);
        }

        [HttpPut(Name = "UpdatePost")]
        async public Task<IResult> UpdatePost([FromForm] int idPost, [FromForm] string heading, [FromForm] string content, [FromForm] int idTag, [FromForm] int? idMedia = null)
        {
            content = content.Replace(@"\n", "\n");
            return await PostDataTransfer.UpdatePosts(idPost, heading, content, idTag, idMedia);
        }
    }
}
