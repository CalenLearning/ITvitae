using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;

namespace GroenOuderbijdrageSchool
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Een basisschool berekent de ouderbijdrage als volgt:
            • Een basisbedrag van € 50,=. Daarnaast voor elk kind jonger dan 10 jaar € 25,= (voor maximaal 3
            kinderen) en voor elk kind van 10 jaar en ouder € 37,= (voor maximaal 2 kinderen).
            • De maximale ouderbijdrage bedraagt € 150,=.
            • Voor éénoudergezinnen wordt op de berekende bijdrage (nádat de controle op het maximum
            heeft plaatsgevonden) een reductie toegepast van 25%.
            De te ontwikkelen software moet aan de hand van de gezinsgegevens de verschuldigde
            ouderbijdrage bepalen. De leeftijd van elk kind moet aan de hand van geboortedatum en een
            peildatum worden berekend.
            */

            //Basis €50
            //Elk kind <10 = €25 (max 3)
            //Elk kind >=10 = €37 (max 2)
            //Eenouderkorting -25%
            //Jaarlijkse peildatum

            Console.WriteLine("Hoeveel kinderen heeft u?");
            string kinderenInvoer = Console.ReadLine();
            int kinderen = 0;
            try
            {
                kinderen = Convert.ToInt32(kinderenInvoer);
            }
            catch (Exception)
            {
                Console.WriteLine("Ongeldige invoer. Kies een getal van 1 of hoger.");
            }
            if (kinderen < 1)
            {
                Console.WriteLine("Ongeldige invoer. Kies een getal van 1 of hoger.");
            }

            int onder10 = 0;
            int over10 = 0;
            double prijs = 50;
            for (int aantal = 1; aantal <= kinderen; aantal++)
            {
                Console.WriteLine("Wat is de geboortedatum van uw {0}e kind? Voer dit in als JJJJ MM DD. Bijvoorbeeld 2012 2 1 voor 1 februari 2012.", aantal);
                string gebDatum = Console.ReadLine();
                DateTime geboorteDatum = DateTime.Now;
                try
                {
                    geboorteDatum = Convert.ToDateTime(gebDatum);
                }
                catch (Exception)
                {
                    Console.WriteLine("Ongeldige invoer. Schrijf de geboortedatum als JJJJ MM DD. Bijvoorbeeld 2012 2 1 voor 1 februari 2012.");
                }
                //Wordt een peildatum bedoeld als in een datum aan het begin van een schooljaar?
                //Ik ben er nu van uitgegaan dat alles na peildatum min 10jaar in het onder 10-tarief komt.
                if (geboorteDatum > DateTime.Now) //if geboorteDatum in toekomst
                {
                    kinderen = 0;
                    break;
                }
                DateTime peilDatum = new DateTime(DateTime.Now.Year - 10, 9, 1);
                if (peilDatum < geboorteDatum) //if peilDatum is langer geleden dan geboorteDatum
                {
                    onder10++;
                    if (onder10 <= 3)
                    {
                        prijs += 25;
                        Console.WriteLine(prijs);
                    }
                }
                else
                {
                    over10++;
                    if (over10 <= 2)
                    {
                        prijs += 37;
                        Console.WriteLine(prijs);
                    }
                }
            }

            //if-statement zodat dit niet verschijnt bij een invoer van <= 0 kinderen, of een toekomstige geboortedatum.
            if (kinderen >= 1)
            {
                Console.WriteLine("Bent u een eenoudergezin? Voer het getal 1 of 2 in.\n" +
                    "1. Ja\n" +
                    "2. Nee");
                string eenOuder = Console.ReadLine();
                if (eenOuder == "1")
                {
                    prijs *= 0.75;
                }
                //Ik zou weer {1:C} kunnen neerzetten, maar ik krijg geen €-teken geprint op mijn computer.
                Console.WriteLine("Uw ouderbijdrage voor {0} is {1} euro.", DateTime.Now.Year, prijs);
                Console.WriteLine("Want u heeft {0} kinderen, waarvan {1} onder de 10 en {2} boven de 10.", kinderen, onder10, over10);
                Console.ReadLine();
            }

            //Ik zou eventueel nog de geboortedata in een collection kunnen opslaan.
            //Daarna die sorteren op onder10 en over10, om aan het einde als feedback terug te geven zodat de ouder de input kan controleren.
            //Maar dat is meer dan gevraagd werd. Dus niet nodig?
        }
    }
}
