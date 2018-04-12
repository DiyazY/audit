using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using audit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace audit.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        [BindProperty]
        public Request RequestModel { get; set; }

        public IEnumerable<string> ErrorMessages{get;set;}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Guid.Empty == RequestModel.ObjectId)
            {
                if(Guid.Empty == RequestModel.ObjectId && !ModelState.Values.SelectMany(v => v.Errors).Any())
                    ModelState.AddModelError(string.Empty, $"Empty ObjectId's guid: {RequestModel.ObjectId}");
                ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(p=>p.ErrorMessage);
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
