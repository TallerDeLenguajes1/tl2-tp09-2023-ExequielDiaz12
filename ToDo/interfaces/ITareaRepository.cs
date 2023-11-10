using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.interfaces
{
    public interface ITareaRepository
    {
        void CreateTarea(Tarea tarea);
        bool UpdateTarea(int id, Tarea tarea);
        Tarea GetTareaById(int id);
        IEnumerable<Tarea> GetTareas();
        IEnumerable<Tarea> GetTareasByTablero(int idTablero);
        bool DeleteTarea(int idTarea);
        bool TaskToUser(int idUsuario, int idTarea);
    }
}