using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Pelicula
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Anio { get; set; } = null!;

    public string Genero { get; set; } = null!;
}
