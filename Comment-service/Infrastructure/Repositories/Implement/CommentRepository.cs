using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implement
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(CMContext cMContext) : base(cMContext)
        {
        }
        public override async Task<Comment> GetById(int id)
        {
            var comment = await _cMContext.Comment.ToListAsync();

            var lookup = comment.ToLookup(c => c.ParentCommentId);

            Comment BuildTree(Comment c)
            {
                c.Replies = lookup[c.Id].Select(BuildTree).ToList();
                return c;
            }
            var rootComment = comment.FirstOrDefault(c => c.Id == id);
            return rootComment == null ? null : BuildTree(rootComment);
        }

        public async override Task<IEnumerable<Comment>> GetAllAsync()
        {
            var comment = await _cMContext.Comment.ToListAsync();

            var lookup = comment.ToLookup(c => c.ParentCommentId);

            List<Comment> BuildTree(int? parentId)
            {
                return lookup[parentId]
                    .Select(c => { c.Replies = BuildTree(c.Id); return c; })
                    .ToList();
            }
            return BuildTree(null);
        }

        public async Task<IEnumerable<Comment>> GetCommentByProblemId(int problemId)
        {
            var comment = await _cMContext.Comment.ToListAsync();

            var lookup = comment.ToLookup(c => c.ParentCommentId);

            List<Comment> BuildTree(int? parentId)
            {
                return lookup[parentId]
                    .Select(c => { c.Replies = BuildTree(c.Id); return c; })
                    .ToList();            
            }

            var rootComments = comment.Where(c => c.ProblemId == problemId && c.ParentCommentId == null).ToList();

            var CommentWithReplies = rootComments.Select(c => { c.Replies = BuildTree(c.Id); return c; }).ToList();

            return CommentWithReplies;
        }

        public async Task<IEnumerable<Comment>> SortCommentByLike(int problemId)
        {
            return await _cMContext.Comment
                .Include(c => c.Replies)
                .Include(c => c.ParentComment)
                .Where(c => c.ProblemId == problemId && c.ParentCommentId == null)
                .OrderByDescending(c => c.Like)
                .ThenByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> SortCommentByCreatedAt(int problemId)
        {
            return await _cMContext.Comment
                .Include(c => c.Replies)
                .Include(c => c.ParentComment)
                .Where(c => c.ProblemId == problemId && c.ParentCommentId == null)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
