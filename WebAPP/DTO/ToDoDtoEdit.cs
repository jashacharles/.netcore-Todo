namespace WebAPP.DTO;

public class ToDoDtoEdit
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueTime { get; set; }
    public bool IsCompleted { get; set; }
}