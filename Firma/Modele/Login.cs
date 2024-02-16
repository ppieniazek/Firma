using System;
using System.Collections.Generic;

namespace Firma.Modele;

public partial class Login
{
    public string Username { get; set; } = null!;

    public string Passw { get; set; } = null!;

    public string Rola { get; set; } = null!;

    public int? BId { get; set; }

}
