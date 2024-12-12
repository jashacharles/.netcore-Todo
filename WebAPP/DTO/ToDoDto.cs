namespace WebAPP.DTO;

public class ToDoDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueTime { get; set; }
    public bool IsCompleted { get; set; }
    
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}
