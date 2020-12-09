using System;
using Xunit;
using System.Linq;
using RegistroEnfermedades.Azure;
using RegistroEnfermedades.Models;

namespace XUnitTestRegistroEnfermedades
{
    public class UnitTestUsuario
    {
        
        [Fact]
        public void TestObtenerUsuario()
        {
            // Arrage 

            bool vieneConDatos = false;

            // Act
            var resultado = UsuarioAzure.ObtenerUsuarios();
            vieneConDatos = resultado.Any();

            // Assert

            Assert.True(vieneConDatos);

        }

        
        [Fact]
        public void TestObtenerUsuarioPorRut()
        {
            //Arrange
            string rutProbar = "1111111111";
            Usuario UsuarioRetornada;
            string resultadoEsperado = "1111111111";
            //Act
            UsuarioRetornada = UsuarioAzure.ObtenerUsuarioPorRut(rutProbar);

            //Assert 
            Assert.Equal(resultadoEsperado, UsuarioRetornada.RUT);
        }

     

        [Fact]
        public void TestAgregarUsuarioPorParametro()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            string RutUsuario = "7777777777";
            string Nombre = "DANIEL";
            string Apellido = "DIAZ";
            DateTime FechaNacimiento = Convert.ToDateTime("1995-05-03");


            //Act  
            resultadoObtenido = UsuarioAzure.AgregarUsuarioParametros(RutUsuario, Nombre, Apellido, FechaNacimiento);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


        [Fact]
        public void TestAgregarUsuario()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            Usuario usuario = new Usuario();

            usuario.RUT = "8888888888";
            usuario.NOMBRE = "FRANCO";
            usuario.APELLIDO = "ROJAS";
            usuario.FECHANAC = Convert.ToDateTime("1996-05-06");


            //Act
            resultadoObtenido = UsuarioAzure.AgregarUsuario(usuario);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


        [Fact]
        public void TestActualizarUsuarioPorRut()
        {
            //Arrange         
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            Usuario usuario = new Usuario();
            
            usuario.RUT = "1111111111";
            usuario.NOMBRE = "ALEXANDER";
            usuario.APELLIDO = "MUÑOZ";
            usuario.FECHANAC = Convert.ToDateTime("1991-03-27");



            //Act
            resultadoObtenido = UsuarioAzure.ActualizarUsuarioPorRut(usuario);
            usuario.RUT = "1111111111";
            usuario.NOMBRE = "ALEXANDER";
            usuario.APELLIDO = "HERNANDEZ";
            usuario.FECHANAC = Convert.ToDateTime("1991-03-27");



            UsuarioAzure.ActualizarUsuarioPorRut(usuario);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }


        
        [Fact]
        public void TestEliminarUsuarioPorRut()
        {
            //Arrange         
            Usuario usuario = new Usuario();


            //string 
            string RutUsuarioEliminar = "6666666666";

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;


            //Act
            resultadoObtenido = UsuarioAzure.EliminarUsuarioPorRut(RutUsuarioEliminar);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


    }
}
