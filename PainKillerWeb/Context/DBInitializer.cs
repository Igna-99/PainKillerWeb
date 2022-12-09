using Microsoft.EntityFrameworkCore.Internal;
using PainKillerWeb.Models.Main;
using PainKillerWeb.Models.Pivot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
                new Atributo{ nombre= "Fuerza"},
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
                        new AtributoDePersonaje() { atributo = atributosCreados[5], nivel = 4 },
                        new AtributoDePersonaje() { atributo = atributosCreados[4], nivel = 4 },
                        new AtributoDePersonaje() { atributo = atributosCreados[3], nivel = 4 },
                        new AtributoDePersonaje() { atributo = atributosCreados[2], nivel = 2 },
                        new AtributoDePersonaje() { atributo = atributosCreados[1], nivel = 6 },
                        new AtributoDePersonaje() { atributo = atributosCreados[0], nivel = 2 }
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
                    descripcion=""
                },
                new Habilidad
                {
                    nombre = "Arqueria",
                    atributoId = 2,
                    descripcion=""
                },
                new Habilidad
                {
                    nombre = "Hechiceria",
                    atributoId = 3,
                    descripcion=""
                }
            };
            foreach (var p in habilidades)
            {
                context.habilidades.Add(p);
            }

            var elementos = new Elemento[]
            {
                new Elemento() {nombre = "Fuego"},
                new Elemento() {nombre = "Tierra"},
                new Elemento() {nombre = "Aire"},
                new Elemento() {nombre = "Agua"},
                new Elemento() {nombre = "Frio"},
                new Elemento() {nombre = "Rayo"},
                new Elemento() {nombre = "Cuerpo"},
                new Elemento() {nombre = "Espiritu"},
                new Elemento() {nombre = "Mente"},
                new Elemento() {nombre = "Astral"},
                new Elemento() {nombre = "Luz"},
                new Elemento() {nombre = "Oscuridad"}
            };

            foreach (var p in elementos)
            {
                context.elementos.Add(p);
            }

            var distancia = new Distancia[]
            {
                new Distancia() {nombre = "Vista"},
                new Distancia() {nombre = "Personal"},
                new Distancia() {nombre = "Toque"},
                new Distancia() {nombre = "Corto"},
                new Distancia() {nombre = "Medio"},
                new Distancia() {nombre = "Largo"},
                new Distancia() {nombre = "Epico"}
            };
            foreach (var p in distancia)
            {
                context.distancias.Add(p);
            }
            context.raza.Add(new Raza() { nombre = "Feerica", idAtributoRelevante = 2, idAtributoRelevante2 = 3, idAtributoPesimo = 1 });

            context.hechizos.Add(new Hechizo() { nombre = "Antorcha", costeExp = 1, distanciaId = 3, elementoId = 1, costeUso = 2, tipoCoste = 3, tiempo = "Instantaneo", duracion = "Un min por punto en FE", efecto = "Conjuras un pequeño orbe de fuego en las manos que da calor y luz como una antorcha." });
            context.hechizos.Add(new Hechizo() { nombre = "Saetas", costeExp = 2, distanciaId = 5, elementoId = 1, costeUso = 2, tipoCoste = 2, tiempo = "Instantaneo",
                efecto = "[Ignora resistencia a conjuros] [Proyectil] Lanza una hiriente saeta de fuego Ataque (Mente + Hechicería) daño 3K1 Inflige Daño Físico."
                //,
                //cadena = context.hechizos.Find(x => x.nombre == "Antorcha")
            });
           //Hay que agregar a cadena los hechizos que hagan cadena. Nunca puede ser el mismo hechizo


            context.SaveChanges();
        }
    }
}
