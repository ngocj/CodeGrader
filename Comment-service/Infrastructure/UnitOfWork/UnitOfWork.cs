using Infrastructure.Context;
using Infrastructure.Repositories.Implement;
using Infrastructure.Repositories.Interface;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected CMContext _CMContext;

        public UnitOfWork(CMContext CMContext)
        {
            _CMContext = CMContext;
            CommentRepository = new CommentRepository(_CMContext);
        }

        public ICommentRepository CommentRepository { get; }

        public void Dispose()
        {
            _CMContext.Dispose();       
        }

        public Task<int> SaveChangeAsync()
        {
            return _CMContext.SaveChangesAsync();
        }
    }
}
