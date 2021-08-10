using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InspiraLibertad.Models;
using System.Security.Cryptography;
using System.Text;
using InspiraLibertad.Clases;

namespace InspiraLibertad.Pages
{
    public class RegistroModel : PageModel
    {
        private readonly InspiraLibertad.Models.ILDBContext _context;

        public RegistroModel(InspiraLibertad.Models.ILDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string clave = Cliente.Password;
            SHA256Managed sha = new SHA256Managed();
            byte[] buffer = Encoding.Default.GetBytes(clave);
            byte[] claveCifrada = sha.ComputeHash(buffer);
            string claveCifradaString = BitConverter.ToString(claveCifrada).Replace("-", "");

            Cliente.Password = claveCifradaString;

            string token = "";
            for (int i = 0; i < 20; i++)
            {
                Random r = new Random();
                int n = r.Next(0, 9);
                token += n.ToString();
            }
            Cliente.Token = token;

            Correo.enviarCorreo(Cliente.Email, "Activa tu cuenta en Inspira Libertad", " Para activar tu cuenta en Inspira Libertad haz click <a href='https://localhost:44380/Token/" + token + "'>Aquí</a>");
            
            _context.Cliente.Add(Cliente);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
