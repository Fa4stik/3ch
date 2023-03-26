using _3ch.Model;

namespace _3ch.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationContext context = new ApplicationContext();
        private GenericRepository<Comment> commentRepository;
        private GenericRepository<Post> postRepository;
        private GenericRepository<Tag> tagRepository;
        private GenericRepository<Media> mediaRepository;

        public GenericRepository<Media> MediaRepository
        {
            get
            {
                if (this.mediaRepository == null)
                {
                    this.mediaRepository = new GenericRepository<Media>(context);
                }
                return mediaRepository;
            }
        }

        public GenericRepository<Tag> TagRepository
        {
            get
            {
                if (this.tagRepository == null)
                {
                    this.tagRepository = new GenericRepository<Tag>(context);
                }
                return tagRepository;
            }
        }

        public GenericRepository<Post> PostRepository
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new GenericRepository<Post>(context);
                }
                return postRepository;
            }
        }

        public GenericRepository<Comment> CommentRepository
        {
            get
            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new GenericRepository<Comment>(context);
                }
                return commentRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
