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

        public Raza raza { get; set; }

        [Required(ErrorMessage = ErrMsj.RequeridoF)]
        [Display(Name = "Experiencia")]
        [Range(1, 1000, ErrorMessage = ErrMsj.RangoF)]
        public int expActual { get; set; }

        public int expGastada { get; set; }
        [Required(ErrorMessage = ErrMsj.RequeridoF)]
        [Display(Name = "Vida Maxima")]
        [Range(1, 20, ErrorMessage = ErrMsj.RangoF)]
        public int vidaMax { get; set; }
        [Required(ErrorMessage = ErrMsj.RequeridoM)]
        [Display(Name = "Mana Maxima")]
        [Range(1, 20, ErrorMessage = ErrMsj.RangoM)]
        public int manaMax { get; set; }
        [Required(ErrorMessage = ErrMsj.RequeridoF)]
        [Display(Name = "Energia Maxima")]
        [Range(1, 20, ErrorMessage = ErrMsj.RangoF)]
        public int energiaMax { get; set; }

        public ICollection<AtributoDePersonaje> atributos { get; set; }
        public ICollection<HabilidadDePersonaje> habilidades { get; set; }
    }
}
