using System.ComponentModel.DataAnnotations;

namespace PainKillerWeb.Models.Main
{
    public class Item
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
    }
}
