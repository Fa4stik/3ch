using _3ch.Model;
using _3ch.Model.Responses;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DAL
{
    public static class ExtensionGenericRepository
    {
        public async static Task<IEnumerable<PostResponse>> GetPostsByTag(this GenericRepository<Post> repository, string shortNameTag, int start, int end)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return (await repository.dbSet.ToListAsync()).Where(x => unitOfWork.TagRepository.Get(x.tag).shortName == shortNameTag).OrderByDescending(x => x.id).Take(new Range(start,end))
                .Select(x =>
                {
                    return new PostResponse()
                    {
                        id = x.id,
                        content = x.content,
                        date = x.date,
                        heading = x.heading,
                        mediaId = x.mediaId,
                        tag = x.tag,
                        mediaSrc = x.mediaId.HasValue ? unitOfWork.MediaRepository.Get(x.mediaId.Value).src : null,
                        tagName = unitOfWork.TagRepository.Get(x.tag).name,
                        tagShortName = unitOfWork.TagRepository.Get(x.tag).shortName
                    };
                });
        }
    }
}
