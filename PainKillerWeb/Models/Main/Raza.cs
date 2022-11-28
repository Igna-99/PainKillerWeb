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
        [Required]
        public string nombre { get; set; }
        public int idAtributoRelevante { get; set; }
        public Atributo atributoRelevante { get; set; }
        public int idAtributoRelevante2 { get; set; }
        public Atributo atributoRelevante2 { get; set; }
        public int idAtributoPesimo { get; set; }
        public Atributo atributoPesimo { get; set; }
    }
}
