using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(ILogger<UsuarioController> logger,
        IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try{
                var usuario = _usuarioRepository.GetUsuario(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var usuarios = _usuarioRepository.GetAllUsuarios();
                return Ok(usuarios);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Usuario usuario)
        {
            try{
                _usuarioRepository.CreateUsuario(usuario);
                return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
            }
            catch{
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if(_usuarioRepository.UpdateUsuario(id,usuario))
                {
                    return NoContent();
                }
                else{
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            try{
                if (_usuarioRepository.DeleteUsuario(id))
                {
                    return NoContent();
                }else{
                    return NotFound();
                }
            }catch{
                return StatusCode(500);
            }
        }
    }
}