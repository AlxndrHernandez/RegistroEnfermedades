using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RegistroEnfermedades.Models;

namespace RegistroEnfermedades.Azure
{
    public class FichaEnfermedadAzure
    {
       
            static string connectionString = @"Server=DESKTOP-C1E27D4\SQLEXPRESS01;Database=REGISTROENFERMEDADES2;Trusted_Connection=True;";


            private static List<FichaEnfermedad> fichasenfermedades;

            public static List<FichaEnfermedad> ObtenerFichasEnfermedades()
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var consultaSql = "select * from FICHAENFERMEDAD";

                    var comando = ConsultaSqlFichaEnfermedad(connection, consultaSql);

                    var dataTableFichasEnfermedades = LlenarDataTable(comando);

                    return LLenadoFichasEnfermedades(dataTableFichasEnfermedades);
                }
            }

            public static FichaEnfermedad ObtenerFichaEnfermedadPorId(int idFichaEnf)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var consultaSql = $"select * from FICHAENFERMEDAD where IDFICHAENF = {idFichaEnf}";

                    var comando = ConsultaSqlFichaEnfermedad(connection, consultaSql);

                    var dataTable = LlenarDataTable(comando);

                    return CreacionFichaEnfermedad(dataTable);
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

            public static int AgregarFichaEnfermedad(FichaEnfermedad fichaenfermedad)
            {
                int filasAfectadas = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(null, connection);
                    sqlCommand.CommandText = "Insert into FICHAENFERMEDAD (IDFICHAENF,NROFICHA_FE,IDENFERMEDAD_FE) values (@IDFICHAENF,@NROFICHA_FE,@IDENFERMEDAD_FE)";
                    sqlCommand.Parameters.AddWithValue("@IDFICHAENF", fichaenfermedad.IDFICHAENF);
                    sqlCommand.Parameters.AddWithValue("@NROFICHA_FE", fichaenfermedad.NROFICHA_FE);
                    sqlCommand.Parameters.AddWithValue("@IDENFERMEDAD_FE", fichaenfermedad.IDENFERMEDAD_FE);


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

            public static int AgregarFichaEnfermedadParametros(int idfichaenf, int nroficha_fe, int idenfermedad_fe)
            {
                int resultado = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(null, connection);
                    sqlCommand.CommandText = "Insert into FICHAENFERMEDAD (IDFICHAENF,NROFICHA_FE,IDENFERMEDAD_FE) values (@IDFICHAENF,@NROFICHA_FE,@IDENFERMEDAD_FE)";
                    sqlCommand.Parameters.AddWithValue("@IDFICHAENF", idfichaenf);
                    sqlCommand.Parameters.AddWithValue("@NROFICHA_FE", nroficha_fe);
                    sqlCommand.Parameters.AddWithValue("@IDENFERMEDAD_FE", idenfermedad_fe);

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

            public static int EliminarFichaEnfermedadPorIdFichaEnf(int idfichaenf)
            {
                int resultado = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(null, connection);
                    sqlCommand.CommandText = "Delete from FICHAENFERMEDAD where IDFICHAENF = @IDFICHAENF";
                    sqlCommand.Parameters.AddWithValue("@IDFICHAENF", idfichaenf);

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

            public static int ActualizarFichaEnfermedadPorId(FichaEnfermedad fichaenfermedad)
            {
                int resultado = 0;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                    sqlCommand.CommandText = "Update FICHAENFERMEDAD SET NROFICHA_FE = @NROFICHA_FE, IDENFERMEDAD_FE = @IDENFERMEDAD_FE where IDFICHAENF = @IDFICHAENF";

                    sqlCommand.Parameters.AddWithValue("@NROFICHA_FE", fichaenfermedad.NROFICHA_FE);
                    sqlCommand.Parameters.AddWithValue("@IDENFERMEDAD_FE", fichaenfermedad.IDENFERMEDAD_FE);
                    sqlCommand.Parameters.AddWithValue("@IDFICHAENF", fichaenfermedad.IDFICHAENF);

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

            private static SqlCommand ConsultaSqlFichaEnfermedad(SqlConnection connection, string consulta)
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = consulta;
                connection.Open();
                return sqlCommand;
            }
            private static FichaEnfermedad CreacionFichaEnfermedad(DataTable dataTable)
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                FichaEnfermedad fichaenfermedad = new FichaEnfermedad();
                fichaenfermedad.IDFICHAENF = int.Parse(dataTable.Rows[0]["IDFICHAENF"].ToString());
                fichaenfermedad.NROFICHA_FE = int.Parse(dataTable.Rows[0]["NROFICHA_FE"].ToString());
                fichaenfermedad.IDENFERMEDAD_FE = int.Parse(dataTable.Rows[0]["IDENFERMEDAD_FE"].ToString());
                return fichaenfermedad;
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
            private static List<FichaEnfermedad> LLenadoFichasEnfermedades(DataTable dataTable)
            {
            fichasenfermedades = new List<FichaEnfermedad>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                FichaEnfermedad fichaenfermedad = new FichaEnfermedad();
                    fichaenfermedad.IDFICHAENF = int.Parse(dataTable.Rows[0]["IDFICHAENF"].ToString());
                    fichaenfermedad.NROFICHA_FE = int.Parse(dataTable.Rows[0]["NROFICHA_FE"].ToString());
                    fichaenfermedad.IDENFERMEDAD_FE = int.Parse(dataTable.Rows[0]["IDENFERMEDAD_FE"].ToString());
                fichasenfermedades.Add(fichaenfermedad);
                }
                return fichasenfermedades;
            }
        
    }
}
