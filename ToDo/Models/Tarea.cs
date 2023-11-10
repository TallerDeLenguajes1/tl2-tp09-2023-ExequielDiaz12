using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public int IdTablero { get; set; }
        public string? Nombre { get; set; }
        public int Estado { get; set; }
        public string? Descripcion { get; set; }
        public string? Color { get; set; }
        public int IdUsuarioAsignado { get; set; }
    }
}