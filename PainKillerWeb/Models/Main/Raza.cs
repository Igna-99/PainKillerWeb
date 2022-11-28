using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PainKillerWeb.Models.Main
{
    public class Raza
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = ErrMsj.RequeridoM)]
        [Display(Name = "Nombre")]
        [MinLength(3, ErrorMessage = ErrMsj.largoMin)]
        [MaxLength(21, ErrorMessage = ErrMsj.largoMax)]
        public string nombre { get; set; }

        [Display(Name = "Atributo Fuerte")]
        public int idAtributoRelevante { get; set; }
        public Atributo atributoRelevante { get; set; }

        [Display(Name = "Segundo Atributo Fuerte")]
        public int idAtributoRelevante2 { get; set; }
        public Atributo atributoRelevante2 { get; set; }

        [Display(Name = "Atributo Debil ")]
        public int idAtributoPesimo { get; set; }
        public Atributo atributoPesimo { get; set; }
    }
}
