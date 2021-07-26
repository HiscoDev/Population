using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopulationAPI_nh.Models;
using PopulationAPI_nh.Repositories;

namespace PopulationAPI_nh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulationsController : ControllerBase
    {
        
        
        private readonly IPopulationRepository _PopulationRepository;

        public PopulationsController(IPopulationRepository PopulationRepository)
        {
            _PopulationRepository = PopulationRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Population>> GetPopulations()
        {
            return await _PopulationRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Population>> GetPopulations(int id)
        {
            return await _PopulationRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Population>> PostPopulations([FromBody] Population Population)
        {
            var newPopulation = await _PopulationRepository.Create(Population);
            return CreatedAtAction(nameof(GetPopulations), new { id = newPopulation.ID }, newPopulation);
        }

        [HttpPut]
        public async Task<ActionResult> PutPopulations(int id, [FromBody] Population Population)
        {
            if (id != Population.ID)
            {
                return BadRequest();
            }

            await _PopulationRepository.Update(Population);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var PopulationToDelete = await _PopulationRepository.Get(id);
            if (PopulationToDelete == null)
                return NotFound();

            await _PopulationRepository.Delete(PopulationToDelete.ID);
            return NoContent();
        }

    }
}