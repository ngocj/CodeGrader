namespace Domain.Entities
{
    public class UserProgress : IEntityId
    {
        public int Id { get; set; }
        public int TotalSubmisstion { get; set; }
        public int EasySolved { get; set; } //hệ số: 1
        public int MediumSolved { get; set; } // hệ số: 2
        public int HardSolved { get; set; } // hệ số 3:

        // cách tính rank:  tổng điểm các bài nhân theo hệ số
        public int Rank { get; set; }
    }
}
