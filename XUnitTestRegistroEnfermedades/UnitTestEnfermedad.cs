using System;
using Xunit;
using System.Linq;
using RegistroEnfermedades.Azure;
using RegistroEnfermedades.Models;

namespace XUnitTestRegistroEnfermedades
{
    public class UnitTestEnfermedad
    {

        [Fact]
        public void TestObtenerEnfermedad()
        {
            // Arrage 

            bool vieneConDatos = false;

            // Act
            var resultado = EnfermedadAzure.ObtenerEnfermedades();
            vieneConDatos = resultado.Any();

            // Assert

            Assert.True(vieneConDatos);

        }


        [Fact]
        public void TestObtenerEnfermedadPorId()
        {
            //Arrange
            int idProbar = 1;
            Enfermedad EnfermedadRetornada;
            int resultadoEsperado = 1;
            //Act
            EnfermedadRetornada = EnfermedadAzure.ObtenerEnfermedadPorId(idProbar);

            //Assert 
            Assert.Equal(resultadoEsperado, EnfermedadRetornada.IDENFERMEDAD);
        }



        [Fact]
        public void TestObtenerEnfermedadPorDescripcion()
        {
            //Arrange
            string NombreEnfermedad = "DIABETES";
            Enfermedad EnfermedadRetornada;
            string resultadoEsperado = "DIABETES";
            //Act
            EnfermedadRetornada = EnfermedadAzure.ObtenerEnfermedadPorDescripcion(NombreEnfermedad);

            //Assert 
            Assert.Equal(resultadoEsperado, EnfermedadRetornada.DESCRIPCION);



        }




        [Fact]
        public void TestAgregarEnfermedadPorParametro()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            string Descripcion = "SIFILIS";
            int idEnfermedad = 6;


            //Act
            resultadoObtenido = EnfermedadAzure.AgregarEnfermedadParametros(idEnfermedad, Descripcion);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }



        [Fact]
        public void TestAgregarEnfermedad()
        {
            //Arrange
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;
            Enfermedad enfermedad = new Enfermedad();
            enfermedad.IDENFERMEDAD = 7;
            enfermedad.DESCRIPCION = "GONORREA";



            //Act
            resultadoObtenido = EnfermedadAzure.AgregarEnfermedad(enfermedad);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

       

        [Fact]
        public void TestActualizarEnfermedadPorId()
        {
            //Arrange         
            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            Enfermedad enfermedad = new Enfermedad();
            enfermedad.IDENFERMEDAD = 1;
            enfermedad.DESCRIPCION = "ESCOLIOSIS";


            //Act
            resultadoObtenido = EnfermedadAzure.ActualizarEnfermedadPorId(enfermedad);
            
            enfermedad.IDENFERMEDAD = 1;
            enfermedad.DESCRIPCION = "FARINIGITIS";
            EnfermedadAzure.ActualizarEnfermedadPorId(enfermedad);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }




        [Fact]
        public void TestEliminareEnfermedadPorId()
        {
            //Arrange         
            Enfermedad enfermedad = new Enfermedad();


            //string 
            int DescripcionEnfermedadEliminar = 5;

            int resultadoEsperado = 1;
            int resultadoObtenido = 0;

            EnfermedadAzure.EliminarEnfermedadPorId(DescripcionEnfermedadEliminar);

            //Act
            resultadoObtenido = EnfermedadAzure.EliminarEnfermedadPorId(DescripcionEnfermedadEliminar);

            //Assert 
            Assert.Equal(resultadoEsperado, resultadoObtenido);

        }

    }
}

