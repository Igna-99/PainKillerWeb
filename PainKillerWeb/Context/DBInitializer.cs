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
                new Atributo{ nombre= "Fuerza" },
                new Atributo{ nombre= "Agilidad" },
                new Atributo{ nombre= "Entendimiento" },
                new Atributo{ nombre= "Fe" },
                new Atributo{ nombre= "Bravura" },
                new Atributo{ nombre= "Personalidad" },
            };

            foreach (var a in atributos)
            {
                context.atributos.Add(a);
            }

            context.raza.Add(new Raza() { nombre = "Humano" });
            context.raza.Add(new Raza() { nombre = "Feerica", idAtributoRelevante = 2, idAtributoRelevante2 = 3, idAtributoPesimo = 1 });
            context.raza.Add(new Raza() { nombre = "Draconica", idAtributoRelevante = 1, idAtributoRelevante2 = 3, idAtributoPesimo = 6 });
            context.raza.Add(new Raza() { nombre = "Impia", idAtributoRelevante = 6, idAtributoRelevante2 = 3, idAtributoPesimo = 4 });
            context.raza.Add(new Raza() { nombre = "Sacra", idAtributoRelevante = 6, idAtributoRelevante2 = 4, idAtributoPesimo = 3 });
            context.raza.Add(new Raza() { nombre = "Bestia Magica", idAtributoRelevante = 1, idAtributoRelevante2 = 2, idAtributoPesimo = 4 });
            context.raza.Add(new Raza() { nombre = "Titanica", idAtributoRelevante = 1, idAtributoRelevante2 = 3, idAtributoPesimo = 2 });
            context.raza.Add(new Raza() { nombre = "Espiritu", idAtributoRelevante = 6, idAtributoRelevante2 = 5, idAtributoPesimo = 1 });
            context.raza.Add(new Raza() { nombre = "Chuta!", idAtributoRelevante = 1, idAtributoRelevante2 = 2, idAtributoPesimo = 3 });
            context.raza.Add(new Raza() { nombre = "Aberracion", idAtributoRelevante = 1, idAtributoRelevante2 = 5, idAtributoPesimo = 6 });

            context.SaveChanges();

            var raza = context.raza.FirstOrDefault();

            var atributosCreados = context.atributos.ToArray();

            var personajes = new Personaje[]
            {
                new Personaje
                {
                    nombre= "Test",
                    expActual = 15,
                    atributos = new List<AtributoDePersonaje>()
                    {
                        new AtributoDePersonaje() { atributo = atributosCreados[0], nivel = 2 },
                        new AtributoDePersonaje() { atributo = atributosCreados[1], nivel = 6 },
                        new AtributoDePersonaje() { atributo = atributosCreados[2], nivel = 2 },
                        new AtributoDePersonaje() { atributo = atributosCreados[3], nivel = 4 },
                        new AtributoDePersonaje() { atributo = atributosCreados[4], nivel = 4 },
                        new AtributoDePersonaje() { atributo = atributosCreados[5], nivel = 4 }
                    },
                    raza = raza

                }
            };

            foreach (var p in personajes)
            {
                context.personajes.Add(p);
            }

            var habilidades = new Habilidad[]
            {
                new Habilidad
                {
                    nombre = "Mandoble",
                    atributoId = 1,
                },
                new Habilidad
                {
                    nombre = "Arqueria",
                    atributoId = 2,
                },
                new Habilidad
                {
                    nombre = "Hechiceria",
                    atributoId = 3,
                }
            };
            foreach (var p in habilidades)
            {
                context.habilidades.Add(p);
            }

            context.SaveChanges();
        }
    }
}
