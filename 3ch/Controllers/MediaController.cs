using _3ch.Model;
using _3ch.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3ch.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MediaController : Controller
    {
        private readonly IFileManager _fileManager;
        public MediaController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet(Name = "GetFileByPath")]
        public async Task<IResult> GetFileByPath(string filePath) 
            => await _fileManager.GetFile(filePath);

        [HttpGet(Name = "GetFileById")]
        public async Task<IResult> GetFileById(int fileId)
            => await _fileManager.GetFile(fileId);

        [HttpPost(Name = "UploadFile")]
        public async Task<IResult> UploadFile(IFormFile file) 
            => await _fileManager.UploadFile(file);
        

        [HttpDelete(Name = "DeleteFile")]
        public async Task<IResult> DeleteFile([FromForm] string filePath) 
            => await _fileManager.DeleteFile(filePath);

    }
}
