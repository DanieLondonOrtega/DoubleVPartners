namespace DoubleVPartners.API.Models
{
    public class TaskModel
    {
        public int? IdUser { get; set; }
        public string NameTask { get; set; }
        public string Description { get; set; }
        public string StatusTask { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class TaskUpdateModel : TaskModel
    {
        public int IdTask { get; set; }
    }

    public class TaskChangeAssignModel
    {
        public int IdTask { get; set; }
        public string StatusTask { get; set; }
        public int IdUser { get; set; }
    }
}
