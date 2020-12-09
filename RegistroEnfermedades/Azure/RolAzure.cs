using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroEnfermedades.Models;
using System.Data;
using System.Data.SqlClient;


namespace RegistroEnfermedades.Azure
{
    public class RolAzure
    {
        static string connectionString = @"Server=DESKTOP-C1E27D4\SQLEXPRESS01;Database=REGISTROENFERMEDADES2;Trusted_Connection=True;";


        private static List<Rol> roles;

        public static List<Rol> ObtenerRoles()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from ROL";

                var comando = ConsultaSqlRol(connection, consultaSql);

                var dataTableRoles = LlenarDataTable(comando);

                return LLenadoRoles(dataTableRoles);
            }
        }

        public static Rol ObtenerRolPorId(int idRol)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from ROL where IDROL = {idRol}";

                var comando = ConsultaSqlRol(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionRol(dataTable);
            }
        }

        public static Rol ObtenerRolPorDescripcion(string descripcion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from ROL where DESCRIPCION = '{descripcion}'";

                var comando = ConsultaSqlRol(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionRol(dataTable);

            }
        }

        public static int AgregarRol(Rol rol)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into ROL (IDROL,DESCRIPCION) values (@IDROL,@DESCRIPCION)";
                sqlCommand.Parameters.AddWithValue("@IDROL", rol.IDROL);
                sqlCommand.Parameters.AddWithValue("@DESCRIPCION", rol.DESCRIPCION);


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

        public static int AgregarRolParametros(int idrol, string descripcion)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into ROL (IDROL, DESCRIPCION) values (@IDROL, @DESCRIPCION)";
                sqlCommand.Parameters.AddWithValue("@IDROL", idrol);
                sqlCommand.Parameters.AddWithValue("@DESCRIPCION", descripcion);

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

        public static int EliminarRolPorDescripcion(string descripcion)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from ROL where DESCRIPCION = @DESCRIPCION";
                sqlCommand.Parameters.AddWithValue("@DESCRIPCION", descripcion);

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

        public static int ActualizarRolPorId(Rol rol)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update ROL SET DESCRIPCION = @DESCRIPCION where IDROL = @IDROL";

                sqlCommand.Parameters.AddWithValue("@DESCRIPCION", rol.DESCRIPCION);
                sqlCommand.Parameters.AddWithValue("@IDROL", rol.IDROL);

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

        private static SqlCommand ConsultaSqlRol(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }
        private static Rol CreacionRol(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Rol rol = new Rol();
                rol.IDROL = int.Parse(dataTable.Rows[0]["IDROL"].ToString());
                rol.DESCRIPCION = dataTable.Rows[0]["DESCRIPCION"].ToString();
                return rol;
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
        private static List<Rol> LLenadoRoles(DataTable dataTable)
        {
            roles = new List<Rol>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Rol rol = new Rol();
                rol.IDROL = int.Parse(dataTable.Rows[i]["IDROL"].ToString());
                rol.DESCRIPCION = dataTable.Rows[i]["DESCRIPCION"].ToString();
                roles.Add(rol);
            }
            return roles;
        }
    }
}
