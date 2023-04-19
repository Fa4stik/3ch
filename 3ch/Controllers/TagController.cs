using _3ch.DataTransfers;
using _3ch.Model;
using _3ch.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using _3ch.DAL;

namespace _3ch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        //private readonly int _countTag;
        private readonly UnitOfWork _unitOfWork;
        public TagController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_countTag = _unitOfWork.TagRepository.Count();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetTagCount()
        //    => Ok(_countTag);

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTag(int id) 
            => Ok(_unitOfWork.TagRepository.Get(id));

        [HttpGet("{shortName}")]
        public async Task<IActionResult> GetTag(string shortName)
        {
            shortName = shortName.Replace("%2F","/").ToLower();
            var context = new ApplicationContext();
            var tag = context.Tag.FirstOrDefault(x => x.shortName == shortName);
            if (tag == null)
            {
                return this.NotFound();
            }
            return Ok(tag);
        }

        [HttpGet]
        public async Task<IEnumerable<Tag>> GetTag(int startIndex = 0, int endIndex = 1)
        {
            //int endId = endIndex ?? _countTag;
            //из-за проблема с count в этом классе не запускался сваггер. я закоментил что-бы потом было видно что не работало.
            return await _unitOfWork.TagRepository.GetList(startIndex, endIndex);
        }
    }
}
