using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Tarjeta
    {
    

        //tarjeta Visa y Mastercard.
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Numeracion")]
        [RegularExpression(@"^4[0-9]{12}(?:[0-9]{3})?$|^5[1-5][0-9]{14}$", ErrorMessage = "El número de tarjeta de crédito no es válido.")]
        public string numeracion { get; set; }

        [Range(typeof(DateTime), "01/01/2023", "31/12/2050", ErrorMessage = "La fecha de vencimiento no es válida.")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de vencimiento")]
        public DateTime FechaVencimiento { get; set; }

        [StringLength(4, MinimumLength = 3, ErrorMessage = "El código de seguridad no es válido.")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "CVV")]
        public int Seguridad { get; set; }

    }
}