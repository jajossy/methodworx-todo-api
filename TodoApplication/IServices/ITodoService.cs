using TodoApplication.Models.Custom;
using TodoApplication.Models.Generated;

namespace TodoApplication.IServices
{
    public interface ITodoService
    {
        Task<List<UserTodo>> GetTodos();
        Task<List<UserTodo>> GetTodoByUser(Guid userId);
        Task<UserTodo> AddTodo(TodoPost todo);
        Task<UserTodo> EditTodo(editInfo todo);
        Task<UserTodo> TodoIsDone(Guid todoId);
        Task<UserTodo> DeleteTodo(Guid todoId);
    }
}
