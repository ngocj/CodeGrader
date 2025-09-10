namespace Domain.Entities
{
    public class ProblemStats : IEntityId
    {
        public int Id { get; set; }
        public int TotalSubmisstion { get; set; }
        public int AvgPoint { get; set; }
    }
}
