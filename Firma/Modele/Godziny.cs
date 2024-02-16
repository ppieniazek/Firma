using System;
using System.Collections.Generic;

namespace Firma.Modele;

public partial class Godziny
{
    public int GId { get; set; }

    public int PId { get; set; }

    public DateOnly DataDnia { get; set; }

    public int Ilosc { get; set; }

}
