using Firma.Modele;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Firma;

internal class LogReg
{
    public static void Start()
    {
        string answer;

        while (true)
        {
            Console.Clear();
            Console.Write("Menu:\n'l' - logowanie (właściciel / brygadzista)\n'r' - rejestracja (właściciel)\n'q' - wyjście\nWybór: ");
            answer = Console.ReadLine();
            switch (answer.ToLower())
            {
                case "l":
                    if (BossExists())
                    {
                        Logowanie();
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("W bazie nie ma żadnych właścicieli. Proszę się zarejestrować.\nNaciśnij jakikolwiek klawisz aby kontynuować...");
                        Console.ReadKey();
                        continue;
                    }
                    break;
                case "r":
                    if (BossExists())
                    {
                        Console.Clear();
                        Console.Write("Właściciel już istnieje. Proszę się zalogować.\nNaciśnij jakikolwiek klawisz aby kontynuować...");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        Register();
                    }
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.Write("Nieprawidłowo wybrana opcja. \nNaciśnij jakikolwiek klawisz aby kontynuować...");
                    Console.ReadKey();
                    continue;
            }
            break;
        }
    }

    public static void Register()
    {
        string username, password;

        while (true)
        {
            Console.Clear();
            Console.Write("Login: ");
            username = Console.ReadLine();
            Console.Write("Hasło: ");
            password = Console.ReadLine();

            if (username.Length > 2 && password.Length > 2)
                break;
        }

        using var context = new FirmaContext();
        context.Loginy.Add(new Login { Username = username, Passw = password, Rola = "W" });
        context.SaveChanges();

        Console.Clear();
        Console.Write("Rejestracja przebiegła pomyślnie.\nProszę się zalogować.\nNaciśnij jakikolwiek klawisz aby kontynuować...");
        Console.ReadKey();
        Start();
    }

    public static void Logowanie()
    {
        string username, password, answer;
        bool askAgain = true;

        while (askAgain)
        {
            Console.Clear();
            Console.Write("Login: ");
            username = Console.ReadLine();
            Console.Write("Hasło: ");
            password = Console.ReadLine();

            using var context = new FirmaContext();
            var user = context.Loginy.FirstOrDefault(u => u.Username == username);
            if (user != null && user.Passw == password)
            {
                askAgain = false;
                if (user.Rola == "W")
                {
                    var wlasciciel = new Wlasciciel();
                    wlasciciel.Menu();
                }
                else if (user.Rola == "B")
                {
                    var brygadzista = new Brygadzista(user.BId);
                    brygadzista.Menu();
                }
            }
            else
            {
                Console.Clear();
                Console.Write("Login lub hasło jest nieprawidłowe.\nNaciśnij 'q' aby wyjść lub cokolwiek innego aby spróbować ponownie.\nWybór:");
                switch (Console.ReadLine().ToLower())
                {
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        continue;
                }
            }
        }
    }
    private static bool BossExists()
    {
        using var context = new FirmaContext();
        return context.Loginy.Any(l => l.Rola == "W");
    }
}
