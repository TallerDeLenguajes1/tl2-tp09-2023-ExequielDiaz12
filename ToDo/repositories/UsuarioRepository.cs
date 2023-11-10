using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;
        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public IEnumerable<Usuario> GetAllUsuarios()
        {
            using (var connection = _dbConnection)
            {
                connection.Open();
                var query = "select * from Usuario";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    using(var reader = command.ExecuteReader())
                    {
                        var usuarios = new List<Usuario>();
                        while(reader.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString()
                            });
                        }
                        return usuarios;
                    }
                }
            }
        }
        public Usuario GetUsuario(int id)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "select * from Usuario where Id = @Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Id", id));
                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read()){
                            return new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString()
                            };
                        }
                        return null;
                    }
                }
            }
        }
        public bool UpdateUsuario(int id, Usuario usuario)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "update Usuario set Nombre = @Nombre where Id=@Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Id", id));
                    command.Parameters.Add(new SQLiteParameter("@Nombre", usuario.Nombre));
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public void CreateUsuario(Usuario usuario)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "insert into Usuario (Nombre) values (@Nombre)";
                using ( var command = new SQLiteCommand(query, (SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Nombre", usuario.Nombre));
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool DeleteUsuario(int id)
        {
            using(var connection = _dbConnection)
            {
                connection.Open();
                var query = "delete from Usuario where Id = @Id";
                using(var command = new SQLiteCommand(query,(SQLiteConnection)connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Id", id));
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}