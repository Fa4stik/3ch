using _3ch.DataTransfers;
using _3ch.Model;
using _3ch.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace _3ch.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet(Name = "GetPosts")]
        async public Task<IEnumerable<Post>> GetPosts(int start = 0, int end = 1)
        {
            return await PostDataTransfer.GetPosts(start, end);
        }

        [HttpGet(Name = "GetPost")]
        async public Task<Post> GetPost(int id)
        {
            return await PostDataTransfer.GetPost(id);
        }

        [HttpPost(Name = "CreatePost")]
        async public Task<Post> CreatePost(string heading, string content, int tagId, int? mediaId = null)
        {        
            return await PostDataTransfer.CreatePosts(heading, content, tagId, mediaId);
        }

        [HttpPut(Name = "UpdatePost")]
        async public Task<Post> UpdatePost(int postId, string heading, string content, int tagId, int? mediaId = null)
        {
            return await PostDataTransfer.UpdatePosts(postId, heading, content, tagId, mediaId);
        }
    }
}
