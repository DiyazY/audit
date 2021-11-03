using audit.Models;
using audit.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace;

public class SequenceModel : PageModel
{
    private readonly AuditService _auditService;

    public SequenceModel(AuditService auditService)
    {
        _auditService = auditService;
    }

    public IEnumerable<AuditModel> AuditObjects{get;set;}

    public async Task OnGetAsync(Guid id)
    {
        AuditObjects = await _auditService.GetModelsOfAuditableObjectThroughItsLifecycle(id);
    }
}
