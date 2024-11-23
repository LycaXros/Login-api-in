using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Models;

[Table("phones")]
[Index("UserId", Name = "_idx")]
[Index("Id", Name = "id_UNIQUE", IsUnique = true)]
public partial class Phone
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userId")]
    public int UserId { get; set; }

    [Column("number")]
    [StringLength(45)]
    public string Number { get; set; } = null!;

    [Column("ciudadCode")]
    [StringLength(45)]
    public string CiudadCode { get; set; } = null!;

    [Column("paisCode")]
    [StringLength(45)]
    public string PaisCode { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Phones")]
    public virtual User User { get; set; } = null!;
}
