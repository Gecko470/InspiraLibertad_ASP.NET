using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InspiraLibertad.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage = "Este campo tiene una longitud máxima de 50 caracteres")]
        [Display(Name = "Nombre Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage = "Este campo tiene una longitud máxima de 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(40, ErrorMessage = "Este campo tiene una longitud máxima de 40 caracteres")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Introduzca un Email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(20, ErrorMessage = "Este campo tiene una longitud máxima de 20 caracteres")]
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Introduzca un número de teléfono válido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [NotMapped]
        [Display(Name = "Repite Password")]
        public string repitePassword { get; set; }
        public string Token { get; set; }

    }
}