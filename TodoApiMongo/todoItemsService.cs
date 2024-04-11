using ErrorOr;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Xml.Linq;
using TodoApiMongo.Models;
using TodoApiMongo.NewFolder;
using TodoApiMongo.StaticClasses;

namespace TodoApiMongo
{
    public class todoItemsService : Interface
    {
        public async Task<ErrorOr<TodoItem>> CreateTarea(string name, bool isComplete)
        {
            try
            {
                var tarea = new TodoItem
                {
                    Name = name,
                    IsComplete = isComplete
                };

                await DBCollections.tareaCollection.InsertOneAsync(tarea);

                return tarea;
            }
            catch (Exception ex)
            {
                return new ErrorOr<TodoItem>();
            }
        }

        public async Task<ErrorOr<string>> DeleteTarea(string id)
        {
            try
            {
                var tarea = await DBCollections.tareaCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
                //DBCollections.tareaCollection.DeleteOne(tarea.Id);
                if (tarea == null)
                {
                    return new ErrorOr<string>();
                }
                var result = await DBCollections.tareaCollection.DeleteOneAsync(t => t.Id == id);
                if (result.DeletedCount == 0)
                {
                    return new ErrorOr<string>();
                }
                return "Tarea eliminada correctamente";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la tarea.", ex);
            }
        }


        public async Task<List<TodoItem>> GetAll() =>
        await DBCollections.tareaCollection.Find(_ => true).ToListAsync();


        public async Task<ErrorOr<TodoItem>> GetTarea(string id)
        {
            try
            {
                var tarea = await DBCollections.tareaCollection.Find(t => t.Id == id).FirstOrDefaultAsync();

                if (tarea == null)
                    return new ErrorOr<TodoItem>();

                return tarea;
            }
            catch (Exception ex)
            {
                return new ErrorOr<TodoItem>();
            }
        }



        public async Task<ErrorOr<TodoItem>> UpdateTarea(string id, string name, bool isComplete)
        {
            try
            {
                var filter = Builders<TodoItem>.Filter.Eq(t => t.Id, id);
                var update = Builders<TodoItem>.Update
                    .Set(t => t.Name, name)
                    .Set(t => t.IsComplete, isComplete);

                var tarea = await DBCollections.tareaCollection.FindOneAndUpdateAsync(filter, update);

                return tarea;
            }
            catch (Exception ex)
            {
                return new ErrorOr<TodoItem>();
            }
        }

        public Task<ErrorOr<TodoItem>> UpdateTarea(string name, bool isComplete)
        {
            throw new NotImplementedException();
        }
    }
}
