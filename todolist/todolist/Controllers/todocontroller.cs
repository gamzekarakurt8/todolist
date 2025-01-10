using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolist.data;
using todolist.models;

namespace todolist.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly todocontext _context;


        public TodoController(todocontext context)
        {
            _context = context;
        }

        // Tüm görevleri listele (GET)
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _context.TodoItems.ToListAsync();
            return Ok(todos);
        }

        // Tek bir görevi al (GET)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound($"Todo item with ID {id} not found.");
            }

            return Ok(todo);
        }

        // Yeni bir görev oluştur (POST)
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] todo newTodo)
        {
            if (newTodo == null)
            {
                return BadRequest("Todo item cannot be null.");
            }

            await _context.TodoItems.AddAsync(newTodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoById), new { id = newTodo.Id }, newTodo);
        }

        // Mevcut bir görevi güncelle (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] todo updatedTodo)
        {
            if (updatedTodo == null || id != updatedTodo.Id)
            {
                return BadRequest("Invalid data.");
            }

            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound($"Todo item with ID {id} not found.");
            }

            todo.Id = updatedTodo.Id;
            todo.Title = updatedTodo.Title;
            todo.IsCompleted = updatedTodo.IsCompleted;

            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync();

            return Ok(todo);
        }

        // Bir görevi sil (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound($"Todo item with ID {id} not found.");
            }

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
