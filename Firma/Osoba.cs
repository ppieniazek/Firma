using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma;

internal class Osoba : IOsoba
{
    public virtual void ZarzadzajGodzinami()
    {
        Console.Clear();
        string menu = "Przepracowane godziny:\n1. Dodaj godziny\n2. Usuń godziny\n'q' - wyjście\n";
        Console.Write(menu);

    }
    public virtual void ZarzadzajZaliczkami()
    {
        Console.Clear();
        string menu = "Wybrane zaliczki:\n1. Dodaj zaliczki\n2. Usuń zaliczki\n'q' - wyjście\n";
        Console.Write(menu);
    }
    public virtual void ZarzadzajUrlopami()
    {
        Console.Clear();
        string menu = "Wybrane urlopy:\n1. Dodaj urlopy\n2. Usuń urlopy\n'q' - wyjście\n";
        Console.Write(menu);
    }
}
internal interface IOsoba
{
    public void ZarzadzajGodzinami();
    public void ZarzadzajZaliczkami();
    public void ZarzadzajUrlopami();
}
