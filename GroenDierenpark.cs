using System;

namespace GroenDierenpark
{
    class GroenDierenpark
    {
        static void Main(string[] args)
        {
            /*
            Dierenpark
            Het Noorder Dierenpark in Emmen hanteert onder andere de volgende tarieven voor abonnementen:
            • Echtpaar € 61,=
            • Gezin met 1 kind € 71,=
            • Gezin met 2 kinderen € 82,=
            • Persoonlijk € 30,=
            • Elk kind extra € 11,=
            • Echtpaar 65 + € 65,=
            • Persoonlijk 65 + € 26,=
            De te ontwikkelen software moet aan de hand van de gegevens van de abonnementaanvrager de
            abonnementsprijs bepalen.
            De leeftijd van de volwassene(n) moet aan de hand van geboortedatum en een peildatum worden
            berekend.

            Wat gebeurt er met een gezin met 1 of meer kinderen ouder dan 18?
            Geen 1-oudergezinnen ook? Kan hier wel ingevoerd worden als 1 volwassene met 11 euro per kind.

            Een WPF zou wellicht praktischer zijn.
            Wellicht ook handiger om bij foutieve invoer bij dezelfde vraag verder te kunnen. In plaats
            van compleet opnieuw. Wellicht door de vragen in if-statements te zetten -> if !(waarde == 0). 
            */

            Vragen();
        }
        private static void Vragen()
        {
            string invoer;
            int aantalTotaal;
            int aantalVolwassenen;
            int aantalVolwassenenBoven65 = 0;
            int aantalKinderen;
            DateTime peilDatumBoven65 = new DateTime(2020 - 65, 1, 1);

            Console.WriteLine("Voor hoeveel mensen wilt u een abonnement?");
            invoer = Console.ReadLine();
            aantalTotaal = NaarInt(invoer);

            Console.WriteLine("Hoeveel volwassenen zijn er?");
            invoer = Console.ReadLine();
            aantalVolwassenen = NaarInt(invoer);
            aantalKinderen = aantalTotaal - aantalVolwassenen;
            Console.WriteLine("Dus u heeft {0} kinderen.", aantalKinderen);

            Console.WriteLine("Wat is uw geboortedatum? Voer dit bijvoorbeeld in als 1980, 12, 30.");
            invoer = Console.ReadLine();
            DateTime gebDatumAanvrager = NaarDateTime(invoer);
            //Eerst geprobeerd dit in een method te zetten. Maar ik kwam er niet uit waar ik dan de counter
            //moest laten. Kon een private static bool doen. Maar dan komt de counter hier in de if-statement.
            //Dus daar schiet ik weinig mee op?
            if (gebDatumAanvrager < peilDatumBoven65)
            {
                aantalVolwassenenBoven65++;
            }

            //Zodat het enkel gevraagd word als er twee volwassenen zijn.
            if (aantalVolwassenen == 2)
            {
                Console.WriteLine("Wat is de geboortedatum van uw partner? Voer dit bijvoorbeeld in als 1980, 12, 30.");
                invoer = Console.ReadLine();
                DateTime gebDatumPartner = NaarDateTime(invoer);
                if (gebDatumPartner < peilDatumBoven65)
                {
                    aantalVolwassenenBoven65++;
                }
            }

            //Check van geboortedata kinderen was niet nodig?
            //Of neem ik "kind" te letterlijk?

            //Wiskundig komt er het juiste uit. Zo kan ik het in zo min mogelijk regels schrijven.
            int prijs = 0;

            if (aantalVolwassenen > aantalVolwassenenBoven65)
            {
                prijs += (aantalVolwassenen * 30);
                if (aantalVolwassenen == 2 && aantalKinderen == 0)
                {
                    prijs += 1;
                }
                if (aantalKinderen > 0)
                {
                    prijs += (aantalKinderen * 11);
                }
            }
            else if (aantalVolwassenenBoven65 == 2)
            {
                prijs = 65;
            }
            else if (aantalVolwassenenBoven65 == 1 && aantalVolwassenen == 1)
            {
                prijs = 26;
            }
            Console.WriteLine("Uw abonnementskosten zijn {0:C} euro", prijs);
        }
        private static int NaarInt(string input)
        {
            int getal = 0;
            try
            {
                getal = Convert.ToInt32(input);
            }
            catch (Exception)
            {
                Console.WriteLine("Ongeldige invoer. Schrijf een getal in cijfers.");
                Console.ReadLine();
                Console.Clear();
                Vragen();
            }
            return getal;
        }
        private static DateTime NaarDateTime(string input)
        {
            DateTime datum = new DateTime(5000, 1, 1);
            try
            {
                datum = Convert.ToDateTime(input);
            }
            catch (Exception)
            {
                Console.WriteLine("Ongeldige invoer. Schrijf de datum bijvoorbeeld als 1980, 12, 30.");
                Console.ReadLine();
                Console.Clear();
                Vragen();
            }
            return datum;
        }
    }
}