//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(MesaMetadata))]
    public partial class Mesa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mesa()
        {
            this.FacturaEncabezado = new HashSet<FacturaEncabezado>();
            this.Pedido = new HashSet<Pedido>();
        }
    
        public int Id { get; set; }
        public int IdRestaurante { get; set; }
        public string NumeroMesa { get; set; }
        public Nullable<int> Capacidad { get; set; }
        public Nullable<bool> Estado { get; set; }
        public int IdEstadoMesa { get; set; }
    
        public virtual EstadoMesa EstadoMesa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacturaEncabezado> FacturaEncabezado { get; set; }
        public virtual Restaurante Restaurante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}