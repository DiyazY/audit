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

    public void OnGet()
    {

    }
}
