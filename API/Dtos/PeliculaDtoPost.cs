using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Domain.Entities;


namespace API.Dtos
{
public class PeliculaDtoPost
{
    public string Titulo { get; set; } = null!;
    public string Director { get; set; } = null!;
    public string Anio { get; set; } = null!;
    public string Genero { get; set; } = null!;
}
}