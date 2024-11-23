using System;
using System.Collections.Generic;

namespace LoginAPI.Models;

public partial class Phone
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Number { get; set; } = null!;

    public string CiudadCode { get; set; } = null!;

    public string PaisCode { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
