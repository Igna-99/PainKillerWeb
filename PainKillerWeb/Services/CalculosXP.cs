using PainKillerWeb.Models.Main;
using PainKillerWeb.Models.Pivot;
using System.Collections.Generic;
using System.Reflection;

namespace PainKillerWeb.Services
{
    public class CalculosXP
    {
        public CalculosXP()
        {

        }

        public int costeCreacionPJ(Raza raza, List<AtributoDePersonaje> atributosDePersonaje)
        {
            int aPesimo = raza.idAtributoPesimo;
            int aRelevante = raza.idAtributoRelevante;
            int aRelevante2 = raza.idAtributoRelevante2;

            int res = 0, modPes = 6, modRel = 4, mod = 5; ;

            foreach(var item in atributosDePersonaje)
            {
                if (aPesimo == item.atributoId)
                {
                  res += xpPorNivel(item.nivel, modPes);
                 
                }else if (aRelevante == item.atributoId || aRelevante2 == item.atributoId)
                {
                    res += xpPorNivel(item.nivel, modRel);
                }
                else
                {
                    res += xpPorNivel(item.nivel, mod);
                }
            }


            return res;
        }

        private int xpPorNivel(int nivel, int mod)
        {
            int res = 0;
            
            for(int i = 0; i != nivel; i++)
            {
                res += res + mod; 

            }

            return res;
        }
    }
}
