using System;
using System.Collections.Generic;

namespace LoginAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Psswd { get; set; } = null!;

    public DateTime Creado { get; set; }

    public DateTime Modificado { get; set; }

    public DateTime Ultimo { get; set; }

    public string Token { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
