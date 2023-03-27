using _3ch.DAL;
using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DataTransfers
{
    public class TagDataTransfer
    {
        private static UnitOfWork _unitOfWork;
        public TagDataTransfer(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static async Task<Tag> GetTag(int idTag) 
            => _unitOfWork.TagRepository.Get(idTag);

        public static async Task<IEnumerable<Tag>> GetTag(int startIndex, int endIndex)
            => await _unitOfWork.TagRepository.GetList(startIndex, endIndex);
    }
}