using Infrastructure.Repositories.Interface;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public ICommentRepository CommentRepository { get; }
        Task<int> SaveChangeAsync();
    }
}
