using PainKillerWeb.Models.Main;
using PainKillerWeb.Models.Pivot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PainKillerWeb.Context
{
    public static class DbInitializer
    {
        public static void Initialize(PainKillerDbContext context)
        {
            // Look for any characters.
            if (context.personajes.Any() || context.atributos.Any())
            {
                return;   // DB has been seeded
            }

            var atributos = new Atributo[]
            {
                new Atributo(){ nombre= "Fuerza" },
                new Atributo{  nombre= "Agilidad" },
                new Atributo{  nombre= "Fe" },
                new Atributo{  nombre= "Inteligencia" },
            };

            foreach (var a in atributos)
            {
                context.atributos.Add(a);
            }

            context.raza.Add(new Raza() { nombre = "Humano", idAtributoRelevante = 1 , idAtributoPesimo = 4});
            context.raza.Add(new Raza() { nombre = "Elfo", idAtributoRelevante = 2, idAtributoRelevante2 = 4, idAtributoPesimo = 1});

            context.SaveChanges();

            var raza = context.raza.FirstOrDefault();

            var atributosCreados = context.atributos.ToArray();

            var personajes = new Personaje[]
            {
                new Personaje
                {
                    nombre= "Patricio",
                    atributos = new List<AtributoDePersonaje>()
                    {
                        new AtributoDePersonaje() { atributo = atributosCreados[0], nivel = 1 },
                        new AtributoDePersonaje() { atributo = atributosCreados[1], nivel = 6 },
                        new AtributoDePersonaje() { atributo = atributosCreados[2], nivel = 2 },
                        new AtributoDePersonaje() { atributo = atributosCreados[3], nivel = 4 }
                    },
                    raza = raza
                },
                new Personaje
                {
                    nombre= "Random",
                    atributos = new List<AtributoDePersonaje>()
                    {
                        new AtributoDePersonaje() { atributo = atributosCreados[1], nivel = 2}
                    },
                    raza = raza

                },
            };

            foreach (var p in personajes)
            {
                context.personajes.Add(p);
            }

            context.SaveChanges();
        }
    }
}
