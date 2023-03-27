using Contact.Network.Service;
using Contact.Network.Service.Contact;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Network.Api.Contact; 

[ApiController]
[Route("api/contact")]
public class ContactEndpoint : ControllerBase {
    
    private readonly IApplicationService<Domain.Contact.Contact> _applicationService;

    public ContactEndpoint(IApplicationService<Domain.Contact.Contact> applicationService) {
        _applicationService = applicationService;
    }

    [HttpPost]
    public async Task<Domain.Contact.Contact> CreateContact([FromBody] Commands.CreateContact command) {
        await _applicationService.Handle(command);
        var contact = await _applicationService.Load(command.Id);
        return contact!;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContact([FromRoute] Guid id) {
        var contact = await _applicationService.Load(id);
        if (contact == null) {
            return NotFound();
        }

        return Ok(contact);
    }
    
    [HttpPatch("rename")]
    public async Task<IActionResult> RenameContact([FromBody] Commands.RenameContact command) {
        await _applicationService.Handle(command);
        var contact = await _applicationService.Load(command.Id);
        return contact == null ? NotFound() : Ok(contact);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid id) {
        await _applicationService.Handle(new Commands.MarkContactAsDeleted(id));

        return Ok();
    }
    
    
}