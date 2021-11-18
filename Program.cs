using System;

namespace PopisStanovnistva
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Izbornik(1));
        }

        static int Izbornik(int sifra)
        {
            switch (sifra)
            {
                case 0:
                    Console.WriteLine("Akcije:");
                    Console.WriteLine("1 - Ispis stanovništva");
                    Console.WriteLine("2 - Ispis stanovnika po OIB-u");
                    Console.WriteLine("3 - Ispis OIB-a po unosu imena i prezimena te datuma rođenja");
                    Console.WriteLine("4 - Unos novog stanovnika");
                    Console.WriteLine("5 - Brisanje stanovnika unosom OIB-a");
                    Console.WriteLine("6 - Birsanje stanovnika po imenu i prezimenu te datumu rođenja");
                    Console.WriteLine("7 - Brisanje svih stanovnika");
                    Console.WriteLine("8 - Uređivanje stanovnika");
                    Console.WriteLine("9 - Statistika");
                    Console.WriteLine("10 - Sortiranje stanovnika");
                    Console.WriteLine("0 - Izlaz iz aplikacije");

                    Console.Write("Odaberite akciju: ");
                    int odabir = int.Parse(Console.ReadLine());
                    while (odabir < 0 || odabir > 10)
                    {
                        Console.WriteLine("Akcija mora biti jedan od brojeva u listi.");
                        Console.Write("Odaberite akciju: ");
                        odabir = int.Parse(Console.ReadLine());
                    }
                    return odabir;
            }
            return -1;
        }
    }
}
