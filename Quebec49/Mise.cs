/******************************************************************************
 * Classe:  Mise
 * 
 * Fichier: Mise.cs
 * 
 * Auteur:  Dan Lévy
 * 
 * But:     Représente un groupe de nombres (6) à jouer pour un tirage.
 * ***************************************************************************/
namespace SimulationLoterie
{
    public class Mise
    {
        private int[] m_iLesNombres = new int[49];

        /// <summary>
        /// Constructeur de la classe Mise.
        /// Déclaration et initialisation du vecteur des nombres du Loto Québec.
        /// Les nombres sont dans l'interval [1,  49].
        /// </summary>
        public Mise()
        {
            for (int i = 1; i < 50; i++)
            {
                m_iLesNombres[i] = i;
            }
        }
        /// <summary>
        /// Fonction évaluant un nombre dans l'intervalle [1, 49].
        /// </summary>
        /// <param name="x">Nomre devant être évalué</param>
        /// <returns>'True' si le nombre est dans l'intervalle,
        /// 'False' autrement.</returns>
        bool inRange(int x) => ((x - 49) * (x - 1) <= 0);
        /// <summary>
        /// Permet d'obtenir un nombre dans le vecteur des nombres du Loto Québec.
        /// </summary>
        /// <param name="indice">Indice dont on veut le nombre.</param>
        /// <returns>Le nombre correspondant dans le vecteur ou -1 si l'indice n'est pas valide.</returns>
        public int GetNombre(int indice)
        {
            if (inRange(indice))
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
