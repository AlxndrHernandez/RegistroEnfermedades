using System;
using Xunit;
using System.Linq;
using RegistroEnfermedades.Azure;
using RegistroEnfermedades.Models;


namespace XUnitTestregistroEnfermedades
{
    public class UnitTestRolUsuario
    {
      
        [Fact]
        public void TestObtenerRolUsuario()
        {
            // Arrage 

            bool vieneConDatos = false;

            // Act
            var resultado = RoUsuarioAzure.ObtenerRolesUsuarios();
            vieneConDatos = resultado.Any();

            // Assert

            Assert.True(vieneConDatos);

        }

        [Fact]
        public void TestObtenerRolUsuarioPorId()
        {
            //Arrange
            int idProbar = 1;
            RolUsuario RolUsuarioRetornado;
            int resultadoEsperado = 1;
            //Act
            RolUsuarioRetornado = RoUsuarioAzure.ObtenerRolUsuarioPorId(idProbar);

            //Assert 
            Assert.Equal(resultadoEsperado, RolUsuarioRetornado.IDROLUSU);
        }



        [Fact]
        public void TestAgregarRolUsuarioPorParametro()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            int idRolUsu = 5;
            int IdRol = 1;
            string rutUsuario = "6666666666";

            //Act  
            resultadoObtenido = RoUsuarioAzure.AgregarRolUsuarioParametros(idRolUsu, IdRol, rutUsuario);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }



        [Fact]
        public void TestAgregarFichaEnf()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            RolUsuario rolusuario = new RolUsuario();

            rolusuario.IDROLUSU = 7;
            rolusuario.IDROL_RU = 2;
            rolusuario.RUTUSUARIO_RU = "4444444444";



            //Act
            resultadoObtenido = RoUsuarioAzure.AgregarRolUsuario(rolusuario);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }




        [Fact]
        public void TestActualizarRolUsuarioPorId()
        {
            //Arrange         
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            RolUsuario rolusuario = new RolUsuario();
            rolusuario.IDROLUSU = 1;
            rolusuario.IDROL_RU = 2;
            rolusuario.RUTUSUARIO_RU = "1111111111";


            //Act
            resultadoObtenido = RoUsuarioAzure.ActualizarRolUsuarioPorId(rolusuario);
            rolusuario.IDROLUSU = 1;
            rolusuario.IDROL_RU = 1;
            rolusuario.RUTUSUARIO_RU = "1111111111";

            RoUsuarioAzure.ActualizarRolUsuarioPorId(rolusuario);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }



        [Fact]
        public void TestEliminarRolUsuarioPorId()
        {
            //Arrange         
            RolUsuario fichaEnfermedad = new RolUsuario();


            //string 
            int idRolUsuario = 4;

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;


            //Act
            resultadoObtenido = RoUsuarioAzure.EliminarRolUsuarioPorIdRolUsu(idRolUsuario);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


    }
}
