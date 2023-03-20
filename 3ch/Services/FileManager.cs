using Microsoft.AspNetCore.Mvc;

namespace _3ch.Services
{
    public class FileManager
    {
        private IWebHostEnvironment appEnvironment;
        public FileManager(IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }

        /// <param name="FileTable">Название таблицы к которой относится изображение(Pictures, Players, Teams)</param>
        public async Task<CreatedAtActionResult> UploadFile(IFormFile uploadedFile, CreatedAtActionResult result)
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
                using (var fileStream = new FileStream(appEnvironment.ContentRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                result.StatusCode = 200;
                result.Value = path;
                return result;
            }
            else
            {
                result.StatusCode = 400;
                result.Value = "Вы отправили пустой объект";
                return result;
            }
        }
        [HttpDelete(Name = "DeleteFile")]
        public async Task<IResult> DeleteFile(string FilePath)
        {
            var allPath = appEnvironment.ContentRootPath + FilePath;
            if (File.Exists(allPath))
            {
                try
                {
                    File.Delete(allPath);
                }
                catch (Exception e)
                {
                    Results.StatusCode(400);
                    Results.Text("The deletion failed: {0}", e.Message);
                }
            }
            else
            {
                Results.StatusCode(404);
                Results.Text("Specified file doesn't exist");
            }
            return Results.Ok();
        }        
    }
}
