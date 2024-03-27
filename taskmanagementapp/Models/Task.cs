namespace taskmanagementapp.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public ICollection<string> ImageUrls { get; set; } = new List<string>();

        public int ColumnId { get; set; } 
        public Column Column { get; set; }
    }
}
