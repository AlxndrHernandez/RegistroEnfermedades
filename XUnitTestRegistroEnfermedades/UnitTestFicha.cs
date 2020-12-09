using System;
using Xunit;
using System.Linq;
using RegistroEnfermedades.Azure;
using RegistroEnfermedades.Models;

namespace XUnitTestRegistroEnfermedades
{
    public class UnitTestFicha
    {
       
        [Fact]
        public void TestObtenerFichas()
        {
            // Arrage 

            bool vieneConDatos = false;

            // Act
            var resultado = FichaAzure.ObtenerFichas();
            vieneConDatos = resultado.Any();

            // Assert

            Assert.True(vieneConDatos);

        }

        [Fact]
        public void TestObtenerFichaPorNroFicha()
        {
            //Arrange
            int idProbar = 1;
            Ficha FichaRetornada;
            int resultadoEsperado = 1;
            //Act
            FichaRetornada = FichaAzure.ObtenerFichaPorNroFicha(idProbar);

            //Assert 
            Assert.Equal(resultadoEsperado, FichaRetornada.NROFICHA);
        }

     

        [Fact]
        public void TestAgregarFichaPorParametros()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            DateTime FechaAtencion = Convert.ToDateTime("2020-10-10");
            int nroficha = 5;
            string rutusuario = "1111111111";

            //Act
            resultadoObtenido = FichaAzure.AgregarFichaParametros(nroficha, FechaAtencion, rutusuario);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }


        [Fact]
        public void TestAgregarFicha()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            Ficha ficha = new Ficha();

            ficha.FECHAATENCION = Convert.ToDateTime("2020-10-10");
            ficha.NROFICHA = 5;
            ficha.RUTUSUARIO = "1111111111";

            //Act
            resultadoObtenido = FichaAzure.AgregarFicha(ficha);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }




        [Fact]
        public void TestActualizarFichaPorNroFicha()
        {
            //Arrange         
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            Ficha ficha = new Ficha();
            ficha.NROFICHA = 1;
            ficha.FECHAATENCION = Convert.ToDateTime("2020-10-10");
            ficha.RUTUSUARIO = "1111111111";
            

            //Act
            resultadoObtenido = FichaAzure.ActualizarFichaPorNroFicha(ficha);

            ficha.FECHAATENCION = Convert.ToDateTime("2018-08-18");
            ficha.NROFICHA = 1;
            ficha.RUTUSUARIO = "1111111111";

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }


   
        [Fact]
        public void TestEliminarFichaPorNroFicha()
        {
            //Arrange         
            Ficha ficha = new Ficha();


            //string 
            int NroFicha = 4;

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;


            //Act
            resultadoObtenido = FichaAzure.EliminarFichaPorNroFicha(NroFicha);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

    }
}
