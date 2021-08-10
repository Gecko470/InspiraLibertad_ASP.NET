using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InspiraLibertad.Pages
{
    public class TokenModel : PageModel
    {
        private readonly InspiraLibertad.Models.ILDBContext _context;

        public TokenModel(InspiraLibertad.Models.ILDBContext context)
        {
            _context = context;
        }
        public void OnGet(string token)
        {
            int numVeces = _context.Cliente.Where(p => p.Token == token).Count();

            if(numVeces != 0)
            {
                var cliente = _context.Cliente.Where(p => p.Token == token).First();
                cliente.Token = null;

                _context.SaveChanges();

                ViewData["resp"] = 1;
            }
            else
            {
                ViewData["resp"] = 0;
            }
        }
    }
}
