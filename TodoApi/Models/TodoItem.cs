using System;
namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Boolean isComplete { get; set; }
    }
}
