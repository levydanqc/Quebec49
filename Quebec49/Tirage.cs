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
        public const int NB_MISES_MIN = 100000;
        public const int NB_MISES_MAX = 300000;

        /// <summary>
        /// Constructeur de la classe Tirage.
        /// Initie la date du tirage avec la date en paramètre.
        /// </summary>
        /// <param name="date">Date du tirage.</param>
        public Tirage(DateTime date)
        {
            m_dtmTirage = date.Date;
        }

        /// <summary>
        /// Accesseur de l'attribut m_dtmTirage.
        /// </summary>
        public DateTime Date
        {
            get { return m_dtmTirage; }
        }

        /// <summary>
        /// Accesseur du nombre de mises.
        /// </summary>
        public int NbMise
        {
            get
            {
                if (m_lesMises != null)
                {
                    return m_lesMises.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Accesseur de l'attribut m_leResultat.
        /// </summary>
        public Resultat Resultat
        {
            get { return m_leResultat; }
        }

        /// <summary>
        /// Permet de formater une chaine de caractère contenant l'ensemble
        /// des informations du tirage, comme le résultat de chaque lot, le nombre
        /// de mise ainsi que la date du tirage.
        /// </summary>
        /// <returns>Le chaine de caractère formatée.</returns>
        public override string ToString()
        {
            if (m_leResultat != null)
            {
                int[] resultat = new int[6];
                for (int i = 0; i < 6; i++)
                {
                    resultat[i] = m_leResultat.GetQuantite((Indice)i);
                }
                return String.Format($@"
{"Résultats du tirage du "} {m_dtmTirage.ToString("yyyy-MM-dd")}
{"Nombre de mises:",-22} {NbMise,10}
{"Gagnants du 2 sur 6+:",-22} {resultat[0],10}
{"Gagnants du 3 sur 6:",-22} {resultat[1],10}
{"Gagnants du 4 sur 6:",-22} {resultat[2],10}
{"Gagnants du 5 sur 6:",-22} {resultat[3],10}
{"Gagnants du 5 sur 6+:",-22} {resultat[4],10}
{"Gagnants du 6 sur 6:",-22} {resultat[5],10}");
            }
            else
            {
                return "Les mises n'ont pas encore été validées pour ce tirage.";
            }
        }

        /// <summary>
        /// Permet d'inscrire les mises du tirage.
        /// </summary>
        /// <param name="nbMises">Nombre de mise à inscrire dans le tirage.</param>
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

        /// <summary>
        /// Effecteur le tirage des numéros gagnants.
        /// Enregistre 6 numéros choisis au hasard et un numéro complémentaire.
        /// </summary>
        /// <returns>True si le tirage s'est bien effectuer, False autrement.</returns>
        public bool Effectuer()
        {
            if (m_lesMises != null) // S'il y a des mises inscrites
            {
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
                // Copier la sélection de 6 nombres classés en ordre croissant
                iLesNombresCroissants.CopyTo(m_iLesNombresGagnants, 0);
                do
                {
                    next = Aleatoire.GenererNombre(48) + 1;
                } while (m_iLesNombresGagnants.Contains(next));
                m_iLesNombresGagnants[m_iLesNombresGagnants.Length - 1] = next; // Ajouter le nombre complémentaire
                return true;
            }
            else // S'il n'y a pas de mise
            {
                return false;
            }
        }

        /// <summary>
        /// Valide les mises préalablement créées et enregistre
        /// le nombre de gagnants pour chaque lot dans l'attribut m_leResultat.
        /// </summary>
        /// <returns>True si les mises ont bien été effectuées, False dans le cas
        /// contraire.</returns>
        public bool ValiderMises()
        {
            m_leResultat = new Resultat();
            int iComplementaire = m_iLesNombresGagnants[m_iLesNombresGagnants.Length - 1];
            foreach (var mise in m_lesMises) // Parcourir toutes les mises
            {
                int iNbCorrespondants = 0; // Nombre de numéros identiques
                bool bAvecComplementaire = false; // À le numéro complémetaire

                for (int i = 0; i < Mise.iTailleSelection; i++) // Comparer les numéros avec la mise
                {
                    int x = mise.GetNombre(i);
                    if (m_iLesNombresGagnants[0..Mise.iTailleSelection].Contains(x))
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
                 */
                if (iNbCorrespondants >= 2) // Gagnant d'un lot
                {
                    if (iNbCorrespondants == 2 && bAvecComplementaire) // 2 + C
                    {
                        m_leResultat.AugmenterQuantite(Indice.DeuxSurSixPlus);
                    }
                    else if (iNbCorrespondants == 5 && bAvecComplementaire) // 5 + C
                    {
                        m_leResultat.AugmenterQuantite(Indice.CinqSurSixPlus);
                    }
                    else if (iNbCorrespondants >= 3 && iNbCorrespondants <= 5)  // 3, 4, 5
                    {
                        m_leResultat.AugmenterQuantite((Indice)iNbCorrespondants - 2);
                    }
                    else if (iNbCorrespondants == 6) // 6
                    {
                        m_leResultat.AugmenterQuantite(Indice.SixSurSix);
                    }

                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (m_leResultat.GetQuantite((Indice)i) != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
