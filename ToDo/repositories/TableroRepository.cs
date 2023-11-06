using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using ToDo.interfaces;
using ToDo.Models;

namespace ToDo.repositories
{
    public class TableroRepository : ITableroRepository
    {
        private readonly IDbConnection _dbConnection;
        public TableroRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public void CreateTablero(Tablero tablero)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "insert into Tablero (IdUsuario, Nombre, Descripcion) values (@IdUsuario,@Nombre,@Descripcion)";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@IdUsuario", tablero.IdUsuario));
                    command.Parameters.Add(new SQLiteParameter("@Nombre", tablero.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@Descripcion", tablero.Descripcion));
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool DeleteTablero(int id)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "delete from Tablero where Id = @Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("Id", id));
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public IEnumerable<Tablero> GetAllTableros()
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "select * from Tablero";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    using(var reader = command.ExecuteReader()){
                        var tableros = new List<Tablero>();
                        while(reader.Read()){
                            tableros.Add(new Tablero
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            });
                        }
                        return tableros;
                    }
                }
            }
        }

        public Tablero GetTablero(int id)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "select * from Tablero where Id = @Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Id", id));
                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return new Tablero
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString()
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public bool UpdateTablero(int id, Tablero tablero)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "update Tablero set IdUsuario = @IdUsuario, Nombre = @Nombre, Descripcion = @Descripcion where Id = @Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Id", id));
                    command.Parameters.Add(new SQLiteParameter("@IdUsuario", tablero.IdUsuario));
                    command.Parameters.Add(new SQLiteParameter("@Nombre", tablero.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@Descripcion", tablero.Descripcion));
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}