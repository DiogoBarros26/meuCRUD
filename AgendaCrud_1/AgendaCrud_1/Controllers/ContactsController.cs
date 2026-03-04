using AgendaCrud_1.Application.Services;
using AgendaCrud_1.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgendaCrud_1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _service;

    public ContactsController(IContactService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contatos = await _service.GetAllAsync();
        return Ok(contatos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var contato = await _service.GetByIdAsync(id);

        if (contato == null)
            return NotFound();

        return Ok(contato);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Contact contact)
    {
        await _service.CreateAsync(contact);
        return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Contact contact)
    {
        await _service.UpdateAsync(id, contact);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
