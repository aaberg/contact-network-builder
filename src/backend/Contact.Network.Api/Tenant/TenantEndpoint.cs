using Contact.Network.Domain.TenantContext;
using Contact.Network.Service;
using Contact.Network.Service.Tenant;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Network.Api.Contact; 

[ApiController]
[Route("api/tenant")]
public class TenantEndpoint : ControllerBase {

    private readonly IApplicationService<Tenant> _applicationService;

    public TenantEndpoint(IApplicationService<Tenant> applicationService) {
        _applicationService = applicationService;
    }
    
    [HttpPost("register-private")]
    public async Task<Tenant> CreateTenant([FromBody] Commands.RegisterPrivateTenant command) {
        var tenant = await _applicationService.Handle(command);
        return tenant!;
    }

    [HttpPost("register-organization")]
    public async Task<Tenant> RegisterOrganizationTenant([FromBody] Commands.RegisterOrganizationTenant command) {
        var tenant = await _applicationService.Handle(command);
        return tenant;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTenant([FromRoute] Guid id) {
        var tenant = await _applicationService.Load(id);
        if (tenant == null) {
            return NotFound();
        }

        return Ok(tenant);
    }
    
    [HttpPatch("rename")]
    public async Task<IActionResult> RenameTenant([FromBody] Commands.RenameTenant command) => Ok(await _applicationService.Handle(command));
    
}