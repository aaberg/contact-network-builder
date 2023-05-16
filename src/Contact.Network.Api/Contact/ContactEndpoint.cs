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
        var contact = await _applicationService.Handle(command);
        //var contact = await _applicationService.Load(command.Id);
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
    public async Task<IActionResult> RenameContact([FromBody] Commands.RenameContact command) => Ok(await _applicationService.Handle(command));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact([FromRoute] Guid id) {
        await _applicationService.Handle(new Commands.MarkContactAsDeleted(id));

        return Ok();
    }

    [HttpPatch("specify-company")]
    public async Task<IActionResult> SpecifyCompany([FromBody] Commands.SpecifyCompany command) =>
        Ok(await _applicationService.Handle(command));

    [HttpPatch("remove-company")]
    public async Task<IActionResult> RemoveCompany([FromBody] Commands.RemoveCompany command) =>
        Ok(await _applicationService.Handle(command));

    [HttpPatch("specify-job-title")]
    public async Task<IActionResult> SpecifyJobTitle([FromBody] Commands.SpecifyJobTitle command) =>
        Ok(await _applicationService.Handle(command));

    [HttpPatch("remove-job-title")]
    public async Task<IActionResult> RemoveJobTitle([FromBody] Commands.RemoveJobTitle command) =>
        Ok(await _applicationService.Handle(command));
    
    [HttpPatch("specify-birth-day")]
    public async Task<IActionResult> SpecifyBirthDay([FromBody] Commands.SpecifyBirthDay command) =>
        Ok(await _applicationService.Handle(command));
    
    [HttpPatch("remove-birth-day")]
    public async Task<IActionResult> RemoveBirthDay([FromBody] Commands.RemoveBirthDay command) =>
        Ok(await _applicationService.Handle(command));
    
    [HttpPatch("add-phone-number")]
    public async Task<IActionResult> AddPhoneNumber([FromBody] Commands.AddPhoneNumber command) =>
        Ok(await _applicationService.Handle(command));
    
    [HttpPatch("remove-phone-number")]
    public async Task<IActionResult> RemovePhoneNumber([FromBody] Commands.RemovePhoneNumber command) =>
        Ok(await _applicationService.Handle(command));
    
    [HttpPatch("add-email")]
    public async Task<IActionResult> AddEmail([FromBody] Commands.AddEmail command) =>
        Ok(await _applicationService.Handle(command));
    
    [HttpPatch("remove-email")]
    public async Task<IActionResult> RemoveEmail([FromBody] Commands.RemoveEmail command) =>
        Ok(await _applicationService.Handle(command));

}