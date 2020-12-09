using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroEnfermedades.Models;
using System.Data;
using System.Data.SqlClient;

namespace RegistroEnfermedades.Azure
{
    public class UsuarioAzure
    {
        static string connectionString = @"Server=DESKTOP-C1E27D4\SQLEXPRESS01;Database=REGISTROENFERMEDADES2;Trusted_Connection=True;";


        private static List<Usuario> usuarios;

        public static List<Usuario> ObtenerUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from USUARIO";

                var comando = ConsultaSqlUsuario(connection, consultaSql);

                var dataTableUsuarios = LlenarDataTable(comando);

                return LLenadoUsuarios(dataTableUsuarios);
            }
        }

        public static Usuario ObtenerUsuarioPorRut(string rut)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from USUARIO where RUT = {rut}";

                var comando = ConsultaSqlUsuario(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionUsuario(dataTable);
            }
        }

        public static Usuario ObtenerUsuarioPorNombre(string nombre)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from USUARIO where NOMBRE = '{nombre}'";

                var comando = ConsultaSqlUsuario(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionUsuario(dataTable);

            }
        }

        public static int AgregarUsuario(Usuario usuario)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into USUARIO (RUT,NOMBRE,APELLIDO,FECHANAC) values (@RUT,@NOMBRE,@APELLIDO,@FECHANAC)";
                sqlCommand.Parameters.AddWithValue("@RUT", usuario.RUT);
                sqlCommand.Parameters.AddWithValue("@NOMBRE", usuario.NOMBRE);
                sqlCommand.Parameters.AddWithValue("@APELLIDO", usuario.APELLIDO);
                sqlCommand.Parameters.AddWithValue("@FECHANAC", usuario.FECHANAC);


                try
                {
                    connection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
            return filasAfectadas;
        }

        public static int AgregarUsuarioParametros(string rut, string nombre, string apellido, DateTime fechanac)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into USUARIO (RUT,NOMBRE,APELLIDO,FECHANAC) values (@RUT,@NOMBRE,@APELLIDO,@FECHANAC)";
                sqlCommand.Parameters.AddWithValue("@RUT", rut);
                sqlCommand.Parameters.AddWithValue("@NOMBRE", nombre);
                sqlCommand.Parameters.AddWithValue("@APELLIDO", apellido);
                sqlCommand.Parameters.AddWithValue("@FECHANAC", fechanac);

                try
                {
                    connection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return resultado;
        }

        public static int EliminarUsuarioPorRut(string rut)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from USUARIO where RUT = @RUT";
                sqlCommand.Parameters.AddWithValue("@RUT", rut);

                try
                {
                    connection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return resultado;
            }
        }

        public static int ActualizarUsuarioPorRut(Usuario usuario)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update USUARIO SET NOMBRE = @NOMBRE, APELLIDO = @APELLIDO, FECHANAC = @FECHANAC where RUT = @RUT";

                sqlCommand.Parameters.AddWithValue("@NOMBRE", usuario.NOMBRE);
                sqlCommand.Parameters.AddWithValue("@APELLIDO", usuario.APELLIDO);
                sqlCommand.Parameters.AddWithValue("@FECHANAC", usuario.FECHANAC);
                sqlCommand.Parameters.AddWithValue("@RUT", usuario.RUT);


                try
                {
                    sqlConnection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return resultado;
        }

        private static SqlCommand ConsultaSqlUsuario(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }
        private static Usuario CreacionUsuario(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Usuario usuario = new Usuario();
                usuario.RUT = dataTable.Rows[0]["RUT"].ToString();
                usuario.NOMBRE = dataTable.Rows[0]["NOMBRE"].ToString();
                usuario.APELLIDO = dataTable.Rows[0]["APELLIDO"].ToString();
                usuario.FECHANAC = DateTime.Parse(dataTable.Rows[0]["FECHANAC"].ToString());
                return usuario;
            }
            else
            {
                return null;
            }
        }
        private static DataTable LlenarDataTable(SqlCommand comando)
        {
            //2. llenamos el dataTable(conversion)
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        private static List<Usuario> LLenadoUsuarios(DataTable dataTable)
        {
            usuarios = new List<Usuario>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Usuario usuario = new Usuario();
                usuario.RUT = dataTable.Rows[i]["RUT"].ToString();
                usuario.NOMBRE = dataTable.Rows[i]["NOMBRE"].ToString();
                usuario.APELLIDO = dataTable.Rows[i]["APELLIDO"].ToString();
                usuario.FECHANAC = DateTime.Parse(dataTable.Rows[0]["FECHANAC"].ToString());
                usuarios.Add(usuario);
            }
            return usuarios;
        }
    }
}
