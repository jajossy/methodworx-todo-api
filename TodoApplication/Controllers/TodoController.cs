using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.IServices;
using TodoApplication.Models.Custom;
using TodoApplication.Models.Generated;
using TodoApplication.Services;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<List<UserTodo>> Get()
        {
            return await _todoService.GetTodos();
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<List<UserTodo>> Get(Guid id)
        {
            return await _todoService.GetTodoByUser(id);
        }

        [HttpPost]
        public async Task<UserTodo> Post([FromBody] TodoPost todoInfo)
        {            
            return await _todoService.AddTodo(todoInfo);
        }

        [HttpPut]
        [Route("TodoDone/{id}")]
        public async Task<UserTodo> TodoDone(Guid id)
        {
            return await _todoService.TodoIsDone(id);
        }

        [HttpPut]        
        public async Task<UserTodo> Put(editInfo todoInfo)
        {
            return await _todoService.EditTodo(todoInfo);            
        }

        [HttpDelete]
        [Route("{todoId}")]
        public async Task<UserTodo> Delete(Guid todoId)
        {
            return await _todoService.DeleteTodo(todoId);
        }
    }
}
