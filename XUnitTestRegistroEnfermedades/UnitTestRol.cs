using System;
using Xunit;
using System.Linq;
using RegistroEnfermedades.Azure;
using RegistroEnfermedades.Models;

namespace XUnitTestRegistroEnfermedades
{
    public class UnitTestRol
    {
        
        [Fact]
        public void TestObtenerRol()
        {
            // Arrage 

            bool vieneConDatos = false;

            // Act
            var resultado = RolAzure.ObtenerRoles();
            vieneConDatos = resultado.Any();

            // Assert

            Assert.True(vieneConDatos);

        }


        [Fact]
        public void TestObtenerRolPorId()
        {
            //Arrange
            int idProbar = 1;
            Rol RolRetornada;
            int resultadoEsperado = 1;
            //Act
            RolRetornada = RolAzure.ObtenerRolPorId(idProbar);

            //Assert 
            Assert.Equal(resultadoEsperado, RolRetornada.IDROL);
        }


        
        [Fact]
        public void TestObtenerRolPorDescripcion()
        {
            //Arrange
            string NombreRol = "PACIENTE";
            Rol RolRetornada;
            string resultadoEsperado = "PACIENTE";
            //Act
            RolRetornada = RolAzure.ObtenerRolPorDescripcion(NombreRol);

            //Assert 
            Assert.Equal(resultadoEsperado, RolRetornada.DESCRIPCION);



        }


        

        [Fact]
        public void TestAgregarRolPorParametro()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            string NombreRol = "ADMINISTRADOR";
            int idrol = 3;


            //Act
            resultadoObtenido = RolAzure.AgregarRolParametros(idrol,NombreRol);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

        

        [Fact]
        public void TestAgregarRol()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            Rol rol = new Rol();
            rol.IDROL = 4;
            rol.DESCRIPCION = "SECRETARIA";
            


            //Act
            resultadoObtenido = RolAzure.AgregarRol(rol);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

        // Prueba a metodo Actualizar  Rol

        [Fact]
        public void TestActualizarRolPorId()
        {
            //Arrange         
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Rol rol = new Rol();
            rol.IDROL = 2;
            rol.DESCRIPCION = "CIRUJANO";


            //Act
            resultadoObtenido = RolAzure.ActualizarRolPorId(rol);

            rol.DESCRIPCION = "MEDICO";
            rol.IDROL = 2;
            RolAzure.ActualizarRolPorId(rol);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }



        // Prueba a metodo Eliminar  Rol
        [Fact]
        public void TestEliminarRolPorDescripcion()
        {
            //Arrange         
            Rol rol = new Rol();


            //string 
            string DescripcionRolEliminar = "PACIENTE";

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            RolAzure.EliminarRolPorDescripcion(DescripcionRolEliminar);

            //Act
            resultadoObtenido = RolAzure.EliminarRolPorDescripcion(DescripcionRolEliminar);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

    }
}
