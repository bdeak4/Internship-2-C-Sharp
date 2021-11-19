using System;
using System.Collections.Generic;
using System.Linq;

namespace PopisStanovnistva
{
    class Program
    {
        static void Main(string[] args)
        {
            var popis = new Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)>
            {
                { "52661241828", ("Ante Antic", new DateTime(2002, 02, 02)) },
                { "52661241829", ("Ante Antic", new DateTime(2002, 02, 02)) },
                { "01588425197", ("Marko Markic", new DateTime(2002, 02, 02)) },
                { "47074601429", ("Ana Anic", new DateTime(2002, 02, 03)) },
                { "69053261356", ("Marija Maric", new DateTime(1995, 05, 06)) },
                { "20296932573", ("Ante Neantic", new DateTime(2003, 04, 05)) },
                { "70490225881", ("Kazimir Kazimirovic", new DateTime(1990, 11, 07)) },
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
                        IspisStanovnika(popis);
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 12:
                        IspisStanovnika(popis
                                .OrderBy(x => x.Value.dateOfBirth.ToString("yyyy-MM-dd"))
                                .ToDictionary(x => x.Key, x => x.Value));
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 13:
                        IspisStanovnika(popis
                            .OrderByDescending(x => x.Value.dateOfBirth.ToString("yyyy-MM-dd"))
                            .ToDictionary(x => x.Key, x => x.Value));
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 2:
                        string oib = PitajOIB("Unesite OIB stanovnika: ");
                        if (popis.ContainsKey(oib))
                            IspisStanovnika(popis
                                .Where(i => i.Key == oib)
                                .ToDictionary(i => i.Key, i => i.Value));
                        else
                            Console.WriteLine("Stanovnik sa unesenim OIBom nije pronaden.");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 3:
                        string ime = PitajStr("Unesite ime stanovnika: ").Trim().ToLower();
                        string prezime = PitajStr("Unesite prezime stanovnika: ").Trim().ToLower();
                        DateTime bday = PitajDatum("Unesite datum rođenja: ");
                        var results = popis
                                .Where(i => i.Value.nameAndSurname.ToLower() == (ime + " " + prezime) &&
                                            i.Value.dateOfBirth == bday)
                                .ToDictionary(i => i.Key, i => i.Value);
                        if (results.Count > 0) IspisStanovnika(results);
                        else Console.WriteLine("Nema stanovnika koji zadovoljavaju kriterije.");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 4:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 5:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 6:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 7:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 8:
                        izbornik = Odabir(new string[] {
                            "Uredi OIB stanovnika",
                            "Uredi ime i prezime stanovnika",
                            "Uredi datum rođenja",
                        }, izbornik);
                        break;
                    case 81:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 82:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 83:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 9:
                        izbornik = Odabir(new string[] {
                            "Postotak nezaposlenih (od 0 do 23 godine i od 65 do 100 godine) i postotak zaposlenih (od 23 do 65 godine)",
                            "Ispis najčešćeg imena i koliko ga stanovnika ima",
                            "Ispis najčešćeg prezimena i koliko ga stanovnika ima",
                            "Ispis datum na koji je rođen najveći broj ljudi i koji je to datum",
                            "Ispis broja ljudi rođenih u svakom od godišnjih doba",
                            "Ispis najmlađeg stanovnika",
                            "Ispis najstarijeg stanovnika",
                            "Prosječan broj godina",
                            "Medijan godina",
                        }, izbornik);
                        break;
                    case 91:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 92:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 93:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 94:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 95:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 96:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 97:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 98:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 99:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 10:
                        izbornik = Odabir(new string[] {
                            "Abecedno po prezimenima",
                            "Po datumu rođenja",
                            "Silazno",
                        }, izbornik);
                        break;
                    case 101:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 102:
                        Console.WriteLine("TODO");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 103:
                        Console.WriteLine("TODO");
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
            bool success = int.TryParse(Console.ReadLine(), out int odabir);
            while (odabir < 0 || odabir > akcije.Length || !success)
            {
                Console.WriteLine("Akcija mora biti jedan od brojeva u listi.");
                Console.Write("Odaberite akciju: ");
                success = int.TryParse(Console.ReadLine(), out odabir);
            }

            if (prosli_odabir > 0 && odabir != 0)
                odabir += prosli_odabir * 10;

            return odabir;
        }

        static void IspisStanovnika(Dictionary<string, (string nameAndSurname, DateTime dateOfBirth)>  popis)
        {
            Console.WriteLine(
                "OIB".PadRight(11) + " | " +
                "Ime i prezime".PadRight(30) +
                " | Datum rođenja"
            );
            Console.WriteLine("----------- | ------------------------------ | -------------");
            foreach(var stanovnik in popis)
            {
                Console.ForegroundColor = Zaposlenost(stanovnik.Value.dateOfBirth) ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(
                    stanovnik.Key.PadRight(11) + " | " +
                    stanovnik.Value.nameAndSurname.PadRight(30) +
                    " | " + stanovnik.Value.dateOfBirth.ToString("dd. MM. yyyy.")
                );
            }
            Console.ResetColor();
        }

        static bool Zaposlenost(DateTime date)
        {
            int starost = DateTime.Now.Year - date.Year;
            if (DateTime.Now < date.AddYears(starost))
                starost -= 1;
            return (starost > 23 && starost < 65);
        }

        static string PitajOIB(string prompt)
        {
            Console.Write(prompt);
            string oib = Console.ReadLine();
            while (!IsDigitsOnly(oib) || oib.Length != 11)
            {
                Console.WriteLine("OIB mora sadrzavati samo brojeve i imati 11 znamenki.");
                Console.Write(prompt);
                oib = Console.ReadLine();
            }
            return oib;
        }

        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        static string PitajStr(string prompt)
        {
            Console.Write(prompt);
            string str = Console.ReadLine();
            while (str.Length == 0)
            {
                Console.WriteLine("Unos ne smije biti prazan.");
                Console.Write(prompt);
                str = Console.ReadLine();
            }
            return str;
        }

        static DateTime PitajDatum(string prompt)
        {
            Console.Write(prompt);
            bool success = DateTime.TryParseExact(
                Console.ReadLine(),
                "dd. MM. yyyy.",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime datum);
            while (!success)
            {
                Console.WriteLine("Datum mora biti u formatu \"dd. mm. gggg.\".");
                Console.Write(prompt);
                success = DateTime.TryParse(Console.ReadLine(), out datum);
            }
            return datum;
        }

    }
}
