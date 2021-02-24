/******************************************************************************
 * Classe:  GestionnaireTirages
 * 
 * Fichier: GestionnaireTirages.cs
 * 
 * Auteur:  Dan Lévy
 * 
 * But:     Représente et gère un ensemble de tirages.
 * ***************************************************************************/
using System;
using Utilitaires;
namespace SimulationLoterie
{
    public class GestionnaireTirages
    {
        public const int NB_TIRAGES = 104; // Nombre de tirage par année (2/semaine) 
        private Tirage[] m_lesTirages;

        /// <summary>
        /// Constructeur de la classe GestionnaireTirages.
        /// Déclaration et initialisation des instances de la classe Tirage
        /// dont la date correspont aux 104 mercredi et samedi suivant la date actuelle.
        /// </summary>
        public GestionnaireTirages()
        {
            /* Les valeurs de dtmDate.DayOfWeek sont:
             * Dimanche     -> 0
             * Lundi        -> 1
             * Mardi        -> 2
             * Mercredi     -> 3 (tirage)
             * Jeudi        -> 4
             * Vendredi     -> 5
             * Samedi       -> 6 (tirage)
             */
            DateTime dtmToday = DateTime.Today;
            m_lesTirages = new Tirage[NB_TIRAGES];

            for (int i = 0; i < m_lesTirages.Length; i++)
            {
                while ((int)dtmToday.DayOfWeek != 3 && (int)dtmToday.DayOfWeek != 6)
                {
                    // Ajoute 1 jours tant que != Mercredi ou Samedi
                    dtmToday = dtmToday.AddDays(1);
                }
                m_lesTirages[i] = new Tirage(dtmToday);
                dtmToday = dtmToday.AddDays(1);
            }

        }

        /// <summary>
        /// Permet d'obtenir un tirage parmis le gestionnaire de tirages.
        /// </summary>
        /// <param name="indice">L'indice dans le tableau du gestionnaire de tirages.</param>
        /// <returns>Un Tirage à l'indice fournis dans le tableau du gestionnaire de tirages.</returns>
        public Tirage GetTirage(int indice)
        {
            if (Interval.InRange(indice, 104, 0))
            {
                return m_lesTirages[indice];
            }
            else
            {
                return null;
            }
        }
    }
}
