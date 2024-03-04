using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories
{
    public class PeliculaRepository : GenericRepository<Pelicula>, IPelicula
    {
        private readonly peliculasContext _context;

        public PeliculaRepository(peliculasContext context) : base(context)
        {
            _context = context;
        }
    }
}