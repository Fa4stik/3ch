using _3ch.DataTransfers;
using _3ch.Model;
using _3ch.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3ch.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : Controller
    {
        [HttpGet(Name = "GetTag")]
        public static async Task<Tag> GetTagById(int idTag) 
            => await TagDataTransfer.GetTagById(idTag);

        [HttpGet(Name = "GetTag")]
        public static async Task<IEnumerable<Tag>> GetTagBetween(int startIndex, int endIndex = 0) 
            => await TagDataTransfer.GetTagBetween(startIndex, endIndex);

        [HttpGet(Name = "GetTag")]
        public static async Task<int> GetTagCount() 
            => await TagDataTransfer.GetTagCount();
    }
}
