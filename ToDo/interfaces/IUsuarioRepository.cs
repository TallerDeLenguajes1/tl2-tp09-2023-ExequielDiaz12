using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAllUsuarios();
        Usuario GetUsuario(int id);
        void CreateUsuario(Usuario Usuario);
        bool UpdateUsuario(int id, Usuario Usuario);
        bool DeleteUsuario(int id);
    }
}