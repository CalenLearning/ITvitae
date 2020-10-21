using System;

namespace GroenTaxikosten
{
    class GroenTaxikosten
    {
        static void Main(string[] args)
        {
            /*
            Taxikosten
            Een taxibedrijf hanteert het volgende tarief:
            
            • Per gereden km € 1,=.Daarboven een bedrag per gereden minuut: € 0,25 tussen 8.00 en 18.00
            uur, € 0,45 buiten deze periode.
            
            • Van vrijdagavond 22.00 uur tot maandagochtend 7.00 uur worden de ritprijzen verhoogd met
            een weekendtoeslag van 15 %.Deze toeslag wordt alleen toegepast als de rit begint in de
            genoemde periode.
            
            De te ontwikkelen software moet aan de hand van de ritgegevens de ritprijs berekenen. Je mag er
            vanuit gaan dat een rit op één en dezelfde dag begint en eindigt.
            */

            //1. Voert de chauffeur de ritgegevens handmatig in?
            //2. Voert het apparaat dat de rit bijhoudt de gegevens in?
            //3. Komt de invoer uit een database van ritten?

            //Ik ga maar voor optie 1. Want er zijn geen gegeven variabelen van de uitvoer van een apparaat.
            //En optie 1 is beter te testen qua variaties. Anders wordt het DateTime.Now - begintijd, etc.
            //Ritgegevens is meervoud, maar ik neem aan dat de ritprijs hier relevant is aan het einde van de rit.
            //Ik neem aan dat de bedoeling is om te tonen dat ik het verhaal in code kan omzetten. Dus een GUI lijkt me overdreven bij de groene opdrachten?

            while (true)
            {
                Invoer();
            }
        }
        //Had een vage error "Use of unassigned local variable 'dag'". Zie comment rond regel 65.
        //Maar dat lijkt in scope te zijn..?

        public static int dag;

        public static void Invoer()
        {
            Console.Clear();

            //Vaststaande beginwaardes
            decimal kmPrijs = 1;
            decimal dalPrijs = 0.25M;
            decimal piekPrijs = 0.45M;
            decimal weekendToeslag = 0.15M;

            decimal ritPrijs = 0M;

            //Invoer van de ritdag
            Console.WriteLine("Op welke dag was de rit? Kies het gewenste nummer.\n" +
                "1. Maandag\n" +
                "2. Dinsdag\n" +
                "3. Woensdag\n" +
                "4. Donderdag\n" +
                "5. Vrijdag\n" +
                "6. Zaterdag\n" +
                "7. Zondag");
            string invoerDag = Console.ReadLine();
            //int dag; 
            try
            {
                dag = Convert.ToInt32(invoerDag);
            }
            catch (Exception)
            {
                Console.WriteLine("Ongeldige invoer. Kies een nummer tussen 1 en 7.\n" +
                    "Druk op Enter om opnieuw te beginnen.");
                Invoer();
            }
            if (dag >= 8 || dag < 1)
            {
                Console.WriteLine("Ongeldige invoer. Kies een nummer tussen 1 en 7.\n" +
                    "Druk op Enter om opnieuw te beginnen.");
                Console.ReadLine();
                Invoer();
            }

            //Begintijd
            Console.WriteLine("Hoe laat begon de rit? Schrijf dit als UU:MM. Bijvoorbeeld 9:10 voor 9:10 uur.");
            string invoerBeginTijd = Console.ReadLine();
            DateTime beginTijd = new DateTime();
            try
            {
                beginTijd = Convert.ToDateTime(invoerBeginTijd);
            }
            catch (Exception)
            {
                Console.WriteLine("Ongeldige invoer. Schrijf de begintijd als UU:MM. Bijvoorbeeld 9:10 voor 9:10 uur.\n" +
                    "Druk op Enter om opnieuw te beginnen.");
                Console.Read();
                Invoer();
            }

            //Eindtijd
            Console.WriteLine("Hoe laat eindigde de rit? Schrijf dit als UU:MM. Bijvoorbeeld 12:00 voor 12:00 uur.");
            string invoerEindTijd = Console.ReadLine();
            DateTime eindTijd = new DateTime();
            try
            {
                eindTijd = Convert.ToDateTime(invoerEindTijd);
            }
            catch (Exception)
            {
                Console.WriteLine("Ongeldige invoer. Schrijf de begintijd als UU:MM. Bijvoorbeeld 12:00 voor 12:00 uur.\n" +
                    "Druk op Enter om opnieuw te beginnen.");
                Console.Read();
                Invoer();
            }
            if (beginTijd > eindTijd)
            {
                Console.WriteLine("Ongeldige invoer. De begintijd kan niet na de eindtijd zijn.\n" +
                    "Druk op Enter om opnieuw te beginnen.");
                Console.Read();
                Invoer();
            }

            //Tijdsduur
            TimeSpan tijdsduur = new TimeSpan();
            tijdsduur = eindTijd - beginTijd;
            decimal tijdsduurInMin;
            tijdsduurInMin = Convert.ToDecimal(tijdsduur.TotalMinutes);
            
            //Afstand
            Console.WriteLine("Hoeveel kilometer is er gereden?");
            string invoerAfstand = Console.ReadLine();
            decimal afstand;
            try
            {
                afstand = Convert.ToDecimal(invoerAfstand);
            }
            catch (Exception)
            {
                Console.WriteLine("");
                return;
            }

            //Is de tijd >= 08:01 && < 18:00, else & (hoe letterlijk is "tussen"?)
            //Technisch gezien is kmPrijs overbodig, want dat is 1. Maar indien dat ooit verandert, etc.
            if ((beginTijd.Hour >= 8 && beginTijd.Minute > 0) && beginTijd.Hour < 18)
            {
                ritPrijs = (tijdsduurInMin * dalPrijs) + (afstand * kmPrijs);
            }
            else
            {
                ritPrijs = (tijdsduurInMin * piekPrijs) + (afstand * kmPrijs);
            }

            //Weekendtoeslag
            if ((dag == 5 && beginTijd.Hour >= 22 && beginTijd.Minute > 0) ||
                (dag == 6) ||
                (dag == 7) ||
                (dag == 1 && beginTijd.Hour < 7))
            {
                ritPrijs = ritPrijs * (1 + weekendToeslag);
            }

            //Ik krijg het €-teken niet geprint. Ook niet met CultureInfo.
            Console.WriteLine("Deze rit kost {0:C} euro.", ritPrijs);
            Console.ReadLine();
        }
    }
}