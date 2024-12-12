using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using WebAPP.DTO;
using WebAPP.Model;

namespace WebAPP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    // ACTION LIST: 
    // Display(Get) all todo card DONE, return Card List
    // Display(Get) one todo card DONE, return One Card
    // Create new todo card       DONE, return nothing 
    // Delete existing todo cards DONE, return nothing
    // Edit existing todo cards   DONE, return One Card
    
    // setting up _context to interact with the database
    // don't need to dispose 
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoDto>>> Get()
    {
        var allToDos = await _context.ToDoLists.ToListAsync();
        for (int i = allToDos.Count - 1; i >= 0; i--) // Loop backwards to safely remove items
        {
            if (allToDos[i].IsDeleted)
            {
                allToDos.RemoveAt(i);
            }
        }
        var toDo = allToDos.Select(todo => new ToDoDto
        {
            Title = todo.Title,
            Description = todo.Description,
            DueTime = todo.DueTime,
            IsCompleted = todo.IsCompleted,
            CreateTime = todo.CreateTime,
            UpdateTime = todo.UpdateTime
        });
        return Ok(toDo);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoDto>> Get(int id)
    {
        var toDoCard = await _context.ToDoLists.SingleOrDefaultAsync(t => t.Id == id);
        if (toDoCard == null)
        {
            return NotFound("To do card not found ");
        }
        if (toDoCard.IsDeleted)
        {
            return NotFound("To do card not found ");
        }

        var toDoDto = new ToDoDto()
        {
            Title = toDoCard.Title,
            Description = toDoCard.Description,
            DueTime = toDoCard.DueTime,
            IsCompleted = toDoCard.IsCompleted,
            CreateTime = toDoCard.CreateTime,
            UpdateTime = toDoCard.UpdateTime
        };
        return Ok(toDoDto);
    }

    [HttpGet("GetKeyWord")]
    public  async Task<ActionResult<ToDoDto>> GetKeyWord(string title, string? description)
    {
        var allToDos = await _context.ToDoLists.ToListAsync();
        for (int i = allToDos.Count - 1; i >= 0; i--) // Loop backwards to safely remove items
        {
            if (allToDos[i].IsDeleted)
            {
                allToDos.RemoveAt(i);
            }
        }
        var filterRes = allToDos.Select(todo => new ToDoDto
        {
            Title = todo.Title,
            Description = todo.Description,
            DueTime = todo.DueTime,
            IsCompleted = todo.IsCompleted,
            CreateTime = todo.CreateTime,
            UpdateTime = todo.UpdateTime
        }); 
        //start searching 
        if (!string.IsNullOrEmpty(title))
        {
            filterRes = filterRes.Where(a => a.Title.Contains(title));
            if (!string.IsNullOrEmpty(description))
            {
                filterRes = filterRes.Where(a => a.Description.Contains(description));
            }
            return Ok(filterRes);
        }
        return NotFound("To do card not found ");
    }

    [HttpPost]
    public async Task<ActionResult<ToDoList>> Create([FromBody] ToDoDtoCreate dto)
    {
        var newCard = new ToDoList 
        {
            Title = dto.Title,
            Description = dto.Description,
            DueTime = dto.DueTime,
            
            CreateTime = DateTime.UtcNow,
            UpdateTime = DateTime.UtcNow
        };
        _context.ToDoLists.Add(newCard);
        await _context.SaveChangesAsync();
        var updatedList = await _context.ToDoLists.ToListAsync();
        return Ok(updatedList);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ToDoList>> Edit(int id, ToDoDtoEdit dto)
    {
        var originalToDoCard = await _context.ToDoLists.SingleOrDefaultAsync(t => t.Id == id);
        if (originalToDoCard == null)
        {
            return NotFound();
        }
        originalToDoCard.Title = dto.Title;
        originalToDoCard.Description = dto.Description;
        originalToDoCard.DueTime = dto.DueTime;
        originalToDoCard.IsCompleted = dto.IsCompleted;
        originalToDoCard.UpdateTime = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        var updatedList = await _context.ToDoLists.ToListAsync();
        return Ok(updatedList);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id,ToDoDtoDelete dto )
    {
        var toDoCard = await _context.ToDoLists.SingleOrDefaultAsync(t => t.Id == id);
        if (toDoCard == null)
        {
            return NotFound();
        }
        toDoCard.IsDeleted = dto.IsDeleted;
        await _context.SaveChangesAsync();
        var updatedList = await _context.ToDoLists.ToListAsync();
        return Ok(updatedList);
    }
}