using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDo.interfaces;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Route("[controller]")]
    public class TareaController : Controller
    {
        private readonly ILogger<TareaController> _logger;
        private readonly ITareaRepository _tareaRepository;

        public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository)
        {
            _logger = logger;
            _tareaRepository = tareaRepository;
        }

        [HttpPost]
        public IActionResult POST([FromBody]Tarea tarea)
        {
            try{
                _tareaRepository.CreateTarea(tarea);
                return CreatedAtAction(nameof(GET), new { id = tarea.Id }, tarea);
            }catch{
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GET(int id)
        {
            try{
                var tarea = _tareaRepository.GetTareaById(id);
                if(tarea == null){
                    return NotFound();
                }
                return Ok(tarea);
            }catch{
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PUT(int id, [FromBody]Tarea tarea)
        {
            try{
                if(_tareaRepository.UpdateTarea(id,tarea)){
                    return NoContent();
                }else{
                    return NotFound();
                }
            }catch{
                return StatusCode(500);
            }
        }

        [HttpPut("{id}/CambiarEstado")]
        public IActionResult CambiarEstado(int id,int estado)
        {
            try
            {
                var tarea = _tareaRepository.GetTareaById(id);
                tarea.Estado = estado;
                if(_tareaRepository.UpdateTarea(id,tarea)){
                    return NoContent();
                }else{
                    return NotFound();
                }
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DELETE(int id)
        {
            try{
                if(_tareaRepository.DeleteTarea(id)){
                    return NoContent();
                }else{
                    return NotFound();
                }
            }catch{
                return StatusCode(500);
            }
        }

        [HttpGet("{estado}/GET2")]
        public IActionResult GET2(int estado)//cantidad de tareas en un estado
        {
            try{
                var tareas = _tareaRepository.GetTareas();
                if(tareas == null){
                    return NotFound();
                }
                return Ok(tareas.Where(e => e.Estado == estado).ToList().Count());
            }catch{
                return StatusCode(500);
            }
        }

        [HttpGet("{idUsuario}/GET3")]
        public IActionResult GET3(int idUsuario)//listar tareas asignadas a un usuario
        {
            try{
                var tareas = _tareaRepository.GetTareaById(idUsuario);
                if(tareas==null){
                    return NotFound();
                }
                return Ok(tareas);
            }catch{
                return StatusCode(500);
            }
        }

        [HttpGet("{idTablero}/GET4")]     
        public IActionResult GET4(int idTablero)
        {
            try{
                var tareas = _tareaRepository.GetTareas().ToList();
                if(tareas==null){
                    return NotFound();
                }
                return Ok(tareas.Where(t =>t.IdTablero == idTablero));
            }catch{
                return StatusCode(500);
            }
        }
    }
}