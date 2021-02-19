/******************************************************************************
 * Classe:  Resultats
 * 
 * Fichier: Resultats.cs
 * 
 * Auteur:  Dan Lévy
 * 
 * But:     Représente les résultats d'un tirage.
 * ***************************************************************************/
using System;
namespace SimulationLoterie
{
    /// <summary>
    /// Représente les différentes catégories pouvant remporter un lot.
    /// <remark>
    /// Un indice finissant par 'Plus' indique qu'il faut
    /// avoir le nombre complémentaire.
    /// </remark>
    /// </summary>
    public enum Indice
    {
        DeuxSurSixPlus,
        TroisSurSix,
        QuatreSurSix,
        CinqSurSix,
        CinqSurSixPlus,
        SixSurSix
    }

    public class Resulats
    {
        private int[] m_iLesQuantites;

        public Resulats()
        {
            m_iLesQuantites = new int[] { 0, 0, 0, 0, 0, 0 };
        }
        public int GetQuantite(Indice indice)
        {
            return m_iLesQuantites[(int)indice];
        }
        public void AugmenterQuantite(Indice indice, int quantite = 1)
        {
            m_iLesQuantites[(int)indice] += quantite;
        }
    }
}