/******************************************************************************
 * Classe:  Mise
 * 
 * Fichier: Mise.cs
 * 
 * Auteur:  Dan Lévy
 * 
 * But:     Représente un groupe de nombres (6) à jouer pour un tirage.
 * ***************************************************************************/
using Utilitaires;
using System.Linq;
using System;
namespace SimulationLoterie
{
    public class Mise
    {
        public const int iTailleSelection = 6;
        private int[] m_iLesNombres;

        /// <summary>
        /// Constructeur de la classe Mise.
        /// Déclaration et initialisation du vecteur des nombres du Loto Québec.
        /// Les nombres sont dans l'interval [1,  49].
        /// </summary>
        public Mise()
        {
            m_iLesNombres = new int[iTailleSelection];
            int next;
            for (int i = 0; i < m_iLesNombres.Length; i++)
            {
                do
                {
                    next = Aleatoire.GenererNombre(48) + 1;
                } while (m_iLesNombres.Contains(next));
                m_iLesNombres[i] = next;
            }
            Array.Sort(m_iLesNombres);
        }
        /// <summary>
        /// Permet d'obtenir un nombre dans le vecteur des nombres du Loto Québec.
        /// </summary>
        /// <param name="indice">Indice dont on veut le nombre.</param>
        /// <returns>Le nombre correspondant dans le vecteur ou 
        /// -1 si l'indice n'est pas valide.</returns>
        public int GetNombre(int indice)
        {
            if (Interval.InRange(indice))
            {
                return m_iLesNombres[indice];
            }
            else
            {
                return -1;
            }
        }
    }
}
