/******************************************************************************
 * Classe:  Program
 * 
 * Fichier: Program.cs
 * 
 * Auteur:  Dan Lévy
 * 
 * But:     Lancer une simulation de la Loterie selon les règles du Loto Québec.
 * ***************************************************************************/
using System;
using Utilitaires;
using SimulationLoterie;
namespace Quebec49
{
    class Program
    {
        static void Main(string[] args)
        {
            string strMenu = "Menu principal\n\n" +
                "[1] Génération de données\n" +
                "[2] Résultats des tirages\n" +
                "[3] Sommaire des résultats\n" +
                "[4] Auteur\n" +
                "[5] Quitter\n\n" +
                "Votre choix: ";


            string strAsciiName = @"
                ____                 __                   
               / __ \____ _____     / /   ___ _   ____  __
              / / / / __ `/ __ \   / /   / _ \ | / / / / /
             / /_/ / /_/ / / / /  / /___/  __/ |/ / /_/ / 
            /_____/\__,_/_/ /_/  /_____/\___/|___/\__, /  
                                                 /____/   ";


            GestionnaireTirages leGestionnaireTirages = null;

            Console.Clear();
            Console.Write(strMenu);
            string strChoix = Console.ReadLine();

            while (strChoix != "5")
            {
                switch (strChoix)
                {
                    case "1": // Génération de données
                        Console.Clear();
                        leGestionnaireTirages = new GestionnaireTirages();
                        for (int i = 0; i < GestionnaireTirages.NB_TIRAGES;
                            i++)
                        {
                            Tirage leTirage = leGestionnaireTirages
                                .GetTirage(i);
                            Console.WriteLine("Génération du tirage du " +
                                $"{leTirage.Date.ToString("yyyy-MM-dd")}...");
                            // Nombre entre [100 000, 200 000]
                            leTirage.InscrireMises(Aleatoire.GenererNombre(
                                Tirage.NB_MISES_MAX - Tirage.NB_MISES_MIN) +
                                Tirage.NB_MISES_MIN);
                            leTirage.Effectuer();
                        }
                        Console.WriteLine();
                        break;
                    case "2": // Résultat des tirages
                        Console.Clear();
                        if (leGestionnaireTirages != null)
                        {
                            for (int i = 0; i < GestionnaireTirages.NB_TIRAGES;
                                i++)
                            {
                                bool bValidees = leGestionnaireTirages
                                    .GetTirage(i).ValiderMises();
                                if (bValidees)
                                {
                                    string strResultat = leGestionnaireTirages
                                        .GetTirage(i).ToString();
                                    Console.WriteLine(strResultat);
                                }
                                else
                                {
                                    Console.WriteLine("Aucun résultat à " +
                                        "afficher pour ce tirage.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vous devez avoir généré des " +
                                "données pour voir les résultats des tirages." +
                                "\n");
                        }

                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Sommaire des résultats");
                        if (leGestionnaireTirages != null)
                        {
                            int iNbMisesTotal = 0;
                            int[] lesResultatsTotaux = new int[6];
                            for (int i = 0; i < GestionnaireTirages.NB_TIRAGES;
                                i++)
                            {
                                iNbMisesTotal += leGestionnaireTirages
                                    .GetTirage(i).NbMise;
                                for (int j = 0; j < 6; j++)
                                {
                                    lesResultatsTotaux[j] +=
                                        leGestionnaireTirages.GetTirage(i)
                                    .Resultat.GetQuantite((Indice)j);
                                }
                            }
                            string strResultat = String.Format($@"
{"Nombre de mises:",-22} {iNbMisesTotal,10}
{"Gagnants du 2 sur 6+:",-22} {lesResultatsTotaux[0],10}
{"Gagnants du 3 sur 6:",-22} {lesResultatsTotaux[1],10}
{"Gagnants du 4 sur 6:",-22} {lesResultatsTotaux[2],10}
{"Gagnants du 5 sur 6:",-22} {lesResultatsTotaux[3],10}
{"Gagnants du 5 sur 6+:",-22} {lesResultatsTotaux[4],10}
{"Gagnants du 6 sur 6:",-22} {lesResultatsTotaux[5],10}");
                            Console.WriteLine(strResultat);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Vous devez avoir généré des " +
                                "données et valider les mises " +
                            "pour voir les résultats des tirages.\n");
                        }

                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine(strAsciiName);
                        Console.WriteLine("\n\n\n\t\tTravail réalisé par " +
                            "Dan Levy.");
                        Console.WriteLine("\nDA: 181154");
                        Console.WriteLine("\nGitLab: " +
                            "https://gitlab.com/levydanqc/quebec49.git");
                        Console.WriteLine("\nCEGEP François-Xavier-Garneau" +
                            "\n\n");

                        break;
                    default:
                        Console.Write("Choix invalide. ");
                        break;
                }

                Console.Write("Appuyez sur <Entrée> pour continuer...");
                Console.ReadLine();
                Console.Clear();
                Console.Write(strMenu);
                strChoix = Console.ReadLine();
            }
            Console.Clear();
        }
    }
}