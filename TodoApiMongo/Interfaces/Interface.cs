using ErrorOr;
using TodoApiMongo.Models;

namespace TodoApiMongo.NewFolder
{
    public interface Interface
    {
        public Task<ErrorOr<TodoItem>> CreateTarea(string name,
                                                   bool isComplete);
        public Task<ErrorOr<TodoItem>> UpdateTarea(string id,
                                                   string name,
                                                   bool isComplete);

        public Task<ErrorOr<string>> DeleteTarea(string id);

        public Task<ErrorOr<TodoItem>> GetTarea(string id);

        public Task<List<TodoItem>> GetAll();
    }


}
