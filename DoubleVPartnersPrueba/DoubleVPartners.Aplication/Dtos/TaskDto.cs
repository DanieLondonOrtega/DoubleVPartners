namespace DoubleVPartners.Aplication.Dtos
{
    public class TaskDto
    {
        public int IdTask { get; set; }
        public int? IdUser { get; set; }
        public string NameTask { get; set; }
        public string Description { get; set; } 
        public string StatusTask { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
