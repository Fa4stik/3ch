using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DAL
{
    public static class ExtensionGenericRepository
    {
        public async static Task<IEnumerable<Post>> GetPostsByTag(this GenericRepository<Post> repository, string shortNameTag, int start, int end)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return (await repository.dbSet.ToListAsync()).Where(x => unitOfWork.TagRepository.Get(x.tag).shortName == shortNameTag).OrderByDescending(x => x.id).Take(new Range(start,end));
        }
    }
}
