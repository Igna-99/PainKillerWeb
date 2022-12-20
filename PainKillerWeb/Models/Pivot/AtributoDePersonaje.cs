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
        [Range(0, 10, ErrorMessage = "NIVEL MINIMO NO ALCANZADO")]
        public int nivel { get; set; }

        public Personaje personaje { get; set; }
        public Atributo atributo { get; set; }
    }
}
