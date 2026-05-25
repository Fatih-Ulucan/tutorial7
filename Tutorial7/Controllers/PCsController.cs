using Microsoft.AspNetCore.Mvc;
using Tutorial7.DTOs;
using Tutorial7.Services;

namespace Tutorial7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PCsController : ControllerBase
    {
        private readonly IPcService _pcService;

        public PCsController(IPcService pcService)
        {
            _pcService = pcService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPcs()
        {
            var result = await _pcService.GetAllPcsAsync();
            return Ok(result);
        }

        [HttpGet("{id}/components")]
        public async Task<IActionResult> GetPcComponents(int id)
        {
            var result = await _pcService.GetPcWithComponentsAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePc([FromBody] PCRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _pcService.CreatePcAsync(request);
            return CreatedAtAction(nameof(GetAllPcs), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePc(int id, [FromBody] PCRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _pcService.UpdatePcAsync(id, request);
            if (!isUpdated)
            {
                return NotFound();
            }
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePc(int id)
        {
            var isDeleted = await _pcService.DeletePcAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}