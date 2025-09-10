namespace Application.Dtos.CommentDto
{
    public class CreateCommentDto
    {
        public int ProblemId { get; set; }
        public int? ParentCommentId { get; set; }
        public string CommentText { get; set; }
    }
}
