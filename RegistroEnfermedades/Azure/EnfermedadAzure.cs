
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroEnfermedades.Models;
using System.Data;
using System.Data.SqlClient;


namespace RegistroEnfermedades.Azure
{
    public class EnfermedadAzure
    {
        static string connectionString = @"Server=DESKTOP-C1E27D4\SQLEXPRESS01;Database=REGISTROENFERMEDADES2;Trusted_Connection=True;";


        private static List<Enfermedad> enfermedades;

        public static List<Enfermedad> ObtenerEnfermedades()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from ENFERMEDAD";

                var comando = ConsultaSqlEnfermedad(connection, consultaSql);

                var dataTableEnfermedades = LlenarDataTable(comando);

                return LLenadoEnfermedades(dataTableEnfermedades);
            }
        }

        public static Enfermedad ObtenerEnfermedadPorId(int idEnfermedad)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from ENFERMEDAD where IDENFERMEDAD = {idEnfermedad}";

                var comando = ConsultaSqlEnfermedad(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionEnfermedad(dataTable);
            }
        }

        public static Enfermedad ObtenerEnfermedadPorDescripcion(string descripcion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from ENFERMEDAD where DESCRIPCION = '{descripcion}'";

                var comando = ConsultaSqlEnfermedad(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionEnfermedad(dataTable);

            }
        }

        public static int AgregarEnfermedad(Enfermedad enfermedad)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into ENFERMEDAD (IDENFERMEDAD,DESCRIPCION) values (@IDENFERMEDAD,@DESCRIPCION)";
                sqlCommand.Parameters.AddWithValue("@IDENFERMEDAD", enfermedad.IDENFERMEDAD);
                sqlCommand.Parameters.AddWithValue("@DESCRIPCION", enfermedad.DESCRIPCION);


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

        public static int AgregarEnfermedadParametros(int idenfermedad, string  descripcion)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into ENFERMEDAD (IDENFERMEDAD, DESCRIPCION) values (@IDENFERMEDAD,@DESCRIPCION)";
                sqlCommand.Parameters.AddWithValue("@IDENFERMEDAD", idenfermedad);
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

        public static int EliminarEnfermedadPorId(int idenfermedad)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from ENFERMEDAD where IDENFERMEDAD = @IDENFERMEDAD";
                sqlCommand.Parameters.AddWithValue("@IDENFERMEDAD", idenfermedad);

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

        public static int ActualizarEnfermedadPorId(Enfermedad enfermedad)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update ENFERMEDAD SET DESCRIPCION = @DESCRIPCION where IDENFERMEDAD = @IDENFERMEDAD";

                sqlCommand.Parameters.AddWithValue("@DESCRIPCION", enfermedad.DESCRIPCION);
                sqlCommand.Parameters.AddWithValue("@IDENFERMEDAD", enfermedad.IDENFERMEDAD);

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

        private static SqlCommand ConsultaSqlEnfermedad(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }
        private static Enfermedad CreacionEnfermedad(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Enfermedad enfermedad = new Enfermedad();
                enfermedad.IDENFERMEDAD = int.Parse(dataTable.Rows[0]["IDENFERMEDAD"].ToString());
                enfermedad.DESCRIPCION = dataTable.Rows[0]["DESCRIPCION"].ToString();
                return enfermedad;
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
        private static List<Enfermedad> LLenadoEnfermedades(DataTable dataTable)
        {
            enfermedades = new List<Enfermedad>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Enfermedad enfermedad = new Enfermedad();
                enfermedad.IDENFERMEDAD = int.Parse(dataTable.Rows[i]["IDENFERMEDAD"].ToString());
                enfermedad.DESCRIPCION = dataTable.Rows[i]["DESCRIPCION"].ToString();
                enfermedades.Add(enfermedad);
            }
            return enfermedades;
        }
    }
}
