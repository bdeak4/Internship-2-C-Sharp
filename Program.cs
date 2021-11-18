using System;
using System.Collections.Generic;

namespace PopisStanovnistva
{
    class Program
    {
        static void Main(string[] args)
        {
            var popis = new Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)>
            {
                { "52661241828", ("Ante Antic", new DateTime(2002, 01, 01)) },
                { "01588425197", ("Marko Markic", new DateTime(2002, 02, 02)) },
                { "47074601429", ("Ana Anic", new DateTime(2001, 03, 03)) },
                { "69053261356", ("Marija Maric", new DateTime(1999, 05, 06)) },
                { "20296932573", ("Ante Neantic", new DateTime(2003, 04, 05)) },
                { "70490225881", ("Kazimir Kazimirovic", new DateTime(1945, 11, 07)) },
            };
            int izbornik = 0;
            bool izlaz = false;
            while (!izlaz) {
                Console.Clear();
                switch (izbornik)
                {
                    case 0:
                        izbornik = Odabir(new string[] {
                            "Ispis stanovništva",
                            "Ispis stanovnika po OIB-u",
                            "Ispis OIB-a po unosu imena i prezimena te datuma rođenja",
                            "Unos novog stanovnika",
                            "Brisanje stanovnika unosom OIB-a",
                            "Birsanje stanovnika po imenu i prezimenu te datumu rođenja",
                            "Brisanje svih stanovnika",
                            "Uređivanje stanovnika",
                            "Statistika",
                            "Sortiranje stanovnika",
                        }, izbornik);

                        if (izbornik == 0)
                        {
                            izlaz = true;
                            Console.WriteLine("Izlaz iz aplikacije");
                        }
                        break;
                    case 1:
                        izbornik = Odabir(new string[] {
                            "Onako kako su spremljeni",
                            "Po datumu rođenja uzlazno",
                            "Po datumu rođenja silazno",
                        }, izbornik);
                        break;
                    case 11:
                        Console.WriteLine(popis);
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                }
            }
        }

        static int Odabir(string[] akcije, int prosli_odabir)
        {
            Console.WriteLine("Akcije:");
            for (var i = 0; i < akcije.Length; i++)
                Console.WriteLine(i+1 + " - " + akcije[i]);

            if (prosli_odabir == 0) Console.WriteLine("0 - Izlaz iz aplikacije");
            else Console.WriteLine("0 - Povratak u glavni izbornik");

            Console.Write("Odaberite akciju: ");
            int odabir = int.Parse(Console.ReadLine());
            while (odabir < 0 || odabir > akcije.Length)
            {
                Console.WriteLine("Akcija mora biti jedan od brojeva u listi.");
                Console.Write("Odaberite akciju: ");
                odabir = int.Parse(Console.ReadLine());
            }

            if (prosli_odabir > 0 && odabir != 0)
                odabir += prosli_odabir * 10;

            return odabir;
        }
    }
}
