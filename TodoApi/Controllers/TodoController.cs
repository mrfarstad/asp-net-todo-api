using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0) {
                _context.TodoItems.Add(new TodoItem { Name = "Dra på intervju" });
                _context.SaveChanges();
            }

        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAllTodos()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetTodoItemById(long id)
        {
            var item = _context.TodoItems.FirstOrDefault<TodoItem>(todo => todo.Id == id);
            if (item == null) {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        // The[FromBody] attribute tells MVC to get the value of the to-do item
        // from the body of the HTTP request.
        public IActionResult CreateTodo([FromBody] TodoItem item)
        {
            if (item == null) {
                return BadRequest();
            }

            _context.TodoItems.Add(item);
            // Saves the changed made to the content to the underlying database
            _context.SaveChanges();

            // Returns a response which includes a location for the
            // newly created item
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();   
            }

            var todo = _context.TodoItems.FirstOrDefault<TodoItem>(t => t.Id == id);
            if (todo == null)
            {
                return BadRequest();
            }

            todo.Name = item.Name;
            todo.isComplete= item.isComplete;

            _context.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(long id)
        {
            var item = _context.TodoItems.FirstOrDefault<TodoItem>(todo => todo.Id == id);
            if (item == null) {
                return NotFound();
            }

            _context.TodoItems.Remove(item);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
