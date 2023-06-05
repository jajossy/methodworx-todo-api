using Microsoft.EntityFrameworkCore;
using TodoApplication.IServices;
using TodoApplication.Models.Custom;
using TodoApplication.Models.Generated;
using TodoApplication.Repository;

namespace TodoApplication.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository _repository;

        public TodoService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserTodo>> GetTodos()
        {
            return await _repository.Query<UserTodo>().ToListAsync();
        }

        public async Task<List<UserTodo>> GetTodoByUser(Guid userId)
        {
            return await _repository.Query<UserTodo>().Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<UserTodo> AddTodo(TodoPost todoInfo)
        {
            UserTodo todo = new UserTodo();
            todo.Description = todoInfo.Description;
            todo.TodoDate = todoInfo.TodoDate;
            todo.UserId = todoInfo.UserId;
            todo.CreatedDate = DateTimeOffset.UtcNow;
            return await _repository.CreateAsync<UserTodo>(todo);
        }

        public async Task<UserTodo> EditTodo(editInfo todo)
        {
            var entity = await _repository.Query<UserTodo>().FirstOrDefaultAsync(x => x.TodoId == todo.TodoId);           
            if (entity != null)
            {
                entity.EditedDate = DateTimeOffset.UtcNow;
                entity.Description = todo.Description;
                //entity.TodoDate = todo.TodoDate;
                return await _repository.PatchAsync<UserTodo>(entity);
            }                
            else return null;
        }

        public async Task<UserTodo> TodoIsDone(Guid todoId)
        {
            var entity = await _repository.Query<UserTodo>().FirstOrDefaultAsync(x => x.TodoId == todoId);
            if (entity != null)
            {
                entity.EditedDate = DateTimeOffset.UtcNow;                
                entity.IsDone = true;
                return await _repository.PatchAsync<UserTodo>(entity);
            }
            else return null;
        }


        public async Task<UserTodo> DeleteTodo(Guid todoId)
        {
            var entity = await _repository.Query<UserTodo>().FirstOrDefaultAsync(x => x.TodoId == todoId);
            if (entity != null)
                return await _repository.DeleteAsync<UserTodo>(entity);
            else return null;
        }
    }
}
