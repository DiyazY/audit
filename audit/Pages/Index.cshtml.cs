using audit.Models;
using audit.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace audit.Pages;

public class IndexModel : PageModel
{
    private readonly AuditService _auditService;

    public IndexModel(AuditService auditService)
    {
        _auditService = auditService;
    }

    public IEnumerable<string> ErrorMessages{get;set;}

    public IEnumerable<AuditModel> AuditObjects{get;set;}

    // public void OnGet()
    // {

    // }

    public async Task OnGetAsync(Guid id)
    {
        // TODO: add some logic there!!!
        AuditObjects = await _auditService.GetModelsOfAuditableObjectThroughItsLifecycle(id);
    }
}
