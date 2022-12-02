using PainKillerWeb.Models.Pivot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PainKillerWeb.Models.Main
{
    public class Personaje
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = ErrMsj.RequeridoM)]
        [Display(Name = "Nombre")]
        [MinLength(3, ErrorMessage = ErrMsj.largoMin)]
        [MaxLength(21, ErrorMessage = ErrMsj.largoMax)]
        public string nombre { get; set; }

        [Display(Name ="raza")]
        public int razaId { get; set; }
        [Display(Name = "Raza")]
        public Raza raza { get; set; }

        [Required(ErrorMessage = ErrMsj.RequeridoF)]
        [Display(Name = "Experiencia")]
        [Range(1, 1000, ErrorMessage = ErrMsj.RangoF)]
        public int expActual { get; set; }
        [Display(Name = "Experiencia Gastada")]
        public int expGastada { get; set; }
        [Display(Name = "Vida Maxima")]
        public int vidaMax { get; set; }
        [Display(Name = "Mana Maximo")]
        public int manaMax { get; set; }
        [Display(Name = "Energia Maxima")]
        public int energiaMax { get; set; }

        public ICollection<AtributoDePersonaje> atributos { get; set; }
        public ICollection<HabilidadDePersonaje> habilidades { get; set; }
    }
}
