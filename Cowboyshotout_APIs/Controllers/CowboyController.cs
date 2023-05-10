using CowboyShotout_DataLayer.Models.ViewModels;
using CowboyShotout_DataLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cowboyshotout_APIs.Controllers;

public class CowboyController : ControllerBase
{
    private readonly CowboyService _service;

    public CowboyController(CowboyService service)
    {
        _service = service;
    }
    // POST: api/cowboy
    [HttpPost]
    public async Task<IActionResult> CreateCowboy([FromBody] CowboyViewModel cowboyViewModel)
    {
        var createdCowboy = await _service.CreateCowboy(cowboyViewModel);
        return CreatedAtAction(nameof(GetCowboy), new { id = createdCowboy.Id }, createdCowboy);
    }

    // GET: api/cowboy/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCowboy(int id)
    {
        var cowboy = await _service.GetCowboy(id);
        if (cowboy == null)
        {
            return NotFound();
        }

        return Ok(cowboy);
    }

    // GET: api/Cowboy
    [HttpGet]
    public async Task<IActionResult> GetAllCowboys()
    {
        var cowboys = await _service.GetAllCowboys();
        return Ok(cowboys);
    }

    // PUT: api/Cowboy/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCowboy(int id, [FromBody] CowboyViewModel cowboy)
    {
        await _service.UpdateCowboy(id, cowboy);
        return Ok();
    }

    // DELETE: api/Cowboy/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCowboy(int id)
    {
        await _service.DeleteCowboy(id);
        return Ok();
    }

    // POST: api/Cowboy/5/shoot
    [HttpPost("{id}/shoot")]
    public async Task<IActionResult> ShootGun(int id)
    {
        await _service.ShootGun(id);
        return Ok();
    }

    // POST: api/Cowboy/5/reload
    [HttpPost("{id}/reload")]
    public async Task<IActionResult> ReloadGun(int id)
    {
        await _service.ReloadGun(id);
        return Ok();
    }

    // GET: api/Cowboy/distance
    [HttpGet("distance")]
    public async Task<IActionResult> DistanceBetweenCowboys(int cowboyId1, int cowboyId2)
    {
        var distance = await _service.DistanceBetweenCowboys(cowboyId1, cowboyId2);
        return Ok(distance);
    }
}