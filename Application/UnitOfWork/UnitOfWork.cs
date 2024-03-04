using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly peliculasContext _context;

        private IPelicula _peliculas;
        public UnitOfWork(peliculasContext context)
        {
            _context = context;
        }

        public IPelicula Peliculas // 2611
        {
            get
            {
                if (_peliculas == null)
                {
                    _peliculas = new PeliculaRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _peliculas;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}