using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistroEnfermedades.Models;
using RegistroEnfermedades.Azure;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistroEnfermedades.Controllers
{
    //localhost:8080/api/Familia
    //192.168.1.20:8080/api/Familia
    //mipaginaweb.com/api/Familia
    [Route("api/[controller]")]
    [ApiController]
    public class FichaController : ControllerBase
    {
        //APARTADO TRANSACCIONES

        //GET /api/Ficha/all
        [HttpGet("all")]
        public JsonResult ObtenerFichas()
        {
            //json
            var fichasRetornadas = FichaAzure.ObtenerFichas();
            return new JsonResult(fichasRetornadas);
        }

        //GET /api/Ficha/{id}-{nombre}
        [HttpGet("{NROFICHA}")]
        public JsonResult ObtenerFicha(String NROFICHA)
        {
            var conversionExitosa = int.TryParse(NROFICHA, out int idConvertido);

            Ficha fichaRetornada;

            if (conversionExitosa)
            {
                fichaRetornada = FichaAzure.ObtenerFichaPorNroFicha(idConvertido);
            }
            else
            {
                fichaRetornada = FichaAzure.ObtenerFichaPorNroRut(NROFICHA);
            }

            if (fichaRetornada is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {NROFICHA}");
            }
            else
            {
                return new JsonResult(fichaRetornada);
            }


        }



        //APARTADO CRUD
        //POST: api/Ficha
        [HttpPost]
        public void AgregarFicha([FromBody] Ficha ficha)
        {
            FichaAzure.AgregarFicha(ficha);
        }


        //DELETE: api/Ficha/nroficha
        [HttpDelete("{NROFICHA}")]
        public void EliminarFichaPorNroFicha(int nroficha)
        {
            FichaAzure.EliminarFichaPorNroFicha(nroficha);
        }

        //PUT: api/Ficha/
        [HttpPut]
        public void ActualizarFichaPorNroFicha([FromBody] Ficha ficha)
        {
            FichaAzure.ActualizarFichaPorNroFicha(ficha);
        }
    }
}
