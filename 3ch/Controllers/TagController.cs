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
        private readonly int _countTag;
        public TagController()
        {
            using var appContext = new ApplicationContext();
            _countTag = appContext.Tag.Count();
        }

        [HttpGet(Name = "GetTagById")]
        public async Task<IResult> GetTagById(int idTag) 
            => await TagDataTransfer.GetTagById(idTag);

        [HttpGet(Name = "GetTagBetween")]
        public async Task<IResult> GetTagBetween(int startIndex = 0, int? endIndex = null)
        {
            int endId = endIndex ?? _countTag;
            return (await TagDataTransfer.GetTagBetween(startIndex, endId));
        }

        [HttpGet(Name = "GetTagCount")]
        public async Task<IResult> GetTagCount() 
            => await TagDataTransfer.GetTagCount();
    }
}
