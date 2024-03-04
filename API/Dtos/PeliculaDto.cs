using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string Anio { get; set; } = null!;

        public string Genero { get; set; } = null!;
    }
}