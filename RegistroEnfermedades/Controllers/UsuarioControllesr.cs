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
    public class UsuarioController : ControllerBase
    {
        //GET /api/Usuarios/all
        [HttpGet("all")]
        public JsonResult ObtenerUsuarios()
        {
            //json
            var usuariosRetornados = UsuarioAzure.ObtenerUsuarios();
            return new JsonResult(usuariosRetornados);
        }

        ////GET /api/Rol/{id}-{nombre}
        //[HttpGet("{IDROL}")]
        //public JsonResult ObtenerUsuario(String RUT)
        //{
        //    var conversionExitosa = int.TryParse(RUT, out int idConvertido);

        //    Rol usuariolRetornada;

        //    if (conversionExitosa)
        //    {
        //        usuariolRetornada = UsuarioAzure.ObtenerUsuarioPorRut(idConvertido);
        //    }
        //    else
        //    {
        //        usuariolRetornada = RolAzure.ObtenerRolPorNombre(RUT);
        //    }

        //    if (usuariolRetornada is null)
        //    {
        //        return new JsonResult($"Intente nuevamente con un parametro distinto a {RUT}");
        //    }
        //    else
        //    {
        //        return new JsonResult(usuariolRetornada);
        //    }


        //}

        //PROFE, LA PARTE DE OBTENER AL USUARIO POR RUT NO NOS FUNCIONO, AL SER STRING INTENTAMOS HACER LA CONVERSION PERO NO FUNCIONO


        //POST: api/Usuario
        [HttpPost]
        public void AgregarUsuario([FromBody] Usuario usuario)
        {
            UsuarioAzure.AgregarUsuario(usuario);
        }


        //DELETE: api/Usuario/rut
        [HttpDelete("{RUT}")]
        public void EliminarUsuarioPorRut(string rut)
        {
            UsuarioAzure.EliminarUsuarioPorRut(rut);
        }

        //PUT: api/Usuario/
        [HttpPut]
        public void ActualizarUsuarioPorRut([FromBody] Usuario usuario)
        {
            UsuarioAzure.ActualizarUsuarioPorRut(usuario);
        }
    }
}
