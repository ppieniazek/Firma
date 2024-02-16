using System;
using System.Collections.Generic;

namespace Firma.Modele;

public partial class Zaliczki
{
    public int ZId { get; set; }

    public int PId { get; set; }

    public DateOnly DataZaliczki { get; set; }

    public int Kwota { get; set; }

}
