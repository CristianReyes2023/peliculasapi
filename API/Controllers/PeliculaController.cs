using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    public class PeliculaController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PeliculaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PeliculaDtoPost>> Post(PeliculaDtoPost inputDto)
        {
            var pelicula = new Pelicula
            {
                Titulo = inputDto.Titulo,
                Director = inputDto.Director,
                Anio = inputDto.Anio,
                Genero = inputDto.Genero
            };

            _unitOfWork.Peliculas.Add(pelicula);
            await _unitOfWork.SaveAsync();

            if (pelicula.Id == 0)
            {
                return BadRequest();
            }

            var resultDto = _mapper.Map<PeliculaDto>(pelicula);
            return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PeliculaDto>> Get(int id)
        {
            var result = await _unitOfWork.Peliculas.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("No se encontro usuario con ese Id");
            }
            return _mapper.Map<PeliculaDto>(result);
        }

        [HttpPut("{id}")] // 2611
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PeliculaDto>> Put(int id, [FromBody] PeliculaDto resultDto)
        {

            var exists = await _unitOfWork.Peliculas.GetByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }
            if (resultDto.Id == 0)
            {
                resultDto.Id = id;
            }
            if (resultDto.Id != id)
            {
                return BadRequest();
            }
            // Update the properties of the existing entity with values from resultDto
            _mapper.Map(resultDto, exists);
            // The context is already tracking result, so no need to attach it
            await _unitOfWork.SaveAsync();
            // Return the updated entity
            return _mapper.Map<PeliculaDto>(exists);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Peliculas.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.Peliculas.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PeliculaDto>>> Get()
        {
            var results = await _unitOfWork.Peliculas.GetAllAsync();
            return _mapper.Map<List<PeliculaDto>>(results);
        }

    }
}