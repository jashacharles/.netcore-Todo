namespace WebAPP.Model;

public class ToDoList
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueTime { get; set; }
    public bool IsCompleted { get; set; }
    
    public bool IsDeleted { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }   
}


// just to test git
/*
 * todo DTO for create 
 * id // won't show to user
 * title
 * description
 * due time
 * in completed 
 */

/*
 * todo DTO for edit
 * id 
 * title
 * description
 * due time
 * in completed
 * update time 
 */
 
/*
 * todo DTO for delete
 * id 
 * is deleted
 */