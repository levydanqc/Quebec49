/******************************************************************************
 * Classe:  Tirage
 * 
 * Fichier: Tirage.cs
 * 
 * Auteur:  Dan Lévy
 * 
 * But:     Représente un tirage à une date donnée.
 * ***************************************************************************/
using System;
using Utilitaires;
using System.Linq;
namespace SimulationLoterie
{
    public class Tirage
    {
        private DateTime m_dtmTirage;
        private int[] m_iLesNombresGagnants;
        private Mise[] m_lesMises;
        private Resultat m_leResultat;
        private const int NB_MISES_MIN = 100000;
        private const int NB_MISES_MAX = 300000;

        public Tirage(DateTime date)
        {
            m_dtmTirage = date.Date;
        }
        public DateTime Date
        {
            get { return m_dtmTirage; }
        }
        public int NbMise
        {
            get
            {
                try
                {
                    return m_lesMises.Length;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public Resultat Resultat
        {
            get { return m_leResultat; }
        }
        public override string ToString()
        {
            try
            {
                int[] resultat = new int[6];
                for (int i = 0; i < Mise.iTailleSelection; i++)
                {
                    resultat[i] = m_leResultat.GetQuantite((Indice)i);
                }
                return String.Format(
                $@"
                        {"Résultats du tirage du "} {m_dtmTirage.ToString("yyyy-MM-dd")}
                        {"Nombre de mises:",-22} {NbMise,10}
                        {"Gagnants du 2 sur 6+:",-22} {resultat[0],10}
                        {"Gagnants du 3 sur 6:",-22} {resultat[1],10}
                        {"Gagnants du 4 sur 6:",-22} {resultat[2],10}
                        {"Gagnants du 5 sur 6:",-22} {resultat[3],10}
                        {"Gagnants du 5 sur 6+:",-22} {resultat[4],10}
                        {"Gagnants du 6 sur 6:",-22} {resultat[5],10}
                ");
            }
            catch
            {
                return "Les mises n'ont pas encore été validées pour ce tirage.";
            }
        }
        public void InscrireMises(int nbMises)
        {
            // Déclaration du vecteur de Mise
            if (Interval.InRange(nbMises, 100000, 300000))
            {
                m_lesMises = new Mise[nbMises];
            }
            else
            {
                m_lesMises = new Mise[200000];
            }
            // Initialisation du vecteur de Mise
            for (int i = 0; i < m_lesMises.Length; i++)
            {
                m_lesMises[i] = new Mise();
            }
        }
        public bool Effectuer()
        {
            try // S'il y a des mises inscrites
            {
                int test = m_lesMises.Length; // Permet de vérifier que les mises ont été inscrites;
                int[] iLesNombresCroissants = new int[Mise.iTailleSelection];
                m_iLesNombresGagnants = new int[Mise.iTailleSelection + 1];
                int next;
                for (int i = 0; i < iLesNombresCroissants.Length; i++)
                {
                    do
                    {
                        next = Aleatoire.GenererNombre(48) + 1;
                    } while (iLesNombresCroissants.Contains(next));
                    iLesNombresCroissants[i] = next;
                }
                Array.Sort(iLesNombresCroissants);
                iLesNombresCroissants.CopyTo(m_iLesNombresGagnants, 0);
                do
                {
                    next = Aleatoire.GenererNombre(48) + 1;
                } while (m_iLesNombresGagnants.Contains(next));
                m_iLesNombresGagnants[m_iLesNombresGagnants.Length - 1] = next;
                return true;
            }
            catch // S'il n'y a pas de mise
            {
                return false;
            }
        }
        public bool ValiderMises()
        {
            m_leResultat = new Resultat();
            int iComplementaire = m_iLesNombresGagnants[m_iLesNombresGagnants.Length - 1];
            foreach (Mise mise in m_lesMises) // Parcourir toutes les mises
            {
                int iNbCorrespondants = 0; // Nombre de numéros identiques
                bool bAvecComplementaire = false; // À le numéro complémetaire

                for (int i = 0; i < Mise.iTailleSelection; i++) // Calcul des numéros identique
                {
                    if (m_iLesNombresGagnants[0..Mise.iTailleSelection].Contains(mise.GetNombre(i)))
                    {
                        iNbCorrespondants++;
                    }
                    if (iComplementaire == mise.GetNombre(i) && !bAvecComplementaire)
                    {
                        bAvecComplementaire = true;
                    }
                }

                /* Ajout de la mise dans le compte du résultat
                 * Les valeurs de iNbCorrespondants possibles sont :
                 * 0, 1, 2, 3, 4, 5 et 6
                 * Les valeurs à rejeter son 0 et 1, car aucun lot n'y est associé.
                 * Il reste donc 2, 3, 4, 5 et 6.
                 * 2+C  --> Indice 0
                 * 3    --> Indice 1
                 * 4    --> Indice 2
                 * 5    --> Indice 3
                 * 5+C  --> Indice 4
                 * 6    --> Indice 5
                 * Donc il y a une tendance de -2.
                 * Aussi, pour s'occuper des cas avec nombre complémentaire,
                 * une formule mathématique permet de les séparer du groupe:
                 * (x - 2) % 3 ==> retourne 0 lorsque i est égale à 2 ou 5.
                 * 
                 * 
                 * 
                 * Indice x = (Indice)Enum.Parse(typeof(Indice), "CinqSurSixPlus", true);
                 */
                if (iNbCorrespondants >= 2) // Gagnant d'un lot
                {
                    if ((iNbCorrespondants - 2) % 3 == 0 && bAvecComplementaire) // Alors 2 ou 5
                    {
                        m_leResultat.AugmenterQuantite((Indice)0);
                        // TODO: Augmenter les résultats pour chaque lot..
                        // TODO: Trouver une relation
                    }
                }
            }


            return false;
        }
    }
}
