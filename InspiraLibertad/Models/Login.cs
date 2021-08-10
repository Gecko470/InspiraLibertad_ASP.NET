using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspiraLibertad.Models
{
    public class Login
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Este campo es obligatorio")]
        //[MaxLength(40, ErrorMessage = "Este campo tiene una longitud máxima de 40 caracteres")]
        //[RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Introduzca un Email válido")]
        //public string Email { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Este campo tiene una longitud máxima de 30 caracteres")]
        [Display(Name = "Nombre Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
