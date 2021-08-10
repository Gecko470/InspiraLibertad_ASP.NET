using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspiraLibertad.Models;

namespace InspiraLibertad.Models
{
    public class ILDBContext : DbContext
    {
        public ILDBContext(DbContextOptions<ILDBContext> options):base(options)
        {

        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Mensajes> Mensajes { get; set; }
        public DbSet<InspiraLibertad.Models.Login> Login { get; set; }
        public DbSet<InspiraLibertad.Models.Contrasenia> Contrasenia { get; set; }
        public DbSet<InspiraLibertad.Models.RecuperarPassword> RecuperarPassword { get; set; }
    }
}
