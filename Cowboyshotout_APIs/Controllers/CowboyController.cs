using CowboyShotout_DataLayer.Models.ViewModels;
using CowboyShotout_DataLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cowboyshotout_APIs.Controllers;

public class CowboyController : ControllerBase
{
    private readonly ICowboyService _cowboyService;

    public CowboyController(ICowboyService cowboyService)
    {
        _cowboyService = cowboyService;
    }

    // POST: api/cowboy
    [HttpPost]
    public async Task<IActionResult> CreateCowboy([FromBody] CowboyViewModel cowboyViewModel)
    {
        var createdCowboy = await _cowboyService.CreateCowboy(cowboyViewModel);
        return CreatedAtAction(nameof(GetCowboy), new { id = createdCowboy.Id }, createdCowboy);
    }

    // GET: api/cowboy/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCowboy(int id)
    {
        var cowboy = await _cowboyService.GetCowboy(id);
        if (cowboy == null)
        {
            return NotFound();
        }

        return Ok(cowboy);
    }
}