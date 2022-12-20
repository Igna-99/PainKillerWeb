using Microsoft.CodeAnalysis.Options;
using PainKillerWeb.Models.Pivot;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PainKillerWeb.Models.Main
{
    public class Hechizo
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string nombre { get; set; }

        [Display(Name = "Duracion")]
        
        public string duracion { get; set; }

        [Display(Name = "Coste Experiencia")]
        public int costeExp { get; set; }

        [Display(Name = "Coste Uso")]
        public int costeUso { get; set; }

        //Si es 1 = vida | 2 = mana | 3 = energia |
        //| 4 = MANA Y VIDA | 5 = MANA Y ENERGIA | 6 = VIDA Y ENERGIA
        
        [Display(Name = "Tipo coste")]
        public int tipoCoste { get; set; }

        [Display(Name = "Efecto")]
        public string efecto { get; set; }

        [Display(Name = "T. Lanzamiento")]
        public string tiempo { get; set; }

        [Display(Name = "Distancia")]
        public Distancia distancia { get; set; }
        [Display(Name = "Distancia")]
        public int distanciaId { get; set; }

        [Display(Name = "Elemento")]
        public Elemento elemento { get; set; }
        [Display(Name = "Elemento")]
        public int elementoId { get; set; }
        public ICollection<Hechizo> cadena { get; set; }


    }
}
