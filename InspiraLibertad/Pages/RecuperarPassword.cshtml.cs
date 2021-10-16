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

namespace InspiraLibertad.Pages
{
    public class RecuperarPasswordModel : PageModel
    {
        private readonly InspiraLibertad.Models.ILDBContext _context;
        public int res = -1;

        public RecuperarPasswordModel(InspiraLibertad.Models.ILDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RecuperarPassword RecuperarPassword { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string email)
        {
            if (!ModelState.IsValid)
            {
                
            }

            Cliente cliente = _context.Cliente.Where(p => p.Email == email).First();

            string clave = RecuperarPassword.Password;
            SHA256Managed sha = new SHA256Managed();
            byte[] buffer = Encoding.Default.GetBytes(clave);
            byte[] claveCifrada = sha.ComputeHash(buffer);
            string claveCifradaString = BitConverter.ToString(claveCifrada).Replace("-", "");

            cliente.Password = claveCifradaString;
            await _context.SaveChangesAsync();

            res = 1;
            return Page();

        }
    }
}
