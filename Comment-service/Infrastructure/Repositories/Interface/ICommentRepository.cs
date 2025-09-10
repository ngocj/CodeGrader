using Domain.Entities;

namespace Infrastructure.Repositories.Interface
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
      Task<IEnumerable<Comment>> GetCommentByProblemId(int problemId);
      Task<IEnumerable<Comment>> SortCommentByLike(int problemId);     
      Task<IEnumerable<Comment>> SortCommentByCreatedAt(int problemId);
    }
}
