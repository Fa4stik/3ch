using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DataTransfers
{
    public class TagDataTransfer
    {
        public static async Task<IResult> GetTagById(int idTag)
        {
            await using var appContext = new ApplicationContext();
            return Results.Ok(await appContext.Tag.FirstOrDefaultAsync(t => t.id == idTag));
        }

        public static async Task<IResult> GetTagBetween(int startIndex, int endIndex)
        {
            await using var appContext = new ApplicationContext();
            return Results.Ok((await appContext.Tag.ToListAsync()).Take(new Range(startIndex, endIndex)));
        }

        public static async Task<IResult> GetTagCount()
        {
            await using var appContext = new ApplicationContext();
            return Results.Ok(appContext.Tag.Count());
        }
    }
}