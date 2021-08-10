using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InspiraLibertad.Models;

namespace InspiraLibertad.Pages
{
    public class ContactoModel : PageModel
    {
        private readonly InspiraLibertad.Models.ILDBContext _context;

        public ContactoModel(InspiraLibertad.Models.ILDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Mensajes Mensajes { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Mensajes.Add(Mensajes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
