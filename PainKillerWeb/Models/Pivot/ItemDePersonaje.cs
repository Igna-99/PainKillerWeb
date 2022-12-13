using PainKillerWeb.Models.Main;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PainKillerWeb.Models.Pivot
{
    public class ItemDePersonaje
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "´Personaje")]
        public int personajeId { get; set; }
        [Required]
        [Display(Name = "Item")]
        public int itemId { get; set; }


        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }


        public Personaje Personaje { get; set; }
        public Item Item { get; set; }
    }
}
