using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspiraLibertad.Clases;
using InspiraLibertad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace InspiraLibertad.Pages
{
    [Authorize]
    public class aPersonalModel : PageModel
    {

        private readonly ILDBContext _context;
        public int res = -1;

        public aPersonalModel(ILDBContext context)
        {
            _context = context;
        }

        public IList<Producto> listaProductos { get; set; }

        public async Task OnGetAsync()
        {
            var lista = from compra in _context.Compras
                        join producto in _context.Producto
                        on compra.ProductoId equals producto.Id
                        where compra.ClienteId.ToString() == SessionHelper.GetNameIndentifier(User)
                        select new Producto
                        {
                            Id = producto.Id,
                            Nombre = producto.Nombre,
                            Video = producto.Video
                        };

            listaProductos = await lista.ToListAsync();
            
            if(listaProductos.Count() == 0)
            {
                res = 0;
                
            }
        }
    }
}
