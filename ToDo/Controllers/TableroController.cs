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
    public class TableroController : Controller
    {
        private readonly ILogger<TableroController> _logger;
        private readonly ITableroRepository _tableroRepository;

        public TableroController(ILogger<TableroController> logger,
        ITableroRepository tableroRepository)
        {
            _logger = logger;
            _tableroRepository = tableroRepository;
        }

        [HttpGet]
        public IActionResult GetAllTableros()
        {
            try
            {
                var tableros = _tableroRepository.GetAllTableros();
                return Ok(tableros);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var tablero = _tableroRepository.GetTablero(id);
                if(tablero == null)
                {
                    return NotFound();
                }
                return Ok(tablero);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Tablero tablero)
        {
            try
            {
                _tableroRepository.CreateTablero(tablero);
                return CreatedAtAction(nameof(Get), new { id = tablero.Id }, tablero);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Tablero tablero)
        {
            try
            {
                if(_tableroRepository.UpdateTablero(id,tablero))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if(_tableroRepository.DeleteTablero(id))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}