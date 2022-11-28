using PainKillerWeb.Models.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PainKillerWeb.Models.Pivot
{
    public class AtributoDePersonaje
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int personajeId { get; set; }
        [Required]
        public int atributoId { get; set; }
        [Required]
        [Range(1, 20, ErrorMessage = "SKERRY")]
        public int nivel { get; set; }

        public Personaje personaje { get; set; }
        public Atributo atributo { get; set; }
    }
}
