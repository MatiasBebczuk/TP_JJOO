using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=JJOO; Trusted_Connection=True;";

    // Listas estáticas 
    public static List<Deportista> Deportistas = new List<Deportista>();
    public static List<Deporte> Deportes = new List<Deporte>();
    public static List<Pais> Paises = new List<Pais>();

    // agregar un deportista a la base de datos
    public static void AgregarDeportista(Deportista dep)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sqlQuery = "INSERT INTO Deportistas (Nombre, Apellido, IdDeporte, IdPais, Foto, FechaNacimiento) VALUES (@Nombre, @Apellido, @IdDeporte, @IdPais, @Foto, @FechaNacimiento)";
            db.Execute(sqlQuery, new { Nombre = dep.Nombre, Apellido = dep.Apellido, IdDeporte = dep.IdDeporte, IdPais = dep.IdPais, Foto = dep.Imagen, FechaNacimiento = dep.FechaNacimiento });
            // Agregar el deportista a la lista estática
            Deportistas.Add(dep);
        }
    }

    // eliminar un deportista de la base de datos
   
    public static void EliminarDeportista(int idDeportista)
    {
        string sql = "DELETE FROM Deportistas WHERE IdDeportista = @pIdDeportista";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new { pIdDeportista = idDeportista });
        }
    } 


    //obtener información de un deporte específico
    public static Deporte VerInfoDeporte(int idDeporte)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportes WHERE IdDeporte = @IdDeporte";
            return connection.QueryFirstOrDefault<Deporte>(sql, new { IdDeporte = idDeporte });
        }
    }

    // obtener información de un país específico
    public static Pais VerInfoPais(int idPais)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Paises WHERE IdPais = @IdPais";
            return connection.QueryFirstOrDefault<Pais>(sql, new { IdPais = idPais });
        }
    }

    // obtener data de un deportista específico
    public static Deportista VerInfoDeportista(int idDeportista)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdDeportista = @IdDeportista";
            return connection.QueryFirstOrDefault<Deportista>(sql, new { IdDeportista = idDeportista });
        }
    }

    // listar todos los países
    public static List<Pais> ListarPaises()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Paises";
            Paises = connection.Query<Pais>(sql).AsList();
            return Paises;
        }
    }

    // listar todos los deportes
    public static List<Deporte> ListarDeportes()
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Deportes";
            Deportes = con.Query<Deporte>(query).AsList();
            return Deportes;
        }
    }

    //  listar deportistas en base a un deporte específico
    public static List<Deportista> ListarDeportistas(int idDeporte)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdDeporte = @IdDeporte";
            Deportistas = connection.Query<Deportista>(sql, new { IdDeporte = idDeporte }).AsList();
            return Deportistas;
        }
    }

    // listar deportistas en base a un país específico
    public static List<Deportista> ListarDeportistasPorPais(int idPais)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdPais = @IdPais";
            Deportistas = connection.Query<Deportista>(sql, new { IdPais = idPais }).AsList();
            return Deportistas;
        }
    }
}