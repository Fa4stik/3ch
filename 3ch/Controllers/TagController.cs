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

        [HttpGet]
        public async Task<IEnumerable<Tag>> GetTag(int startIndex = 0, int endIndex = 1)
        {
            //int endId = endIndex ?? _countTag;
            //из-за проблема с count в этом классе не запускался сваггер. я закоментил что-бы потом было видно что не работало.
            return await _unitOfWork.TagRepository.GetList(startIndex, endIndex);
        }
    }
}
