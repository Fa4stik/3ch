using _3ch.Model;
using _3ch.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3ch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        private readonly IFileManager _fileManager;
        public MediaController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet("{filePath}")]
        public async Task<IActionResult> GetFile(string filePath)
        {
            var result = await _fileManager.GetFile(filePath);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var result = await _fileManager.GetFile(id);
            return result == null ? NotFound(result) : Ok(result);
        }

        [HttpPost("{file:file}")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            var result = await _fileManager.UploadFile(file);
            return result == null ? NotFound(result) : Ok(result);
        }

        [HttpDelete("{filePath}")]
        public async Task<IActionResult> DeleteFile([FromForm] string filePath)
        {
            try
            {
                var result = await _fileManager.DeleteFile(filePath);
                return result == null ? NotFound(result) : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFile([FromForm] int id)
        {
            try
            {
                var result = await _fileManager.DeleteFile(id);
                return result == null ? NotFound(result) : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
