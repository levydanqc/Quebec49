using System;

namespace Utilitaires
{
    public static class Aleatoire
    {
        public static Random g_rndGenerateur = new Random();

        public static int GenererNombre(int iBorneSuperieure)
        {
            return g_rndGenerateur.Next(iBorneSuperieure + 1);
        }
    }
    public static class Interval
    {
        /// <summary>
        /// Fonction évaluant un nombre dans l'intervalle [1, 49].
        /// </summary>
        /// <param name="x">Nomre devant être évalué.</param>
        /// <param name="max">Borne inclusive maximale de l'interval.</param>
        /// <param name="min">Borne inclusive minimale de l'interval.</param>
        /// <returns>'True' si le nombre est dans l'intervalle,
        /// 'False' autrement.</returns>
        public static bool InRange(int x, int max = 49, int min = 1) => ((x - max) * (x - min) <= 0);
    }
}