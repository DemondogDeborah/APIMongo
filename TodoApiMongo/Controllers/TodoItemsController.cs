using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApiMongo.Models;
using TodoApiMongo.NewFolder;

namespace TodoApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        //private readonly TodoContext _context;
        private readonly Interface _service;

        public TodoItemsController(TodoContext context, Interface service)
        {
           // _context = context;
            _service = service;
        }


        // GET: api/TodoItems
        [HttpGet]
        public async Task<IActionResult> GetAll()    
        {
            List<TodoItem> tareaList = await _service.GetAll();

            if (tareaList == null || tareaList.Count == 0)
            {
                return NotFound(); 
            }

            return Ok(tareaList);
        }


        // GET: api/TodoItems/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarea(string id)
        {
            ErrorOr<TodoItem> getTareaResult = await _service.GetTarea(id);

            if (getTareaResult.Value == null)
            {
                return BadRequest();
            }

            return Ok(getTareaResult.Value);
        }


        // PUT: api/TodoItems/5
        [HttpPut]
        public async Task<IActionResult> UpdateTarea(TodoItem todoItem)
        {
           
            try
            {
                ErrorOr<TodoItem> updateTareaResult = await _service.UpdateTarea(todoItem.Id,
                                                                                 todoItem.Name,
                                                                                 todoItem.IsComplete);
                return Ok(updateTareaResult.Value);
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, "Error al actualizar la tarea: " + ex.Message);
            }
        }


        // POST: api/TodoItems
        [HttpPost]
        public async Task<IActionResult> TodoItem(TodoItem todoItem)
        {
            ErrorOr<TodoItem> createTareaResult = await _service.CreateTarea(todoItem.Name,
                                                                             todoItem.IsComplete);

            return Ok(createTareaResult.Value);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(string id)
        {

            try
            {
                ErrorOr<string> deleteTareaResult = await _service.DeleteTarea(id);
                return Ok(deleteTareaResult.Value);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Error al actualizar la tarea: " + ex.Message);
            }
        }
    }
}
