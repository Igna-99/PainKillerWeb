using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PainKillerWeb.Models
{
    public class ErrMsj
    {
        public const string RequeridoM = "El {0} es requerido";
        public const string RequeridoF = "La {0} es requerida";

        public const string largoMin = "El {0} debe tener mas de {1} caracteres";
        public const string largoMax = "El {0} debe tener menos de {1} caracteres";

        public const string RangoF = "La {0} debe ser un numero entre {1} y {2}";
        public const string RangoM = "El {0} debe ser un numero entre {1} y {2}";
    }
}
