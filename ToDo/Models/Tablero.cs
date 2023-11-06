using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Tablero
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}