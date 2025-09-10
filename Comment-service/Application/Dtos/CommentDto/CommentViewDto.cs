namespace Application.Dtos.CommentDto
{
    public class CommentViewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProblemId { get; set; }
        public string CommentText { get; set; }
        public int? ParentCommentId { get; set; }
        public int Like { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CommentViewDto> Replies { get; set; } = new List<CommentViewDto>();
    }
}
