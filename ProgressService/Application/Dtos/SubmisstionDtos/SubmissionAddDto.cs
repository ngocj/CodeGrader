namespace Application.Dtos.SubmisstionDtos
{
    public class SubmissionAddDto
    {
        public int ProblemId { get; set; }
        public string Language { get; set; }
        public int Point { get; set; }
        public EvaluationCriteriaDto EvaluationCriteria { get; set; }
    }
}
