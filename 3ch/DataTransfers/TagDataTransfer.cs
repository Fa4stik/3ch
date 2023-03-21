using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DataTransfers
{
    public class TagDataTransfer
    {
        public static async Task<Tag> GetTagById(int idTag)
        {
            using (var AppContext = new ApplicationContext())
                return await AppContext.Tag.FirstOrDefaultAsync(t => t.id == idTag);
        }

        public static async Task<IEnumerable<Tag>> GetTagBetween(int startIndex, int endIndex=0)
        {
            using (var AppContext = new ApplicationContext())
                return await AppContext.Tag.Take(new Range(startIndex, endIndex)).ToListAsync();
        }

        public static async Task<int> GetTagCount()
        {
            using (var AppContext = new ApplicationContext())
                return AppContext.Tag.Count();
        }
    }
}