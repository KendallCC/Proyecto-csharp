//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.Carrito = new HashSet<Carrito>();
            this.DetallePedido = new HashSet<DetallePedido>();
        }

        public int IDProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Producto")]
        [RegularExpression(@"^[A-Za-z\s]{6,}$", ErrorMessage = "El campo {0} debe contener solo letras y espacios, y tener al menos 6 caracteres")]
        public string NombreProducto { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripci�n")]
        [RegularExpression(@"^[A-Za-z0-9\s]{6,}$", ErrorMessage = "El campo {0} debe contener letras, n�meros y espacios, y tener al menos 6 caracteres")]
        public string DescripcionProducto { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Categor�a")]
        public int IDCategoria { get; set; }

        [Display(Name = "Imagen")]
        public string Imagen { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carrito> Carrito { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<DetallePedido> DetallePedido { get; set; }
    }
}
