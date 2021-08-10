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
    public class PasswordModel : PageModel
    {
        private readonly InspiraLibertad.Models.ILDBContext _context;

        public PasswordModel(InspiraLibertad.Models.ILDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contrasenia Contrasenia { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int numVeces = _context.Cliente.Where(p => p.NombreUsuario == Contrasenia.NombreUsuario && p.Email == Contrasenia.Email && p.Telefono == Contrasenia.Telefono && p.Token == null).Count();
            if (numVeces != 0)
            {
                Correo.enviarCorreo(Contrasenia.Email, "Recupera tu Password de Inspira Libertad", " Para recuperar tu Password de Inspira Libertad haz click <a href='https://localhost:44380/RecuperarPassword/"+ Contrasenia.Email +"'>Aquí</a>");

                return RedirectToPage("./Index");
            }
            else
            {
                ViewData["respuesta"] = 0;
                return Page();
            }
        }
    }
}
