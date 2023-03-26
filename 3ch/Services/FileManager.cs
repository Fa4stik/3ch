using _3ch.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _3ch.Services
{
    public interface IFileManager
    {
        public Task<IResult> UploadFile(IFormFile? uploadedFile);
        public Task<IResult> DeleteFile(string filePath);
        public Task<IResult> GetFile(string filePath);
        public Task<IResult> GetFile(int fileId);
    }

    public class FileManager : IFileManager
    {
        private readonly IWebHostEnvironment _appEnvironment;
        public FileManager(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        /// <param name="FileTable">Название таблицы к которой относится изображение(Pictures, Players, Teams)</param>
        public async Task<IResult> UploadFile(IFormFile? uploadedFile)
        {
            if (uploadedFile != null)
            {
                //var extension = uploadedFile.FileName.Split('.')[1];
                //if (extension != "png" && extension != "jpg" && extension != "jepg")
                //{
                //    result.StatusCode = 400;
                //    result.Value = "Неверное расширение файла";
                //    return result;
                //}
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.ContentRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                    fileStream.Flush();
                }

                using (var AppContext = new ApplicationContext())
                {
                    var media = (await AppContext.Media.AddAsync(new Media() {src = path})).Entity;
                    await AppContext.SaveChangesAsync();
                    return Results.Ok(media);
                }
            }

            return Results.NotFound("You download empty file");
        }

        public async Task<IResult> DeleteFile(string filePath)
        {
            var fullFilePath = _appEnvironment.ContentRootPath + filePath;
            if (File.Exists(fullFilePath))
            {
                try
                {
                    File.Delete(fullFilePath);
                    using (var AppContext = new ApplicationContext())
                    {
                        var deleteFile = await AppContext.Media.FirstOrDefaultAsync(m => m.src == filePath);
                        AppContext.Remove(deleteFile);
                        await AppContext.SaveChangesAsync();
                        return Results.Ok(deleteFile);
                    }
                }
                catch (Exception e)
                {
                    return Results.BadRequest($"The deletion failed: {e.Message}");
                }
            }
            return Results.NotFound("Specified file doesn't exist");
        }

        public async Task<IResult> GetFile(string filePath)
        {
            using (var AppContext = new ApplicationContext())
            {
                var file = await AppContext.Media.FirstOrDefaultAsync(m => m.src == filePath);
                return Results.Ok(file);
            }
        }

        public async Task<IResult> GetFile(int fileId)
        {
            using (var AppContext = new ApplicationContext())
            {
                var file = await AppContext.Media.FirstOrDefaultAsync(m => m.id == fileId);
                return Results.Ok(file);
            }
        }
    }
}
