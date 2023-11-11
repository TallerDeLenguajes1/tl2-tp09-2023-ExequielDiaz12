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
    public class TareaRepository : ITareaRepository
    {
        private readonly IDbConnection _dbConnection;
        public TareaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public void CreateTarea(Tarea tarea)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "insert into Tarea (IdTablero, Nombre, Estado, Descripcion, Color, IdUsuarioAsignado) values (@IdTablero, @Nombre, @Estado, @Descripcion, @Color, @IdUsuarioAsignado)";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@IdTablero", tarea.IdTablero));
                    command.Parameters.Add(new SQLiteParameter("@Nombre", tarea.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@Estado", tarea.Estado));
                    command.Parameters.Add(new SQLiteParameter("@Descripcion", tarea.Descripcion));
                    command.Parameters.Add(new SQLiteParameter("@Color", tarea.Color));
                    command.Parameters.Add(new SQLiteParameter("@IdUsuarioAsignado", tarea.IdUsuarioAsignado));
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool DeleteTarea(int idTarea)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "delete from Tarea where Id = @Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("Id", idTarea));
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public Tarea GetTareaById(int id)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "select * from Tarea where Id = @Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Id", id));
                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return new Tarea
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                IdTablero = Convert.ToInt32(reader["IdTablero"]),
                                Nombre = reader["Nombre"].ToString(),
                                Estado = Convert.ToInt32(reader["Estado"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                Color = reader["Color"].ToString(),
                                IdUsuarioAsignado = Convert.ToInt32(reader["IdUsuarioAsignado"])
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public IEnumerable<Tarea> GetTareas()
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "select * from Tarea";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    using(var reader = command.ExecuteReader()){
                        var tareas = new List<Tarea>();
                        while(reader.Read()){
                            tareas.Add(new Tarea
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                IdTablero = Convert.ToInt32(reader["IdTablero"]),
                                Nombre = reader["Nombre"].ToString(),
                                Estado = Convert.ToInt32(reader["Estado"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                Color = reader["Color"].ToString(),
                                IdUsuarioAsignado = Convert.ToInt32(reader["IdUsuarioAsignado"])
                            });
                        }
                        return tareas;
                    }
                }
            }
        }

        public IEnumerable<Tarea> GetTareasByTablero(int idTablero)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "select * from Tarea where IdTablero = @IdTablero";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    using(var reader = command.ExecuteReader()){
                        var tareas = new List<Tarea>();
                        while(reader.Read()){
                            tareas.Add(new Tarea
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                IdTablero = Convert.ToInt32(reader["IdTablero"]),
                                Nombre = reader["Nombre"].ToString(),
                                Estado = Convert.ToInt32(reader["Estado"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                Color = reader["Color"].ToString(),
                                IdUsuarioAsignado = Convert.ToInt32(reader["IdUsuarioAsignado"])
                            });
                        }
                        return tareas;
                    }
                }
            }
        }

        public bool TaskToUser(int idUsuario, int idTarea)
        {
            Tarea tarea = GetTareaById(idTarea);
            tarea.IdUsuarioAsignado = idUsuario;
            return UpdateTarea(idTarea, tarea);
        }

        public bool UpdateTarea(int id, Tarea tarea)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "update Tarea set IdTablero = @IdTablero, Nombre = @Nombre, Estado = @Estado, Descripcion = @Descripcion, Color = @Color, IdUsuarioAsignado = @IdUsuarioAsignado where Id = @Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Id", id));
                    command.Parameters.Add(new SQLiteParameter("@IdTablero", tarea.IdTablero));
                    command.Parameters.Add(new SQLiteParameter("@Nombre", tarea.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@Estado", tarea.Estado));
                    command.Parameters.Add(new SQLiteParameter("@Descripcion", tarea.Descripcion));
                    command.Parameters.Add(new SQLiteParameter("@Color", tarea.Color));
                    command.Parameters.Add(new SQLiteParameter("@IdUsuarioAsignado", tarea.IdUsuarioAsignado));
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}