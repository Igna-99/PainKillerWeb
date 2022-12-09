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
        [Required]
        public int costeExp { get; set; }

        [Display(Name = "Coste Uso")]
        [Required]
        public int costeUso { get; set; }

        //Si es 1 = vida | 2 = mana | 3 = energia
        [Display(Name = "Tipo coste")]
        [Required]
        public int tipoCoste { get; set; }

        [Display(Name = "Efecto")]
        [Required]
        public string efecto { get; set; }

        [Display(Name = "T. Lanzamiento")]
        [Required]
        public string tiempo { get; set; }

        [Display(Name = "Distancia")]
        [Required]
        public Distancia distancia { get; set; }
        [Required]
        public int distanciaId { get; set; }

        [Display(Name = "Elemento")]
        [Required]
        public Elemento elemento { get; set; }
        [Required]
        public int elementoId { get; set; }
        public ICollection<Hechizo> cadena { get; set; }


    }
}
