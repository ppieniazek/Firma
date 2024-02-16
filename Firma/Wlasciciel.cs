using Firma.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Firma;

internal class Wlasciciel : Osoba, IOsoba
{
    public void Menu()
    {
        Console.Clear();
        string menu = $"\tWłaściciel\n1. Zarządzaj pracownikami\n2. Zarządzaj godzinami" +
            "\n3. Zarządzaj urlopami\n4. Zarządzaj zaliczkami\n5. Zarządzaj brygadami\n" +
            "6. Zarządzaj brygadzistami\n7. Sporządź wypłaty\n'q' - wyjście\n";
        Console.Write(menu);
        bool askAgain = true;
        while (askAgain)
        {
            Console.Write("Wybór: ");
            switch (Console.ReadLine())
            {
                case "1":
                    ZarzadzajPracownikami();
                    break;
                case "2":
                    ZarzadzajGodzinami();
                    break;
                case "3":
                    ZarzadzajUrlopami();
                    break;
                case "4":
                    ZarzadzajZaliczkami();
                    break;
                case "5":
                    ZarzadzajBrygadami();
                    break;
                case "6":
                    ZarzadzajBrygadzistami();
                    break;
                case "7":
                    SporzadzWyplaty();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                    continue;
            }
            askAgain = false;
        }
    }

    public void ZarzadzajPracownikami()
    {
        Console.Clear();
        string menu = "Pracownicy:\n1. Dodaj pracownika\n2. Usuń pracownika\n'q' - wyjście\n";
        Console.Write(menu);
        bool askAgain = true;
        while (askAgain)
        {
            Console.Write("Wybór: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajPracownika();
                    break;
                case "2":
                    UsunPracownika();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                    continue;
            }
            askAgain = false;
        }

        void DodajPracownika()
        {
            Console.Clear();
            Console.Write("Podaj imie: ");
            string imie = Console.ReadLine();
            Console.Write("Podaj nazwisko: ");
            string nazwisko = Console.ReadLine();
            Console.Write("Podaj adres: ");
            string adres = Console.ReadLine();
            Console.Write("Podaj kod pocztowy: ");
            string kod_p = Console.ReadLine();
            Console.Write("Podaj stanowisko: ");
            string stanowisko = Console.ReadLine();
            Console.Write("Podaj date urodzenia (YYYY-MM-DD): ");
            var date_u = DateOnly.Parse(Console.ReadLine());
            Console.Write("Podaj date przyjecia (YYYY-MM-DD): ");
            var date_p = DateOnly.Parse(Console.ReadLine());
            Console.Write("Podaj id brygady: ");
            int bid = int.Parse(Console.ReadLine());
            Console.Write("Podaj stawke: ");
            int stawka = int.Parse(Console.ReadLine());

            using (var context = new FirmaContext())
            {
                var pracownik = new Pracownicy 
                { 
                    Imie = imie,
                    Nazwisko = nazwisko,
                    Adres = adres,
                    KodPocztowy = kod_p,
                    Stanowisko = stanowisko,
                    DataUrodzenia = date_u,
                    DataPrzyjecia = date_p,
                    BId = bid,
                    Stawka = stawka
                };
                context.PRacownicy.Add(pracownik);
                context.SaveChanges();
            }

            Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
            Console.ReadKey();
            Menu();
        }

        void UsunPracownika()
        {
            Console.Clear();
            Console.Write("Podaj id pracownika: ");
            int id = int.Parse(Console.ReadLine());
            using (var context = new FirmaContext())
            {
                var rowsToDelete = context.PRacownicy.Where(g => g.PId == id).ToList();
                context.PRacownicy.RemoveRange(rowsToDelete);
                context.SaveChanges();
            }

            Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
            Console.ReadKey();
            Menu();
        }
    }

