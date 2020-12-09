using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroEnfermedades.Models;
using System.Data;
using System.Data.SqlClient;

namespace RegistroEnfermedades.Azure
{
    public class RoUsuarioAzure
    {
        static string connectionString = @"Server=DESKTOP-C1E27D4\SQLEXPRESS01;Database=REGISTROENFERMEDADES2;Trusted_Connection=True;";


        private static List<RolUsuario> rolesusuarios;

        public static List<RolUsuario> ObtenerRolesUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from ROLUSUARIO";

                var comando = ConsultaSqlRolUsuario(connection, consultaSql);

                var dataTableRolesUsuarios = LlenarDataTable(comando);

                return LLenadoRolesUsuarios(dataTableRolesUsuarios);
            }
        }

        public static RolUsuario ObtenerRolUsuarioPorId(int idRolUsu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from ROLUSUARIO where IDROLUSU = {idRolUsu}";

                var comando = ConsultaSqlRolUsuario(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionRolUsuario(dataTable);
            }
        }

        /*  public static RolUsuario ObtenerRolUsuarioPorDescripcion(string descripcion) 
          {
              using (SqlConnection connection = new SqlConnection(connectionString))
              {
                  var consultaSql = $"select * from ROL where DESCRIPCION = '{descripcion}'";

                  var comando = ConsultaSqlRol(connection, consultaSql);

                  var dataTable = LlenarDataTable(comando);

                  return CreacionRol(dataTable);

              }
          }*/

        public static int AgregarRolUsuario(RolUsuario rolusuario)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into ROLUSUARIO (IDROLUSU,IDROL_RU,RUTUSUARIO_RU) values (@IDROLUSU,@IDROL_RU,@RUTUSUARIO_RU)";
                sqlCommand.Parameters.AddWithValue("@IDROLUSU", rolusuario.IDROLUSU);
                sqlCommand.Parameters.AddWithValue("@IDROL_RU", rolusuario.IDROL_RU);
                sqlCommand.Parameters.AddWithValue("@RUTUSUARIO_RU", rolusuario.RUTUSUARIO_RU);


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

        public static int AgregarRolUsuarioParametros(int idrolusu, int idrol_ru, string rutusuario_ru)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into ROLUSUARIO (IDROLUSU,IDROL_RU,RUTUSUARIO_RU) values (@IDROLUSU,@IDROL_RU,@RUTUSUARIO_RU)";
                sqlCommand.Parameters.AddWithValue("@IDROLUSU", idrolusu);
                sqlCommand.Parameters.AddWithValue("@IDROL_RU", idrol_ru);
                sqlCommand.Parameters.AddWithValue("@RUTUSUARIO_RU", rutusuario_ru);

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

        public static int EliminarRolUsuarioPorIdRolUsu(int idrolusu)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from ROLUSUARIO where IDROLUSU = @IDROLUSU";
                sqlCommand.Parameters.AddWithValue("@IDROLUSU", idrolusu);

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

        public static int ActualizarRolUsuarioPorId(RolUsuario rolusuario)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update ROLUSUARIO SET IDROL_RUL = @IDROL_RU, RUTUSUARIO_RU = @RUTUSUARIO_RU where IDROLUSU = @IDROLUSU";

                sqlCommand.Parameters.AddWithValue("@IDROL_RU", rolusuario.IDROL_RU);
                sqlCommand.Parameters.AddWithValue("@RUTUSUARIO_RU", rolusuario.RUTUSUARIO_RU);
                sqlCommand.Parameters.AddWithValue("@IDROLUSU", rolusuario.IDROLUSU);

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

        private static SqlCommand ConsultaSqlRolUsuario(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }
        private static RolUsuario CreacionRolUsuario(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                RolUsuario rolusuario = new RolUsuario();
                rolusuario.IDROLUSU = int.Parse(dataTable.Rows[0]["IDROLUSU"].ToString());
                rolusuario.IDROL_RU = int.Parse(dataTable.Rows[0]["IDROL_RU"].ToString());
                rolusuario.RUTUSUARIO_RU = dataTable.Rows[0]["RUTUSUARIO_RU"].ToString();
                return rolusuario;
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
        private static List<RolUsuario> LLenadoRolesUsuarios(DataTable dataTable)
        {
            rolesusuarios = new List<RolUsuario>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                RolUsuario rolusuario = new RolUsuario();
                rolusuario.IDROLUSU = int.Parse(dataTable.Rows[0]["IDROLUSU"].ToString());
                rolusuario.IDROL_RU = int.Parse(dataTable.Rows[0]["IDROL_RU"].ToString());
                rolusuario.RUTUSUARIO_RU = dataTable.Rows[0]["RUTUSUARIO_RU"].ToString();
                rolesusuarios.Add(rolusuario);
            }
            return rolesusuarios;
        }
    }
}