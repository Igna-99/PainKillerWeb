using PainKillerWeb.Models.Main;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PainKillerWeb.Models.Pivot
{
    public class HechizoDePersonaje
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "´Personaje")]
        public int personajeId { get; set; }
        [Required]
        [Display(Name = "Hechizo")]
        public int HechizoId { get; set; }

        public Personaje Personaje { get; set; }
        public Hechizo Hechizo{ get; set; }
    }
}
