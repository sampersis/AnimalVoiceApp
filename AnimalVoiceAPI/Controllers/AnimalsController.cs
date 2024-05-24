using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalAPI.Database;
using AnimalAPI.DTO;
using AutoMapper;
using AnimalAPI.Static;

namespace AnimalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalsdbContext animaldb;
        private readonly IMapper mapper;
        private readonly ILogger<AnimalsController> logger;

        public AnimalsController(AnimalsdbContext context, IMapper mapper, ILogger<AnimalsController> logger)
        {
            animaldb = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAnimals()
        {
            try
            {
                var animals = mapper.Map<IEnumerable<AnimalDto>>(await animaldb.Animals.ToListAsync());
                return Ok(animals);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing HTTP GET in {nameof(GetAnimals)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDto>> GetAnimal(int id)
        {
            try
            {
                var animal = await animaldb.Animals.FindAsync(id);

                if (animal == null)
                {
                    logger.LogWarning($"Record with ID {id} Not Found: {nameof(GetAnimal)}");
                    return NotFound();
                }

                return Ok(mapper.Map<AnimalDto>(animal));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing HTTP GET in {nameof(GetAnimals)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, AnimalDto animalUpdateDto)
        {
            if (id != animalUpdateDto.Id)
            {
                logger.LogWarning($"Update ID {id} invalid in {nameof(PutAnimal)}");
                return BadRequest();
            }

            var animal = await animaldb.Animals.FindAsync(id);

            if (animal == null)
            {
                logger.LogWarning($"{nameof(Animal)} record with ID {id} not found in {nameof(PutAnimal)}");
                return NotFound();
            }

            mapper.Map(animalUpdateDto, animal);

            animaldb.Entry(animal).State = EntityState.Modified;

            try
            {
                await animaldb.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, $"Error Performing GET in {nameof(PutAnimal)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnimalCreateDto>> PostAnimal(AnimalCreateDto animalDto)
        {
            try
            {
                var animal = mapper.Map<Animal>(animalDto);
                await animaldb.Animals.AddAsync(animal);
                await animaldb.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, $"Error Performing POST in {nameof(PostAnimal)}", animalDto);
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            try
            {
                var animal = await animaldb.Animals.FindAsync(id);
                if (animal == null)
                {
                    logger.LogWarning($"{nameof(Animal)} record with ID {id} not found in {nameof(DeleteAnimal)}");
                    return NotFound();
                }

                animaldb.Animals.Remove(animal);
                await animaldb.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteAnimal)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> AnimalExists(int id)
        {
            return await animaldb.Animals.AnyAsync(e => e.Id == id);
        }
    }
}
