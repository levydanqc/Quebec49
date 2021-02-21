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
                for (int i = 0; i < 6; i++)
                {
                    resultat[i] = m_leResultat.GetQuantite((Indice)i);
                }
                return String.Format(
                $@"
                        {"Résultats du tirage du"} {m_dtmTirage.ToString("yyyy-MM-dd")}
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
            };
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

            }
        }
    }
}
