using System.ComponentModel.DataAnnotations;

namespace todolist.models
{
    public class todo
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
