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
    public class LoginModel : PageModel
    {
        public int _idCliente;

        private readonly InspiraLibertad.Models.ILDBContext _context;

        public LoginModel(InspiraLibertad.Models.ILDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Login Login { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string clave = Login.Password;
            SHA256Managed sha = new SHA256Managed();
            byte[] buffer = Encoding.Default.GetBytes(clave);
            byte[] claveCifrada = sha.ComputeHash(buffer);
            string claveCifradaString = BitConverter.ToString(claveCifrada).Replace("-", "");

            Login.Password = claveCifradaString;

            int numVeces = _context.Cliente.Where(p => p.NombreUsuario == Login.NombreUsuario && p.Password == Login.Password && p.Token == null).Count();
            if (numVeces != 0)
            {
                _idCliente = _context.Cliente.Where(p => p.NombreUsuario == Login.NombreUsuario && p.Password == Login.Password).First().Id;

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
