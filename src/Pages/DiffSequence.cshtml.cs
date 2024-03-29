
using audit.Models;
using audit.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace;

public class DiffSequenceModel : PageModel
{
    private readonly AuditService _service;
    public DiffSequenceModel(AuditService service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _service = service;
    }
    public IEnumerable<AuditModel> AuditObjects { get; set; }
    public IEnumerable<string> Changes { get; set; }

    public async Task OnGet(Guid id)
    {
        var (models, changes) = await _service.GetModelsOfAuditableObjectThroughItsLifecycle(id);
        AuditObjects = models;
        Changes = changes;
    }
}
