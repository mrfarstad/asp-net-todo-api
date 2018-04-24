using System;
using Microsoft.EntityFrameworkCore;

// The database context is the main class that coordinates Entity Framework
// functionality for a given data model
namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
