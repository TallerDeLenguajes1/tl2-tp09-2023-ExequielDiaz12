using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.interfaces
{
    public interface ITableroRepository
    {
        IEnumerable<Tablero> GetAllTableros();
        Tablero GetTablero(int id);
        void CreateTablero(Tablero tablero);
        bool UpdateTablero(int id, Tablero tablero);
        bool DeleteTablero(int id);
    }
}