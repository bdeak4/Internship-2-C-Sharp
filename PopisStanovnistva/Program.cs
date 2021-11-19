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
                        DateTime bday = PitajDatum("Unesite datum rođenja (dd. mm. yyyy.): ");
                        var results = popis
                                .Where(i => i.Value.nameAndSurname.ToLower() == (ime + " " + prezime) &&
                                            i.Value.dateOfBirth == bday)
                                .ToDictionary(i => i.Key, i => i.Value);
                        if (results.Count > 0) IspisStanovnika(results);
                        else Console.WriteLine("Nema stanovnika koji zadovoljavaju kriterije.");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 4:
                        string novi_oib = PitajOIB("Unesite OIB novog stanovnika: ");
                        string novo_ime = PitajStr("Unesite ime novog stanovnika: ").Trim();
                        string novo_prezime = PitajStr("Unesite prezime novog stanovnika: ").Trim();
                        DateTime novi_bday = PitajDatum("Unesite datum rođenja novog stanovnika (dd. mm. yyyy.): ");
                        if (!popis.ContainsKey(novi_oib))
                        {
                            popis.Add(novi_oib, (novo_ime + " " + novo_prezime, novi_bday));
                            Console.WriteLine("Stanovnik uspjesno unesen.");
                            IspisStanovnika(popis
                                .Where(i => i.Key == novi_oib)
                                .ToDictionary(i => i.Key, i => i.Value));
                        }
                        else Console.WriteLine("Stanovnik sa unesenim OIBom vec postoji.");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 5:
                        string brisanje_oib = PitajOIB("Unesite OIB stanovnika: ");
                        if (popis.ContainsKey(brisanje_oib))
                        {
                            if (PitajBool("Jeste li sigurni da zelite obrisati stanovnika (da/ne): "))
                            {
                                popis.Remove(brisanje_oib);
                                Console.WriteLine("Stanovnik uspjesno obrisan.");
                            }
                        }
                        else Console.WriteLine("Stanovnik sa unesenim OIBom ne postoji.");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 6:
                        string brisanje_ime = PitajStr("Unesite ime stanovnika: ").Trim().ToLower();
                        string brisanje_prezime = PitajStr("Unesite prezime stanovnika: ").Trim().ToLower();
                        DateTime brisanje_bday = PitajDatum("Unesite datum rođenja (dd. mm. yyyy.): ");
                        var brisanje_results = popis
                                .Where(i => i.Value.nameAndSurname.ToLower() == (brisanje_ime + " " + brisanje_prezime) &&
                                            i.Value.dateOfBirth == brisanje_bday)
                                .ToDictionary(i => i.Key, i => i.Value);
                        if (brisanje_results.Count == 1)
                        {
                            if (PitajBool("Jeste li sigurni da zelite obrisati stanovnika (da/ne): "))
                            {
                                popis.Remove(brisanje_results.ToList()[0].Key);
                                Console.WriteLine("Stanovnik uspjesno obrisan.");
                            }
                        }
                        else if (brisanje_results.Count > 1)
                        {
                            IspisStanovnika(brisanje_results);
                            string brisanje_results_oib = PitajOIB("Unesite OIB stanovnika: ");
                            if (popis.ContainsKey(brisanje_results_oib))
                            {
                                if (PitajBool("Jeste li sigurni da zelite obrisati stanovnika (da/ne): "))
                                {
                                    popis.Remove(brisanje_results_oib);
                                    Console.WriteLine("Stanovnik uspjesno obrisan.");
                                }
                            }
                            else Console.WriteLine("Stanovnik sa unesenim OIBom ne postoji.");
                        }
                        else Console.WriteLine("Nema stanovnika koji zadovoljavaju kriterije.");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 7:
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (PitajBool("Jeste li sigurni da zelite obrisati SVE stanovnike (da/ne): "))
                        {
                            Console.ResetColor();
                            popis.Clear();
                            Console.WriteLine("Svi stanovnici uspjesno obrisani.");
                        }
                        Console.ResetColor();
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
                        string edit_oib = PitajOIB("Unesite OIB stanovnika: ");
                        if (popis.ContainsKey(edit_oib))
                        {
                            string edit_novi_oib = PitajOIB("Unesite novi OIB stanovnika: ");
                            if (!popis.ContainsKey(edit_novi_oib))
                            {
                                if (PitajBool("Jeste li sigurni da zelite promjeniti OIB stanovnika (da/ne): "))
                                {
                                    var s = popis[edit_oib];
                                    popis.Remove(edit_oib);
                                    popis[edit_novi_oib] = s;
                                    Console.WriteLine("OIB stanovnika uspjesno izmjenjen.");
                                }
                            } 
                            else Console.WriteLine("Stanovnik sa unesenim OIBom vec postoji.");
                        }
                        else Console.WriteLine("Stanovnik sa unesenim OIBom ne postoji.");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 82:
                        string name_edit_oib = PitajOIB("Unesite OIB stanovnika: ");
                        if (popis.ContainsKey(name_edit_oib))
                        {
                            string edit_novo_ime = PitajStr("Unesite novo ime stanovnika: ").Trim();
                            string edit_novo_prezime = PitajStr("Unesite novo prezime stanovnika: ").Trim();
                            if (PitajBool("Jeste li sigurni da zelite promjeniti ime i prezime stanovnika (da/ne): "))
                            {
                                popis[name_edit_oib] = (edit_novo_ime + " " + edit_novo_prezime, popis[name_edit_oib].dateOfBirth);
                                Console.WriteLine("Ime i prezime stanovnika uspjesno izmjenjeno.");
                            }
                        }
                        else Console.WriteLine("Stanovnik sa unesenim OIBom ne postoji.");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 83:
                        string bday_edit_oib = PitajOIB("Unesite OIB stanovnika: ");
                        if (popis.ContainsKey(bday_edit_oib))
                        {
                            DateTime edit_novi_bday = PitajDatum("Unesite novi datum rođenja (dd. mm. yyyy.): ");
                            if (PitajBool("Jeste li sigurni da zelite promjeniti datum rođenja stanovnika (da/ne): "))
                            {
                                popis[bday_edit_oib] = (popis[bday_edit_oib].nameAndSurname, edit_novi_bday);
                                Console.WriteLine("Datum rođenja stanovnika uspjesno izmjenjen.");
                            }
                        }
                        else Console.WriteLine("Stanovnik sa unesenim OIBom ne postoji.");
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
                        int broj_zaposlenih = popis.Where(i => Zaposlenost(i.Value.dateOfBirth)).ToList().Count();
                        int postotak_zaposlenih = (int)((float)broj_zaposlenih / popis.Count() * 100);
                        Console.WriteLine("Postotak zaposlenih: " + postotak_zaposlenih + "%");
                        Console.WriteLine("Postotak nezaposlenih: " + (100-postotak_zaposlenih) + "%");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 92:
                        var name_count = new Dictionary<string, int> { };
                        foreach (var stanovnik in popis)
                        {
                            string ime_stanovnika = stanovnik.Value.nameAndSurname.Split(" ")[0];
                            if (name_count.ContainsKey(ime_stanovnika)) name_count[ime_stanovnika] += 1;
                            else name_count[ime_stanovnika] = 1;
                        }
                        var common_name = name_count.OrderByDescending(x => x.Value).ToList()[0];
                        Console.WriteLine("Najcesce ime \""+common_name.Key+"\" ponavlja se "+common_name.Value+" puta");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 93:
                        var surname_count = new Dictionary<string, int> { };
                        foreach (var stanovnik in popis)
                        {
                            string prezime_stanovnika = stanovnik.Value.nameAndSurname.Split(" ")[1];
                            if (surname_count.ContainsKey(prezime_stanovnika)) surname_count[prezime_stanovnika] += 1;
                            else surname_count[prezime_stanovnika] = 1;
                        }
                        var common_surname = surname_count.OrderByDescending(x => x.Value).ToList()[0];
                        Console.WriteLine("Najcesce prezime \"" + common_surname.Key + "\" ponavlja se " + common_surname.Value + " puta");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 94:
                        var bday_count = new Dictionary<DateTime, int> { };
                        foreach (var stanovnik in popis)
                        {
                            DateTime bday_stanovnika = stanovnik.Value.dateOfBirth;
                            if (bday_count.ContainsKey(bday_stanovnika)) bday_count[bday_stanovnika] += 1;
                            else bday_count[bday_stanovnika] = 1;
                        }
                        var common_bday = bday_count.OrderByDescending(x => x.Value).ToList()[0];
                        Console.WriteLine("Najcesci datum rođenja \"" + common_bday.Key.ToString("dd. MM. yyyy.") + "\" ponavlja se " + common_bday.Value + " puta");
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 95:
                        var season_count = new Dictionary<string, int>
                        {
                            {"proljece", 0},
                            {"ljeto", 0},
                            {"jesen", 0},
                            {"zima", 0},
                        };
                        foreach (var stanovnik in popis)
                        {
                            float d = stanovnik.Value.dateOfBirth.Month + ((float)stanovnik.Value.dateOfBirth.Day / 100);
                            if (d < 3.21 || d > 12.21) season_count["zima"] += 1;
                            else if (d < 6.21) season_count["proljece"] += 1;
                            else if (d < 9.23) season_count["ljeto"] += 1;
                            else season_count["jesen"] += 1;
                        }
                        foreach (var season in season_count.OrderByDescending(x => x.Value).ToList())
                        {
                            Console.WriteLine("Godisnje doba: " + season.Key + ", rođeno " + season.Value + " stanovnika");
                        }
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 96:
                        var najmladi_oib = popis.OrderByDescending(x => x.Value.dateOfBirth).ToList()[0].Key;
                        IspisStanovnika(popis
                                .Where(i => i.Key == najmladi_oib)
                                .ToDictionary(i => i.Key, i => i.Value));
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 97:
                        var najstariji_oib = popis.OrderBy(x => x.Value.dateOfBirth).ToList()[0].Key;
                        IspisStanovnika(popis
                                .Where(i => i.Key == najstariji_oib)
                                .ToDictionary(i => i.Key, i => i.Value));
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 98:
                        int sum = 0;
                        foreach(var stanovnik in popis)
                        {
                            int starost = DateTime.Now.Year - stanovnik.Value.dateOfBirth.Year;
                            if (DateTime.Now < stanovnik.Value.dateOfBirth.AddYears(starost)) starost -= 1;
                            sum += starost;
                        }
                        Console.WriteLine("Prosječan broj godina: " + ((float)sum / popis.Count()).ToString("0.00"));
                        izbornik = Odabir(Array.Empty<string>(), izbornik);
                        break;
                    case 99:
                        var starosti = new List<int> { };
                        foreach (var stanovnik in popis)
                        {
                            int starost = DateTime.Now.Year - stanovnik.Value.dateOfBirth.Year;
                            if (DateTime.Now < stanovnik.Value.dateOfBirth.AddYears(starost)) starost -= 1;
                            starosti.Add(starost);
                        }
                        starosti.Sort();
                        Console.WriteLine("Medijan godina: " + starosti[starosti.Count / 2]);
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
                        popis = popis
                                .OrderBy(x => x.Value.nameAndSurname.Split(" ")[1])
                                .ToDictionary(x => x.Key, x => x.Value);
                        Console.WriteLine("Popis uspjesno sortiran");
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
            if (DateTime.Now < date.AddYears(starost)) starost -= 1;
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

        static bool PitajBool(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (input == "da" || input == "y" || input == "yes") return true;
            return false;
        }


    }
}
