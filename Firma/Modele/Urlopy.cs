using System;
using System.Collections.Generic;

namespace Firma.Modele;

public partial class Urlopy
{
    public int UId { get; set; }

    public int PId { get; set; }

    public DateOnly DataRozpoczecia { get; set; }

    public DateOnly DataZakonczenia { get; set; }

    public int? Dni { get; set; }

}
