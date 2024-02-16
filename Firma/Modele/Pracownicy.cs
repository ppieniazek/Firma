using System;
using System.Collections.Generic;

namespace Firma.Modele;

public partial class Pracownicy
{
    public int PId { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string KodPocztowy { get; set; } = null!;

    public string Stanowisko { get; set; } = null!;

    public DateOnly DataUrodzenia { get; set; }

    public DateOnly DataPrzyjecia { get; set; }

    public int? BId { get; set; }

    public int Stawka { get; set; }

}
