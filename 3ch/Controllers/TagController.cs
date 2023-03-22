using _3ch.DataTransfers;
using _3ch.Model;
using _3ch.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace _3ch.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : Controller
    {
        [HttpGet(Name = "GetTagById")]
        public async Task<Tag> GetTagById(int idTag) 
            => await TagDataTransfer.GetTagById(idTag);

        [HttpGet(Name = "GetTagBetween")]
        public async Task<IEnumerable<Tag>> GetTagBetween(int startIndex = 0, int endIndex = 3)
        {
            var list = (await TagDataTransfer.GetTagBetween(startIndex, endIndex)).ToList();
            return list;
        }

        [HttpGet(Name = "GetTagCount")]
        public async Task<int> GetTagCount() 
            => await TagDataTransfer.GetTagCount();
    }
}
