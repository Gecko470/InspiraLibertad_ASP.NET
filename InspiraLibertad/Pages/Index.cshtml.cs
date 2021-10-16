using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspiraLibertad.Models;
using InspiraLibertad.Clases;

namespace InspiraLibertad.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILDBContext _context;

        public IndexModel(ILDBContext context)
        {
            _context = context;
        }

        Stack<int> Cursos = new Stack<int>();
        Stack<string> noCursos = new Stack<string>();
        public Compras compra = new Compras();


        [BindProperty]
        public InputCursos inpCursos { get; set; }
        public void OnPostComprar()
        {

            if(inpCursos.InpCursos0 != -1)
            {
                Comprobaciones(inpCursos.InpCursos0);
            }
            if(inpCursos.InpCursos1 != -1)
            {
                Comprobaciones(inpCursos.InpCursos1);
            }
            if (inpCursos.InpCursos2 != -1)
            {
                Comprobaciones(inpCursos.InpCursos2);
            }

            if(Cursos.Count() > 0)
            {
                
                foreach(var curso in Cursos)
                {
                    compra.ClienteId = Convert.ToInt32(SessionHelper.GetNameIndentifier(User));
                    compra.ProductoId = curso;

                    _context.Compras.Add(compra);
                    _context.SaveChanges();

                    ViewData["res2"] = "1";
                }
            }
        }

        public void Comprobaciones(int id)
        {
            bool numVeces = _context.Compras.Where(p => p.ClienteId.ToString() == SessionHelper.GetNameIndentifier(User) && p.ProductoId == id).Any();

            if (numVeces)
            {
                var curso = _context.Producto.Where(p => p.Id == id).SingleOrDefault();
                noCursos.Push(curso.Nombre);
                ViewData["noCursos"] = noCursos;
                ViewData["res1"] = "1";
            }
            else
            {
                Cursos.Push(id);
            }
        }
    }
}
