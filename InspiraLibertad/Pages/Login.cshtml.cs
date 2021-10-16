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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace InspiraLibertad.Pages
{
    public class LoginModel : PageModel
    {
        public int res = -1;

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
        public async Task<IActionResult> OnPost()
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

            var usuario = _context.Cliente.Where(p => p.NombreUsuario == Login.NombreUsuario && p.Password == Login.Password).SingleOrDefault();
            if(usuario == null)
            {
                res = 1;
                return Page();
            }
            else if(usuario != null && usuario.Token != null)
            {
                res = 0;
                return Page();
            }
            else
            {
                var usuarioT = _context.Cliente.Where(p => p.NombreUsuario == Login.NombreUsuario && p.Password == Login.Password && p.Token == null).SingleOrDefault();

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()));

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddDays(1), IsPersistent = true });
                ViewData["res3"] = "1";
                return Redirect("/Index");
           
            }
            
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("./Index");
        }
    }
}
