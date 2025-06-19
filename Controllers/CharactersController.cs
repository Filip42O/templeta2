using CourseStudent.Services;

namespace CourseStudent.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/characters")]
public class CharactersController : ControllerBase
{
    private readonly ICharacterService _svc;
    public CharactersController(ICharacterService svc) => _svc = svc;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var dto = await _svc.GetCharacterAsync(id);
        if (dto == null)
            return NotFound(new { message = "Character not found" });

        return Ok(dto);
    }
    
    
    [HttpPost("{id}/backpacks")]
    public async Task<IActionResult> AddToBackpack(
        int id,
        [FromBody] List<int> itemIds)
    {
        var error = await _svc.AddBackpackItemsAsync(id, itemIds);
        if (error != null)
        {
            if (error == "Character not found")
                return NotFound(new { message = error });
            return BadRequest(new { message = error });
        }

        return NoContent();
    }
    
    
    
}