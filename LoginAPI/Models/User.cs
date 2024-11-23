using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Models;

[Table("users")]
[Index("Email", Name = "email_UNIQUE", IsUnique = true)]
[Index("Id", Name = "id_UNIQUE", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(200)]
    public string Nombre { get; set; } = null!;

    [Column("email")]
    [StringLength(200)]
    public string Email { get; set; } = null!;

    [Column("psswd")]
    [StringLength(200)]
    public string Psswd { get; set; } = null!;

    [Column("creado", TypeName = "datetime")]
    public DateTime Creado { get; set; }

    [Column("modificado", TypeName = "datetime")]
    public DateTime Modificado { get; set; }

    [Column("ultimo", TypeName = "datetime")]
    public DateTime Ultimo { get; set; }

    [Column("token")]
    [StringLength(200)]
    public string Token { get; set; } = null!;

    [Required]
    [Column("activo")]
    public bool? Activo { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
