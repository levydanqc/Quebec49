/******************************************************************************
 * Classe:  Resultat
 * 
 * Fichier: Resultat.cs
 * 
 * Auteur:  Dan Lévy
 * 
 * But:     Représente les résultats d'un tirage.
 * ***************************************************************************/
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

    public class Resultat
    {
        private int[] m_iLesQuantites;

        /// <summary>
        /// Constructeur de la classe Resultat.
        /// Déclaration et initialisation   du vecteur du nombre de gagnant par
        /// catégorie.
        /// </summary>
        public Resultat()
        {
            m_iLesQuantites = new int[6];
        }
        /// <summary>
        /// Nombre de gagnant par catégorie.
        /// </summary>
        /// <param name="indice">Indice du groupe dont on veut le nombre de
        /// gagnant.</param>
        /// <returns>Le nombre de gagnant dans une catégorie.</returns>
        public int GetQuantite(Indice indice)
        {
            return m_iLesQuantites[(int)indice];
        }
        /// <summary>
        /// Augmente le nombre de gagnant dans une catégorie.
        /// </summary>
        /// <param name="indice">Indice du groupe dont on veut augmenter la quantité
        /// de gagnant</param>
        public void AugmenterQuantite(Indice indice)
        {
            m_iLesQuantites[(int)indice]++;
        }
    }
}