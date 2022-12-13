using Microsoft.EntityFrameworkCore;
using PainKillerWeb.Models.Main;
using PainKillerWeb.Models.Pivot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PainKillerWeb.Context
{
    public class PainKillerDbContext : DbContext
    {
        public PainKillerDbContext(DbContextOptions<PainKillerDbContext> options) : base(options)
        {

        }

        public DbSet<Personaje> personajes { get; set; }
        public DbSet<Habilidad> habilidades { get; set; }
        public DbSet<Atributo> atributos { get; set; }
        public DbSet<AtributoDePersonaje> atributosDePersonajes { get; set; }
        public DbSet<HabilidadDePersonaje> habilidadDePersonajes { get; set; }
        public DbSet<Raza> raza { get; set; }
        public DbSet<Elemento> elementos { get; set; }
        public DbSet<Hechizo> hechizos { get; set; }
        public DbSet<HechizoDePersonaje> hechizosDePersonajes{ get; set; }
        public DbSet<Distancia> distancias { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<ItemDePersonaje> itemsDePersonajes { get; set; }
    }
}
