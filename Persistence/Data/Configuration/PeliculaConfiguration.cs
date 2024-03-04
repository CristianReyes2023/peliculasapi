using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class PeliculaConfiguration : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("peliculas");

            builder.Property(e => e.Anio)
                .HasMaxLength(50)
                .HasColumnName("anio");
            builder.Property(e => e.Director)
                .HasMaxLength(50)
                .HasColumnName("director");
            builder.Property(e => e.Genero)
                .HasMaxLength(50)
                .HasColumnName("genero");
            builder.Property(e => e.Titulo)
                .HasMaxLength(50)
                .HasColumnName("titulo");
        }
    }
}