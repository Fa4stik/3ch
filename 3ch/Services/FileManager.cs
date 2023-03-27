using _3ch.DAL;
using _3ch.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace _3ch.Services
{
    public interface IFileManager
    {
        public Task<Media> UploadFile(IFormFile? uploadedFile);
        public Task<Media?> DeleteFile(string filePath);
        public Task<Media?> DeleteFile(int id);
        public Task<Media?> GetFile(string filePath);
        public Task<Media?> GetFile(int fileId);
    }

    public class FileManager : IFileManager
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _appEnvironment;
        public FileManager(IWebHostEnvironment appEnvironment, UnitOfWork unitOfWork)
        {
            _appEnvironment = appEnvironment;
            _unitOfWork = unitOfWork;
        }

        /// <param name="FileTable">Название таблицы к которой относится изображение(Pictures, Players, Teams)</param>
        public async Task<Media?> UploadFile(IFormFile? uploadedFile)
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

                var media = _unitOfWork.MediaRepository.Create(new Media() { src = path });
                _unitOfWork.Save();
                return media;
            }

            return null;
        }

        public async Task<Media?> DeleteFile(string filePath)
        {
            var fullFilePath = _appEnvironment.ContentRootPath + filePath;
            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
                var deleteFile = (await _unitOfWork.MediaRepository.GetList()).FirstOrDefault(m => m.src == filePath); 
                _unitOfWork.MediaRepository.Delete(deleteFile.id); 
                _unitOfWork.Save();
                return deleteFile;
            }
            return null;
        }

        public async Task<Media?> DeleteFile(int id)
        {
            var filePath = (await GetFile(id)).src;
            var fullFilePath = _appEnvironment.ContentRootPath + filePath;
            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
                var deleteFile = (await _unitOfWork.MediaRepository.GetList()).FirstOrDefault(m => m.src == filePath);
                _unitOfWork.MediaRepository.Delete(deleteFile.id);
                _unitOfWork.Save();
                return deleteFile;
            }
            return null;
        }

        public async Task<Media?> GetFile(string filePath)
        {
            var file = (await _unitOfWork.MediaRepository.GetList()).FirstOrDefault(m => m.src == filePath);
            return file;
        }

        public async Task<Media?> GetFile(int id)
        {
            var file = _unitOfWork.MediaRepository.Get(id);
            return file;
        }
    }
}
