using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskList.Models;

namespace TaskList.Services
{
    public class TaskService
    {
        private readonly DatabaseContext _context;

        public TaskService()
        {
            _context = new DatabaseContext();
            _context.Database.EnsureCreated();
        }

        public async Task<List<TodoTask>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task AddTaskAsync(TodoTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TodoTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TodoTask task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
