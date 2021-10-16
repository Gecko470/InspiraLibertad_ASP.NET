using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InspiraLibertad.Models;
using InspiraLibertad.Clases;

namespace InspiraLibertad.Pages
{
    public class ContactoModel : PageModel
    {
        private readonly InspiraLibertad.Models.ILDBContext _context;
        public int res = -1;

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
                res = 0;
                return Page();
            }

            _context.Mensajes.Add(Mensajes);
            await _context.SaveChangesAsync();
            Correo.enviarCorreo("codeworks9@gmail.com", "Mensaje Cliente Inspira Libertad", "<p>Nombre Cliente: " + Mensajes.Nombre + "</p><p>Email Cliente: " + Mensajes.Email + "</p><p>Mensaje Cliente: " + Mensajes.Mensaje + "</p>");

            res = 1;
            return Page();
        }
    }
}
