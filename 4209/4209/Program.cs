using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4209
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Promillerechner rechner = new Promillerechner();
            rechner.BerechnePromille();
        }
    }

    public class Person
    {
        public string Geschlecht { get; private set; }
        public double Gewicht { get; private set; }
        public double Groesse { get; private set; }
        public int Alter { get; private set; }
        private double _promille = 0.0;

        public Person(string geschlecht, double gewicht, double groesse, int alter)
        {
            Geschlecht = geschlecht;
            Gewicht = gewicht;
            Groesse = groesse;
            Alter = alter;
        }

        public double Gesamtkörperwasser()
        {
            if (Geschlecht.ToLower() == "männlich")
            {
                return 2.447 - 0.09516 * Alter + 0.1074 * Groesse + 0.3362 * Gewicht;
            }
            else // Weiblich
            {
                return 0.203 - 0.07 * Alter + 0.1069 * Groesse + 0.2466 * Gewicht;
            }
        }

        public void Trinke(AlkoholischesGetraenk getraenk)
        {
            double alkoholMasse = getraenk.Volumen * getraenk.Alkoholgehalt * 0.8;
            double gkw = Gesamtkörperwasser();
            _promille += (0.8 * alkoholMasse) / (1.055 * gkw);
        }

        public double GetPromille()
        {
            return _promille;
        }
    }

    public class AlkoholischesGetraenk
    {
        public double Volumen { get; private set; }
        public double Alkoholgehalt { get; private set; } // Alkoholgehalt in Prozent

        public AlkoholischesGetraenk(double volumen, double alkoholgehalt)
        {
            Volumen = volumen;
            Alkoholgehalt = alkoholgehalt;
        }
    }

    public class Spruch
    {
        public string GeneriereSpruch(double promille)
        {
            if (promille < 0.3)
            {
                return "Du bist noch nüchtern, weiter so!";
            }
            else if (promille < 0.8)
            {
                return "Langsam solltest du aufhören zu trinken.";
            }
            else if (promille < 1.5)
            {
                return "Du solltest das Auto lieber stehen lassen.";
            }
            else
            {
                return "Das wird teuer, falls du erwischt wirst!";
            }
        }
    }


    public class Promillerechner
    {
        public void BerechnePromille()
        {
            // Personendaten erfassen
            Console.WriteLine("Geben Sie Ihr Geschlecht ein (männlich/weiblich): ");
            string geschlecht = Console.ReadLine();

            Console.WriteLine("Geben Sie Ihr Gewicht in kg ein: ");
            double gewicht = double.Parse(Console.ReadLine());

            Console.WriteLine("Geben Sie Ihre Größe in cm ein: ");
            double groesse = double.Parse(Console.ReadLine());

            Console.WriteLine("Geben Sie Ihr Alter ein: ");
            int alter = int.Parse(Console.ReadLine());

            Person person = new Person(geschlecht, gewicht, groesse, alter);

            // Getränkedaten erfassen
            Console.WriteLine("Geben Sie das Volumen des Getränks in ml ein: ");
            double volumen = double.Parse(Console.ReadLine());

            Console.WriteLine("Geben Sie den Alkoholgehalt in Prozent ein (z.B. 5 für Bier): ");
            double alkoholgehalt = double.Parse(Console.ReadLine()) / 100.0;

            AlkoholischesGetraenk getraenk = new AlkoholischesGetraenk(volumen, alkoholgehalt);

            // Promillegehalt berechnen
            person.Trinke(getraenk);
            double promille = person.GetPromille();

            // Spruch generieren
            Spruch spruch = new Spruch();
            string ausgabeSpruch = spruch.GeneriereSpruch(promille);

            // Ergebnisse ausgeben
            Console.WriteLine($"Ihr aktueller Promillewert beträgt: {promille:F4}");
            Console.WriteLine(ausgabeSpruch);
        }
    }


}
