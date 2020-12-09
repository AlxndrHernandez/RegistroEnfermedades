using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroEnfermedades.Models
{
    public class Ficha
    {
        public int NROFICHA{ get; set; }
        public DateTime FECHAATENCION { get; set; }

        public string RUTUSUARIO { get; set; }

        public Usuario USUARIO  {get; set; }



}
}