    public override void ZarzadzajGodzinami()
    {
        base.ZarzadzajGodzinami();
        bool askAgain = true;
        while (askAgain)
        {
            Console.Write("Wybór: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajGodziny();
                    break;
                case "2":
                    UsunGodziny();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                    continue;
            }
            askAgain = false;
        }

        void DodajGodziny()
        {
            bool askAgain = true;
            Console.Clear();
            Console.Write("Chcesz wprowadzić dane dla pojedyńczego pracownika czy dla wszystkich w wybranej brygadzie?:\n" +
                "1. Pojedyńczy pracownik\n2. Cała brygada\n'q' - wyjście\n");
            while (askAgain)
            {
                Console.Write("Wybór: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Podaj id pracownika: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę przepracowanego dnia (YYYY-MM-DD): ");
                        var date = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj liczbę przepracowanych godzin: ");
                        int ilosc = int.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var godzina = new Godziny { PId = id, DataDnia = date, Ilosc = ilosc };
                            context.GOdziny.Add(godzina);
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Podaj id brygady: ");
                        int bid = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę przepracowanego dnia (YYYY-MM-DD): ");
                        var dateB = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj liczbę przepracowanych godzin: ");
                        int iloscB = int.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == bid).ToList();

                            foreach (var employee in employees)
                            {
                                var godzina = new Godziny { PId = employee.PId, DataDnia = dateB, Ilosc = iloscB };
                                context.GOdziny.Add(godzina);
                            }
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                        continue;
                }
                askAgain = false;
            }
        }

        void UsunGodziny()
        {
            bool askAgain = true;
            Console.Clear();
            Console.Write("Chcesz usunąć dane dla pojedyńczego pracownika czy dla wszystkich w wybranej brygadzie?:\n" +
                "1. Pojedyńczy pracownik\n2. Cała brygada\n'q' - wyjście\n");
            while (askAgain)
            {
                Console.Write("Wybór: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Podaj id pracownika: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę przepracowanego dnia (YYYY-MM-DD): ");
                        var date = DateOnly.Parse(Console.ReadLine());
                        using (var context = new FirmaContext())
                        {
                            var rowsToDelete = context.GOdziny.Where(g => g.PId == id && g.DataDnia == date).ToList();
                            context.GOdziny.RemoveRange(rowsToDelete);
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Podaj id brygady: ");
                        int bid = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę przepracowanego dnia (YYYY-MM-DD): ");
                        var dateB = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == bid).ToList();

                            foreach (var employee in employees)
                            {
                                var rowsToDelete = context.GOdziny.Where(g => g.PId == employee.PId && g.DataDnia == dateB).ToList();
                                context.GOdziny.RemoveRange(rowsToDelete);
                            }
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                        continue;
                }
                askAgain = false;
            }
        }
    }

    public override void ZarzadzajUrlopami()
    {
        base.ZarzadzajUrlopami();
        bool askAgain = true;
        while (askAgain)
        {
            Console.Write("Wybór: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajUrlopy();
                    break;
                case "2":
                    UsunUrlopy();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                    continue;
            }
            askAgain = false;
        }

        void DodajUrlopy()
        {
            bool askAgain = true;
            Console.Clear();
            Console.Write("Chcesz wprowadzić dane dla pojedyńczego pracownika czy dla wszystkich w wybranej brygadzie?:\n" +
                "1. Pojedyńczy pracownik\n2. Cała brygada\n'q' - wyjście\n");
            while (askAgain)
            {
                Console.Write("Wybór: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Podaj id pracownika: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę rozpoczęcia (YYYY-MM-DD): ");
                        var date1 = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj datę zakończenia (YYYY-MM-DD): ");
                        var date2 = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var urlop = new Urlopy { PId = id, DataRozpoczecia = date1, DataZakonczenia = date2 };
                            context.URlopy.Add(urlop);
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Podaj id brygady: ");
                        int bid = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę rozpoczęcia (YYYY-MM-DD): ");
                        var date3 = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj datę zakończenia (YYYY-MM-DD): ");
                        var date4 = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == bid).ToList();

                            foreach (var employee in employees)
                            {
                                var urlop = new Urlopy { PId = employee.PId, DataRozpoczecia = date3, DataZakonczenia = date4 };
                                context.URlopy.Add(urlop);
                            }
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                        continue;
                }
                askAgain = false;
            }
        }

        void UsunUrlopy()
        {
            bool askAgain = true;
            Console.Clear();
            Console.Write("Chcesz usunąć dane dla pojedyńczego pracownika czy dla wszystkich w wybranej brygadzie?:\n" +
                "1. Pojedyńczy pracownik\n2. Cała brygada\n'q' - wyjście\n");
            while (askAgain)
            {
                Console.Write("Wybór: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Podaj id pracownika: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę rozpoczęcia (YYYY-MM-DD): ");
                        var date = DateOnly.Parse(Console.ReadLine());
                        using (var context = new FirmaContext())
                        {
                            var rowsToDelete = context.URlopy.Where(g => g.PId == id && g.DataRozpoczecia == date).ToList();
                            context.URlopy.RemoveRange(rowsToDelete);
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Podaj id brygady: ");
                        int bid = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę rozpoczęcia (YYYY-MM-DD): ");
                        var dateB = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == bid).ToList();

                            foreach (var employee in employees)
                            {
                                var rowsToDelete = context.URlopy.Where(g => g.PId == employee.PId && g.DataRozpoczecia == dateB).ToList();
                                context.URlopy.RemoveRange(rowsToDelete);
                            }
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                        continue;
                }
                askAgain = false;
            }
        }
    }

    public override void ZarzadzajZaliczkami()
    {
        base.ZarzadzajZaliczkami();
        bool askAgain = true;
        while (askAgain)
        {
            Console.Write("Wybór: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajZaliczki();
                    break;
                case "2":
                    UsunZaliczki();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                    continue;
            }
            askAgain = false;
        }

        void DodajZaliczki()
        {
            bool askAgain = true;
            Console.Clear();
            Console.Write("Chcesz wprowadzić dane dla pojedyńczego pracownika czy dla wszystkich w wybranej brygadzie?:\n" +
                "1. Pojedyńczy pracownik\n2. Cała brygada\n'q' - wyjście\n");
            while (askAgain)
            {
                Console.Write("Wybór: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Podaj id pracownika: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę zaliczki (YYYY-MM-DD): ");
                        var date1 = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj kwotę: ");
                        var kwota = int.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var zaliczka = new Zaliczki { PId = id, DataZaliczki = date1, Kwota = kwota };
                            context.ZAliczki.Add(zaliczka);
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Podaj id brygady: ");
                        int bid = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę zaliczki (YYYY-MM-DD): ");
                        var date2 = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj kwotę: ");
                        var kwota2 = int.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == bid).ToList();

                            foreach (var employee in employees)
                            {
                                var zaliczka = new Zaliczki { PId = employee.PId, DataZaliczki = date2, Kwota = kwota2 };
                                context.ZAliczki.Add(zaliczka);
                            }
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                        continue;
                }
                askAgain = false;
            }
        }

        void UsunZaliczki()
        {
            bool askAgain = true;
            Console.Clear();
            Console.Write("Chcesz usunąć dane dla pojedyńczego pracownika czy dla wszystkich w wybranej brygadzie?:\n" +
                "1. Pojedyńczy pracownik\n2. Cała brygada\n'q' - wyjście\n");
            while (askAgain)
            {
                Console.Write("Wybór: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Podaj id pracownika: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę zaliczki (YYYY-MM-DD): ");
                        var date = DateOnly.Parse(Console.ReadLine());
                        using (var context = new FirmaContext())
                        {
                            var rowsToDelete = context.ZAliczki.Where(g => g.PId == id && g.DataZaliczki == date).ToList();
                            context.ZAliczki.RemoveRange(rowsToDelete);
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Podaj id brygady: ");
                        int bid = int.Parse(Console.ReadLine());
                        Console.Write("Podaj datę zaliczki (YYYY-MM-DD): ");
                        var dateB = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == bid).ToList();

                            foreach (var employee in employees)
                            {
                                var rowsToDelete = context.ZAliczki.Where(g => g.PId == employee.PId && g.DataZaliczki == dateB).ToList();
                                context.ZAliczki.RemoveRange(rowsToDelete);
                            }
                            context.SaveChanges();
                        }

                        Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
                        Console.ReadKey();
                        Menu();
                        break;
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                        continue;
                }
                askAgain = false;
            }
        }
    }

    public void ZarzadzajBrygadami()
    {
        Console.Clear();
        string menu = "Brygady:\n1. Dodaj brygadę\n2. Usuń brygadę\n'q' - wyjście\n";
        Console.Write(menu);
        bool askAgain = true;
        while (askAgain)
        {
            Console.Write("Wybór: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajBrygade();
                    break;
                case "2":
                    UsunBrygade();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                    continue;
            }
            askAgain = false;
        }

        void DodajBrygade()
        {
            Console.Clear();
            Console.Write("Podaj id pracownika: ");
            int id = int.Parse(Console.ReadLine());

            using (var context = new FirmaContext())
            {
                var brygada = new Brygady { PId = id };
                context.BRygady.Add(brygada);
                context.SaveChanges();
            }

            Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
            Console.ReadKey();
            Menu();
        }

        void UsunBrygade()
        {
            Console.Clear();
            Console.Write("Podaj id brygady: ");
            int id = int.Parse(Console.ReadLine());
            using (var context = new FirmaContext())
            {
                var rowsToDelete = context.BRygady.Where(g => g.BId == id).ToList();
                context.BRygady.RemoveRange(rowsToDelete);
                context.SaveChanges();
            }

            Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
            Console.ReadKey();
            Menu();
        }
    }

    public void ZarzadzajBrygadzistami()
    {
        Console.Clear();
        string menu = "Brygadzisci:\n1. Dodaj brygadzistę\n2. Usuń brygadzistę\n'q' - wyjście\n";
        Console.Write(menu);
        bool askAgain = true;
        while (askAgain)
        {
            Console.Write("Wybór: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajBrygadziste();
                    break;
                case "2":
                    UsunBrygadziste();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowo wybrana opcja. Spróbuj jeszcze raz.\n");
                    continue;
            }
            askAgain = false;
        }

        void DodajBrygadziste()
        {
            Console.Clear();
            Console.Write("Podaj login: ");
            string username = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string passwd = Console.ReadLine();
            Console.Write("Podaj id brygady: ");
            int bid = int.Parse(Console.ReadLine());

            using (var context = new FirmaContext())
            {
                var brygadzista = new Login { Username = username, Passw = passwd, Rola = "B", BId = bid };
                context.Loginy.Add(brygadzista);
                context.SaveChanges();
            }

            Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
            Console.ReadKey();
            Menu();
        }

        void UsunBrygadziste()
        {
            Console.Clear();
            Console.Write("Podaj login: ");
            string username = Console.ReadLine();
            using (var context = new FirmaContext())
            {
                var rowsToDelete = context.Loginy.Where(g => g.Username == username).ToList();
                context.Loginy.RemoveRange(rowsToDelete);
                context.SaveChanges();
            }

            Console.WriteLine("\nOperacja przebiegła pomyślnie. Nastąpi powrót do menu. Wciśnij dowolny klawisz...");
            Console.ReadKey();
            Menu();
        }
    }

    public void SporzadzWyplaty()
    {
        Console.Clear();
        Console.Write("Podaj rok: ");
        int year = int.Parse(Console.ReadLine());
        Console.Write("Podaj miesiąc: ");
        int month = int.Parse(Console.ReadLine());

        using (var context = new FirmaContext())
        {
            var totalHours = context.GOdziny
                .Where(g => g.DataDnia.Year == year && g.DataDnia.Month == month)
                .GroupBy(g => g.PId)
                .Select(g => new { PId = g.Key, TotalHours = g.Sum(x => x.Ilosc) })
                .ToList();

            var advances = context.ZAliczki
                .Where(z => z.DataZaliczki.Year == year && z.DataZaliczki.Month == month)
                .GroupBy(z => z.PId)
                .Select(z => new { PId = z.Key, TotalAdvance = z.Sum(x => x.Kwota) })
                .ToList();

            var salaries = totalHours
                .Join(context.PRacownicy, th => th.PId, p => p.PId, (th, p) => new { th.PId, th.TotalHours, p.Stawka })
                .ToList()
                .ToDictionary(x => x.PId, x => x.TotalHours * x.Stawka);

            foreach (var advance in advances)
            {
                if (salaries.ContainsKey(advance.PId))
                {
                    salaries[advance.PId] -= advance.TotalAdvance;
                }
            }
            Console.WriteLine();
            foreach (var kvp in salaries)
            {
                Console.WriteLine($"Id pracownika: {kvp.Key}, Wypłata: {kvp.Value}");
            }

            Console.WriteLine("\nNastąpi powrót do menu. Naciśnij dowolny klawisz...");
            Console.ReadKey();
            Menu();
        }
    }
}
