using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspiraLibertad.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public string Video { get; set; }
    }
}
