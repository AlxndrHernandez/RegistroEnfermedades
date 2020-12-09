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
    //localhost:8080/api/Rol
    //192.168.1.20:8080/api/Rol
    //mipaginaweb.com/api/Rol
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        //GET /api/Rol/all
        [HttpGet("all")]
        public JsonResult ObtenerRoles()
        {
            //json
            var rolesRetornadas = RolAzure.ObtenerRoles();
            return new JsonResult(rolesRetornadas);
        }

        //GET /api/Rol/{id}-{nombre}
        [HttpGet("{IDROL}")]
        public JsonResult ObtenerRol(String IDROL)
        {
            var conversionExitosa = int.TryParse(IDROL, out int idConvertido);

            Rol rolRetornada;

            if (conversionExitosa)
            {
                rolRetornada = RolAzure.ObtenerRolPorId(idConvertido);
            }
            else
            {
                rolRetornada = RolAzure.ObtenerRolPorDescripcion(IDROL);
            }

            if (rolRetornada is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {IDROL}");
            }
            else
            {
                return new JsonResult(rolRetornada);
            }


        }

        //POST: api/Rol
        [HttpPost]
        public void AgregarRol([FromBody] Rol rol)
        {
            RolAzure.AgregarRol(rol);
        }


        //DELETE: api/Rol/descripcion
        [HttpDelete("{DESCRIPCION}")]
        public void EliminarRolPorDescripcion(string descripcion)
        {
            RolAzure.EliminarRolPorDescripcion(descripcion);
        }

        //PUT: api/Rol/
        [HttpPut]
        public void ActualizarRolPorId([FromBody] Rol rol)
        {
            RolAzure.ActualizarRolPorId(rol);
        }
    }
}
