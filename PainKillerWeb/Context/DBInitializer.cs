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
            if (context.personajes.Any() || context.habilidades.Any() || context.atributos.Any())
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
                    descripcion=" Es la habilidad de usar arcos, el arma de proyectil más común de la época. " +
                    "Los arqueros experimentados también saben cómo cuidar y reparar sus armas e incluso como fabricarlas. Tiro con arco cubre ballestas y otras armas relacionadas. " +
                    "No cubre jabalinas y otras armas arrojadizas."
                },
                new Habilidad
                {
                    nombre = "Hechiceria",
                    atributoId = 3,
                    descripcion=" Es la habilidad que contiene los conocimientos que todo usuario de las artes mágicas necesita." +
                    " Provee la habilidad de identificar conjuros cuando estén siendo lanzados o cuando estén en funcionamiento," +
                    " así mismo la comprensión de rituales y simbologías. Esta habilidad en sí no permite realizar magia," +
                    " pero si distinguir sus fuentes, debilites y fortalezas. "
                },
                new Habilidad
                {
                    nombre = "Mano a mano",
                    atributoId = 1,
                    descripcion="Esta es la habilidad de luchar desarmado, peleando a uñas y dientes, o simplemente agitando los brazos y " +
                    "confiando en la experiencia de unos sobre dónde y cómo golpear. Pelear bien requiere coordinación," +
                    "velocidad, la capacidad de aguantar dolor y la voluntad de infligir dolor. "
                },
                new Habilidad
                {
                    nombre = "Medicina",
                    atributoId = 3,
                    descripcion="Esta es la habilidad de luchar desarmado, peleando a uñas y dientes, o simplemente agitando los brazos y " +
                    "confiando en la experiencia de unos sobre dónde y cómo golpear. Pelear bien requiere coordinación," +
                    "velocidad, la capacidad de aguantar dolor y la voluntad de infligir dolor. "
                },
                new Habilidad
                {
                    nombre = "Interpretación",
                    atributoId = 6,
                    descripcion="Es la habilidad de realizar proezas artísticas como cantar, bailar, actuar o tocar un instrumento musical. Esta " +
                    "es una habilidad genérica y deberías elegir un campo de experiencia cuando la adquieras por primera vez. " +
                    "Cada vez que quieras obtener una nueva disciplina artística deberás pagarla por separado."
                },
                new Habilidad
                {
                    nombre = "Concentración",
                    atributoId = 4,
                    descripcion="Es la habilidad que permite a un personaje ignorar las distracciones físicas o mentales para seguir con su " +
                    "accionar. Generalmente utilizada por aquellos que unen la mente y el cuerpo. Esta habilidad permite a los " +
                    "magos seguir conjurando a pesar del dolor y a los faquires resistir terribles torturas sin siquiera pestañear."
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
