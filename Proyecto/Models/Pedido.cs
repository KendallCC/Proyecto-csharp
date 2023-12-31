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

    public partial class Pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedido()
        {
            this.DetallePedido = new HashSet<DetallePedido>();
            this.Factura = new HashSet<Factura>();
            this.HistorialPedido = new HashSet<HistorialPedido>();
        }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "N� pedido")]
        public int IDPedido { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha del pedido")]
        public Nullable<System.DateTime> FechaPedido { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado del pedido")]
        public Nullable<int> EstadoID { get; set; }
        [JsonIgnore]

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallePedido> DetallePedido { get; set; }
        
        public virtual EstadoPedido EstadoPedido { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Factura { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialPedido> HistorialPedido { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
