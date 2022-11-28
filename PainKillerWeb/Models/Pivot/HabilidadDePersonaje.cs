using PainKillerWeb.Models.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PainKillerWeb.Models.Pivot
{
    public class HabilidadDePersonaje
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int personajeId { get; set; }
        [Required]
        public int HabilidadId { get; set; }
        [Required]
        public int Nivel { get; set; }

        public Personaje Personaje { get; set; }
        public Habilidad Habilidad { get; set; }
    }
}
