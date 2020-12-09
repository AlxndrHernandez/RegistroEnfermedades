using System;
using Xunit;
using System.Linq;
using RegistroEnfermedades.Azure;
using RegistroEnfermedades.Models;


namespace XUnitTestregistroEnfermedades
{
    public class UnitTestFichaEnfermedad
    {
        // Prueba de metodo obtener Detalle a Pedido
        [Fact]
        public void TestObtenerFichaEnfermedad()
        {
            // Arrage 

            bool vieneConDatos = false;

            // Act
            var resultado = FichaEnfermedadAzure.ObtenerFichasEnfermedades();
            vieneConDatos = resultado.Any();

            // Assert

            Assert.True(vieneConDatos);

        }

        [Fact]
        public void TestObtenerFichaEnfermedadPorId()
        {
            //Arrange
            int idProbar = 1;
            FichaEnfermedad FichaERetornada;
            int resultadoEsperado = 1;
            //Act
            FichaERetornada = FichaEnfermedadAzure.ObtenerFichaEnfermedadPorId(idProbar);

            //Assert 
            Assert.Equal(resultadoEsperado, FichaERetornada.IDFICHAENF);
        }

        

        [Fact]
        public void TestAgregarFichaEnfermedadPorParametro()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            int idFichaEnf = 5;
            int nroFicha_fe = 1;
            int idEnfermedad_fe = 2;

            //Act  
            resultadoObtenido = FichaEnfermedadAzure.AgregarFichaEnfermedadParametros(idFichaEnf, nroFicha_fe, idEnfermedad_fe);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }



        [Fact]
        public void TestAgregarFichaEnf()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            FichaEnfermedad fichaenfermedad = new FichaEnfermedad();

            fichaenfermedad.IDFICHAENF = 6;
            fichaenfermedad.IDENFERMEDAD_FE = 3;
            fichaenfermedad.NROFICHA_FE = 2;



            //Act
            resultadoObtenido = FichaEnfermedadAzure.AgregarFichaEnfermedad(fichaenfermedad);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


 

        [Fact]
        public void TestActualizarFichaEnfermedadPorId()
        {
            //Arrange         
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            FichaEnfermedad fichaenfermedad = new FichaEnfermedad();
            fichaenfermedad.IDFICHAENF= 1;
            fichaenfermedad.IDENFERMEDAD_FE = 1;
            fichaenfermedad.NROFICHA_FE = 1;


            //Act
            resultadoObtenido = FichaEnfermedadAzure.ActualizarFichaEnfermedadPorId(fichaenfermedad);
            fichaenfermedad.IDFICHAENF = 1;
            fichaenfermedad.IDENFERMEDAD_FE = 4;
            fichaenfermedad.NROFICHA_FE = 1;

            FichaEnfermedadAzure.ActualizarFichaEnfermedadPorId(fichaenfermedad);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }


 
        [Fact]
        public void TestEliminarFichaEnfermedadPorId()
        {
            //Arrange         
            FichaEnfermedad fichaEnfermedad = new FichaEnfermedad();


            //string 
            int idFichaEnfermedad = 4;

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;


            //Act
            resultadoObtenido = FichaEnfermedadAzure.EliminarFichaEnfermedadPorIdFichaEnf(idFichaEnfermedad);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


    }
}

