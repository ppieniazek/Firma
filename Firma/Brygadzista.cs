using Firma.Modele;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma;

internal class Brygadzista(int? bid) : Osoba, IOsoba
{
    public int? BId { get; set; } = bid;

    public void Menu()
    {
        Console.Clear();
        string menu = $"\tBrygadzista (b_id = {BId})\n1. Zarządzaj godzinami" +
            "\n2. Zarządzaj urlopami\n3. Zarządzaj zaliczkami\n'q' - wyjście\n";
        Console.Write(menu);
        bool askAgain = true;
        while (askAgain)
        {
            Console.Write("Wybór: ");
            switch (Console.ReadLine())
            {
                case "1":
                    ZarzadzajGodzinami();
                    break;
                case "2":
                    ZarzadzajUrlopami();
                    break;
                case "3":
                    ZarzadzajZaliczkami();
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
            Console.Write("Chcesz wprowadzić dane dla pojedyńczego pracownika czy dla wszystkich w Twojej brygadzie?:\n" +
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
                        Console.Write("Podaj datę przepracowanego dnia (YYYY-MM-DD): ");
                        var dateB = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj liczbę przepracowanych godzin: ");
                        int iloscB = int.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == this.BId).ToList();

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
            Console.Write("Chcesz usunąć dane dla pojedyńczego pracownika czy dla wszystkich w Twojej brygadzie?:\n" +
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
                        Console.Write("Podaj datę przepracowanego dnia (YYYY-MM-DD): ");
                        var dateB = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == this.BId).ToList();

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
            Console.Write("Chcesz wprowadzić dane dla pojedyńczego pracownika czy dla wszystkich w Twojej brygadzie?:\n" +
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
                        Console.Write("Podaj datę rozpoczęcia (YYYY-MM-DD): ");
                        var date3 = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj datę zakończenia (YYYY-MM-DD): ");
                        var date4 = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == this.BId).ToList();

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
            Console.Write("Chcesz usunąć dane dla pojedyńczego pracownika czy dla wszystkich w Twojej brygadzie?:\n" +
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
                        Console.Write("Podaj datę rozpoczęcia (YYYY-MM-DD): ");
                        var dateB = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == this.BId).ToList();

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
            Console.Write("Chcesz wprowadzić dane dla pojedyńczego pracownika czy dla wszystkich w Twojej brygadzie?:\n" +
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
                        Console.Write("Podaj datę zaliczki (YYYY-MM-DD): ");
                        var date2 = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Podaj kwotę: ");
                        var kwota2 = int.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == this.BId).ToList();

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
            Console.Write("Chcesz usunąć dane dla pojedyńczego pracownika czy dla wszystkich w Twojej brygadzie?:\n" +
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
                        Console.Write("Podaj datę zaliczki (YYYY-MM-DD): ");
                        var dateB = DateOnly.Parse(Console.ReadLine());

                        using (var context = new FirmaContext())
                        {
                            var employees = context.PRacownicy.Where(p => p.BId == this.BId).ToList();

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
}
