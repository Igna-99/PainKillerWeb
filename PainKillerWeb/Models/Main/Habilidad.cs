﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PainKillerWeb.Models.Main
{
    public class Habilidad
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string nombre { get; set; }

        [Display(Name = "atributo")]
        [Required(ErrorMessage = ErrMsj.RequeridoM)]
        public int atributoId { get; set; }

        public Atributo atributo { get; set; }

    }
}