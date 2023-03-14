using _3ch.DataTransfers;
using _3ch.Model;
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
        async public Task<IEnumerable<Post>> GetPlayersAsync(int start = 0, int end = 1)
        {
            return await PostDataTransfer.GetPosts(start, end);
        }
    }
}
