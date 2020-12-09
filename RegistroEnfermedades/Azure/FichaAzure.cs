using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroEnfermedades.Models;
using System.Data;
using System.Data.SqlClient;


namespace RegistroEnfermedades.Azure
{
    public class FichaAzure
    {
        static string connectionString = @"Server=DESKTOP-C1E27D4\SQLEXPRESS01;Database=REGISTROENFERMEDADES2;Trusted_Connection=True;";


        private static List<Ficha> fichas;

        public static List<Ficha> ObtenerFichas()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from FICHA";

                var comando = ConsultaSqlFicha(connection, consultaSql);

                var dataTableFichas = LlenarDataTable(comando);

                return LLenadoFichas(dataTableFichas);
            }
        }

        public static Ficha ObtenerFichaPorNroFicha(int nroFicha)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from FICHA where NROFICHA = {nroFicha}";

                var comando = ConsultaSqlFicha(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionFicha(dataTable);
            }
        }

        public static Ficha ObtenerFichaPorNroRut(string rutusuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from FICHA where RUTUSUARIO = '{rutusuario}'";

                var comando = ConsultaSqlFicha(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionFicha(dataTable);

            }
        }
        
        public static int AgregarFicha(Ficha ficha)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into FICHA (NROFICHA,FECHAATENCION,RUTUSUARIO) values (@NROFICHA,@FECHAATENCION,@RUTUSUARIO)";
                sqlCommand.Parameters.AddWithValue("@NROFICHA", ficha.NROFICHA);
                sqlCommand.Parameters.AddWithValue("@FECHAATENCION", ficha.FECHAATENCION);
                sqlCommand.Parameters.AddWithValue("@RUTUSUARIO", ficha.RUTUSUARIO);


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

        public static int AgregarFichaParametros(int nroficha, DateTime fechaatencion, string rutusuario)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into FICHA (NROFICHA,FECHAATENCION,RUTUSUARIO) values (@NROFICHA,@FECHAATENCION,@RUTUSUARIO)";
                sqlCommand.Parameters.AddWithValue("@NROFICHA", nroficha);
                sqlCommand.Parameters.AddWithValue("@FECHAATENCION", fechaatencion);
                sqlCommand.Parameters.AddWithValue("@RUTUSUARIO", rutusuario);

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

        public static int EliminarFichaPorNroFicha(int nroficha)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from FICHA where NROFICHA = @NROFICHA";
                sqlCommand.Parameters.AddWithValue("@NROFICHA", nroficha);

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

        public static int ActualizarFichaPorNroFicha(Ficha ficha)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update FICHA SET FECHAATENCION = @FECHAATENCION where NROFICHA = @NROFICHA";
                
                sqlCommand.Parameters.AddWithValue("@FECHAATENCION", ficha.FECHAATENCION);
                sqlCommand.Parameters.AddWithValue("@RUTUSUARIO", ficha.RUTUSUARIO);
                sqlCommand.Parameters.AddWithValue("@NROFICHA", ficha.NROFICHA);

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

        private static SqlCommand ConsultaSqlFicha(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }
        private static Ficha CreacionFicha(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Ficha ficha = new Ficha();
                ficha.NROFICHA = int.Parse(dataTable.Rows[0]["NROFICHA"].ToString());
                ficha.FECHAATENCION = DateTime.Parse(dataTable.Rows[0]["FECHAATENCION"].ToString());
                ficha.RUTUSUARIO = dataTable.Rows[0]["RUTUSUARIO"].ToString();
                
                return ficha;
               
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
        private static List<Ficha> LLenadoFichas(DataTable dataTable)
        {
            fichas = new List<Ficha>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Ficha ficha = new Ficha();
                ficha.NROFICHA = int.Parse(dataTable.Rows[i]["NROFICHA"].ToString());
                ficha.FECHAATENCION = DateTime.Parse(dataTable.Rows[i]["FECHAATENCION"].ToString());
                ficha.RUTUSUARIO = dataTable.Rows[i]["RUTUSUARIO"].ToString();
                fichas.Add(ficha);
            }
            return fichas;
        }
    }
}
