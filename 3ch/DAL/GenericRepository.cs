using _3ch.Model;
using Microsoft.EntityFrameworkCore;

namespace _3ch.DAL
{
    public class GenericRepository<TEntity>
        where TEntity : class
    {
        internal ApplicationContext context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity item)
        {
            return dbSet.Add(item).Entity;
        }

        public void Delete(int id)
        {
            TEntity entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public int Count() 
            => dbSet.Count();

        public TEntity? Get(int id)
        {
            TEntity? entity = dbSet.Find(id);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetList() 
            => await dbSet.ToListAsync();

        public async Task<IEnumerable<TEntity>> GetList(int start, int end)
        {
            var set = (await dbSet.ToListAsync()).Take(new Range(start, end));
            return set;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            dbSet.Update(item);
        }
    }
}
