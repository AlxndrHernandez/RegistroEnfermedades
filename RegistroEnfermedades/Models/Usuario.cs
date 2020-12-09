using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroEnfermedades.Models
{
    public class Usuario
    {

        public string RUT { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public DateTime FECHANAC { get; set; }

    }
}
