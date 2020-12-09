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
    public class EnfermedadController : ControllerBase
    {   
        //APARTADO TRANSACCIONES

        //GET /api/Enfermedad/all
        [HttpGet("all")]
        public JsonResult ObtenerEnfermedades()
        {
            //json
            var enfermedadesRetornadas = EnfermedadAzure.ObtenerEnfermedades();
            return new JsonResult(enfermedadesRetornadas);
        }

        //GET /api/Enfermedad/{id}-{nombre}
        [HttpGet("{IDENFERMEDAD}")]
        public JsonResult ObtenerEnfermedad(String IDENFERMEDAD)
        {
            var conversionExitosa = int.TryParse(IDENFERMEDAD, out int idConvertido);

            Enfermedad enfermedadRetornada;

            if (conversionExitosa)
            {
                enfermedadRetornada = EnfermedadAzure.ObtenerEnfermedadPorId(idConvertido);
            }
            else
            {
                enfermedadRetornada = EnfermedadAzure.ObtenerEnfermedadPorDescripcion(IDENFERMEDAD);
            }

            if (enfermedadRetornada is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {IDENFERMEDAD}");
            }
            else
            {
                return new JsonResult(enfermedadRetornada);
            }


        }

       
            
         //APARTADO CRUD
         //POST: api/Enfermedad
            [HttpPost]
        public void AgregarEnfermedad([FromBody] Enfermedad enfermedad)
        {
            EnfermedadAzure.AgregarEnfermedad(enfermedad);
        }
        
        
        //DELETE: api/Enfermedad/id
        [HttpDelete("{IDENFERMEDAD}")]
        public void EliminarEnfermedadPorId(int idenfermedad)
        {
            EnfermedadAzure.EliminarEnfermedadPorId(idenfermedad);
        }
        
        //PUT: api/Enfermedad/
        [HttpPut]
        public void ActualizarEnfermedadPorId([FromBody] Enfermedad enfermedad)
        {
            EnfermedadAzure.ActualizarEnfermedadPorId(enfermedad);
        }
    }
}
